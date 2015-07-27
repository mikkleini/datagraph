using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DataGraph
{
    /// <summary>
    /// Data graphics
    /// </summary>
    public partial class DataGraph : UserControl
    {
        // Configuration
        private float minGridSizeX = 1.0f;
        private float scrollSpeedX = 1.0f;
        private bool showScrollX = false;
        private bool showPoints = true;
        private bool useHQ = false;
        private Color textColor = Color.Black;
        private Color gridColor = Color.Silver;
        private Color borderColor = Color.Black;
        private string axisLabelX = String.Empty;

        // Variables
        private SerieList series = new SerieList();
        private float scrollX = 0.0f;
        private float zoomX = 1.0f;
        private int mousePressLocationX = 0;
        private int mousePressLocationY = 0;
        private float mousePressScrollX = 0.0f;
        private float mousePressScrollY = 0.0f;
        private int mousePressSerieIndex = -1;
        private bool controlDown = false;

        // Constants
        private const int footerHeight = 20;
        private const int headerWidth = 60;
        private const int xLabelWidth = 70;
        private const int yLabelHeight = 20;

        /// <summary>
        /// Constructor
        /// </summary>
        public DataGraph()
        {
            InitializeComponent();

            // Use double buffering for flicker free images
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Point series 
        /// </summary>
        [DisplayName("Series"), Description("Point series")]
        public SerieList Series
        {
            get
            {
                return series;
            }
        }

        /// <summary>
        /// Scroll position on X axis
        /// </summary>
        [DisplayName("Scrolll X"), Description("Scroll position on X axis")]
        public float ScrollX
        {
            get
            {
                return scrollX;
            }
            set
            {
                scrollX = value;
                Redraw();
            }
        }

        /// <summary>
        /// Zoom factor on X axis
        /// </summary>
        [DisplayName("Zoom X"), Description("Zoom factor on X axis")]
        public float ZoomX
        {
            get
            {
                return zoomX;
            }
            set
            {
                zoomX = value;
                Redraw();
            }
        }

        /// <summary>
        /// Minimal grid size on X axis 
        /// </summary>
        [DisplayName("Min grid X"), Description("Minimal grid size on X axis")]
        public float MinGridSizeX
        {
            get { return minGridSizeX; }
            set
            {
                minGridSizeX = value;
                Redraw();
            }
        }

        /// <summary>
        /// Scroll speed (with mouse) on X axis
        /// <summary>
        [DisplayName("Scroll speed X"), Description("Scroll speed on X axis")]
        public float ScrollSpeedX
        {
            get { return scrollSpeedX; }
            set { scrollSpeedX = value; }
        }


        /// <summary>
        /// Draw X axis scroll position
        /// </summary>
        [DisplayName("Show scroll X"), Description("Draw X axis scroll position")]
        public bool DrawScrollX
        {
            get { return showScrollX; }
            set
            {
                showScrollX = value;
                Redraw();
            }
        }

        /// <summary>
        /// Draw data markers ?
        /// </summary>
        [DisplayName("Draw points"), Description("Draw point markers")]
        public bool DrawPoints
        {
            get { return showPoints; }
            set
            {
                showPoints = value;
                Redraw();
            }
        }

        /// <summary>
        /// Text color 
        /// </summary>
        [DisplayName("Text color"), Description("Texts color")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                Redraw();
            }
        }

        /// <summary>
        /// Grid color
        /// </summary>
        [DisplayName("Grid color"), Description("Grid color")]
        public Color GridColor
        {
            get { return gridColor; }
            set
            {
                gridColor = value;
                Redraw();
            }
        }

        /// <summary>
        /// Border color
        /// </summary>
        [DisplayName("Border color"), Description("Border color")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Redraw();
            }
        }

        /// <summary>
        /// High quality (anti-aliasing) drawing ? 
        /// </summary>
        [DisplayName("Draw HQ"), Description("Use high-quality drawing")]
        public bool DrawHighQuality
        {
            get { return useHQ; }
            set
            {
                useHQ = value;
                Redraw();
            }
        }

        /// <summary>
        /// Fit points on X axis
        /// </summary>
        public void FitX()
        {
            float minX = float.MaxValue, maxX = float.MinValue;

            for (int s = 0; s < series.Count; s++)
            {
                // Find min and max X value
                for (int i = 0; i < series[s].Points.Count; i++)
                {
                    minX = Math.Min(minX, series[s].Points[i].X);
                    maxX = Math.Max(maxX, series[s].Points[i].X);
                }
            }

            // Calculate X zoom and X scroll
            if (maxX > minX)
            {
                zoomX = (float)(Width - headerWidth) / (maxX - minX);
            }
            else
            {
                zoomX = 1.0f;
            }

            scrollX = minX + (maxX - minX) / 2.0f;

            Redraw();
        }

        /// <summary>
        /// Fit points on X and Y axis
        /// </summary>
        public void FitXY()
        {
            FitX();
            series.ForEach(serie => serie.FitY());
        }

        /// <summary>
        /// Resizing overridden event 
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Redraw();
        }

        /// <summary>
        /// Key down overriden event
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            controlDown = e.Control;
        }

        /// <summary>
        /// Key up overriden event
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            controlDown = e.Control;
        }

        /// <summary>
        /// Mouse button overridden event
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Remember X when mouse button was pressed down
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Get press position on X axis
                mousePressLocationX = e.X;                
                mousePressScrollX = scrollX;
                
                // Get press position on some serie Y axis
                mousePressSerieIndex = GetSerieIndexByYPostion(e.Y);
                if (mousePressSerieIndex >= 0)
                {
                    mousePressLocationY = e.Y;
                    mousePressScrollY = series[mousePressSerieIndex].ScrollY;
                }
            }
        }

        /// <summary>
        /// Mouse movement overriden event (to scroll on X)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);            

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Scroll on X axis
                int diffX = (e.X - mousePressLocationX);
                scrollX = mousePressScrollX - diffX / zoomX;

                // Is mouse over serie ?
                if (mousePressSerieIndex >= 0)
                {
                    // Scroll on serie Y axis
                    int diffY = (e.Y - mousePressLocationY);
                    series[mousePressSerieIndex].ScrollY = mousePressScrollY + diffY / series[mousePressSerieIndex].ZoomY;
                }

                Redraw();
            }
        }

        /// <summary>
        /// Mouse wheel scroll overridden event 
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // X or Y zoom ?
            if (!controlDown)
            {
                // Adjust zoom X
                if (e.Delta > 0)
                {
                    zoomX *= 1.05f;
                }
                else if (e.Delta < 0)
                {
                    zoomX /= 1.05f;
                }
            }
            else
            {
                // Find serie by Y
                int serieIndex = GetSerieIndexByYPostion(e.Y);
                if (serieIndex >= 0)
                {
                    // Adjust zoom Y
                    if (e.Delta > 0)
                    {
                        series[serieIndex].ZoomY *= 1.05f;
                    }
                    else if (e.Delta < 0)
                    {
                        series[serieIndex].ZoomY /= 1.05f;
                    }
                }
            }

            Redraw();
        }

        /// <summary>
        /// Get serie index by mouse Y position
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetSerieIndexByYPostion(int y)
        {
            if (series.Count > 0)
            {
                // Plot area
                Rectangle plotArea = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, ClientRectangle.Height - footerHeight);

                // Is Y in area ?
                if ((y >= plotArea.Top) && (y <= plotArea.Bottom))
                {
                    // Calculate series area height
                    int seriesHeight = plotArea.Height / series.Count;

                    return (y - plotArea.Top) / seriesHeight;
                }
            }
            
            // Any other case return negative number
            return -1;
        }

        /// <summary>
        /// Painting event 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void DataGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            // Draw on all over the client area (not clip)
            Rectangle workRectangle = this.ClientRectangle;

            // Ensure the area is not to small
            if ((workRectangle.Height < 40) || (workRectangle.Width < 120)) return;
            
            // Plot area
            Rectangle plotArea = new Rectangle(workRectangle.Left, workRectangle.Top, workRectangle.Width, workRectangle.Height - footerHeight);

            // Set quality
            gfx.SmoothingMode = (useHQ ? System.Drawing.Drawing2D.SmoothingMode.HighQuality : System.Drawing.Drawing2D.SmoothingMode.HighSpeed);            
            
            // Any series at all ?
            if (series.Count > 0)
            {
				// Calculate visible X radius and start and end
				float radiusX = ((float)(plotArea.Width - headerWidth) / zoomX) / 2;
				float startX = scrollX - radiusX;
				float endX = scrollX + radiusX;

                // Calculate suitable grid size
                float suitableGridSizeX = (endX - startX) / ((plotArea.Width - headerWidth) / xLabelWidth);
                float gridSizeX = Math.Max(minGridSizeX, (float)Math.Floor(suitableGridSizeX / minGridSizeX) * minGridSizeX);

				// Find X grid start and end
                float gridStartX = (float)Math.Floor(startX / gridSizeX) * gridSizeX;
                float gridEndX = (float)Math.Ceiling(endX / gridSizeX + 1) * gridSizeX;

                // Draw X axis label
                if (!string.IsNullOrEmpty(axisLabelX))
                {
                    gfx.DrawString(axisLabelX, Font, new SolidBrush(textColor), plotArea.Left + 4, plotArea.Bottom + 4);
                }

				// Draw X grid and values
                gfx.SetClip(new Rectangle(plotArea.Left + headerWidth, plotArea.Top, plotArea.Width - headerWidth, workRectangle.Height - plotArea.Top));
                for (float gridX = gridStartX; gridX <= gridEndX; gridX += gridSizeX)
				{                    
                    int x = plotArea.Left + headerWidth + (int)((gridX - startX) * zoomX);

                    // Draw vertical grid line (on X axis)
                    gfx.DrawLine(new Pen(gridColor), x, plotArea.Top, x, plotArea.Bottom - 1);

                    // Round X to neareast min grid size multiple number
                    float roundedGridX = (float)Math.Round(gridX / minGridSizeX) * minGridSizeX;

                    // Draw X axis value
                    string gridXLabel = Math.Round(roundedGridX, 5).ToString();
                    gfx.DrawString(gridXLabel, Font, new SolidBrush(textColor), x - gfx.MeasureString(gridXLabel, Font).Width / 2, plotArea.Bottom + 4);
				}
			
                // Calculate series area height
                int seriesHeight = plotArea.Height / series.Count;              

                // Draw points of series
                for (int i = 0; i < series.Count; i++)
                {                    
                    // Draw series in plot area
                    DrawSeries(gfx, series[i], startX, endX,
                        new Rectangle(plotArea.Left, i * seriesHeight, plotArea.Width, seriesHeight));
                }

                // Draw X scroll position
                if (showScrollX)
                {
                    int x = (int)(radiusX * zoomX) + plotArea.Left + headerWidth;
                    gfx.ResetClip();
                    gfx.DrawLine(Pens.Red, x, plotArea.Top, x, plotArea.Bottom - 1);
                }
            }
        }

        /// <summary>
        /// Drawing one series of points
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="serie"></param>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="area"></param>
        private void DrawSeries(Graphics gfx, Serie serie, float startX, float endX, Rectangle area)
        {
            Pen linePen = new Pen(serie.LineColor, serie.LineThickness);
            Pen pointPen = new Pen(serie.PointColor);
            Pen borderPen = new Pen(borderColor);
            int startIndex = 0, endIndex = 0;            
            int i = 0;            

            // Find first point inside of area
            while (i < serie.Points.Count)
            {
                // Is this first point inside area ?
                if ((i > 0) && (serie.Points[i].X > startX))
                {
                    startIndex = i - 1;                    
                    break;
                }
                i++;
            }

            // Find first point outside the area
            while (i < serie.Points.Count)
            {                
                // Is second point outside of area ?
                if (serie.Points[i].X >= endX)
                {
                    endIndex = i;
                    break;
                }
                // End of list ?
                if (i == (serie.Points.Count - 1))
                {
                    endIndex = i;
                    break;
                }
                i++;
            }

            // Set series drawing area
            gfx.SetClip(area);

            // Calculate visible X radius and start and end
            float radiusY = ((float)area.Height / serie.ZoomY) / 2;
            float startY = serie.ScrollY - radiusY;
            float endY = serie.ScrollY + radiusY;

            // Calculate suitable grid size
            float suitableGridSizeY = (endY - startY) / (area.Height / yLabelHeight);
            float gridSizeY = Math.Max(serie.MinGridSizeY, (float)Math.Floor(suitableGridSizeY / serie.MinGridSizeY) * serie.MinGridSizeY);

            // Find Y grid start and end
            float gridStartY = (float)Math.Floor(startY / gridSizeY) * gridSizeY;
            float gridEndY = (float)Math.Ceiling(endY / gridSizeY + 1) * gridSizeY;
                            
            // Draw Y grid                        
            for (float gridY = gridStartY; gridY <= gridEndY; gridY += gridSizeY)
            {
                int y = area.Bottom - (int)((gridY - startY) * serie.ZoomY);

                // Draw horizontal grid line on Y axis
                gfx.DrawLine(new Pen(gridColor), area.Left + headerWidth, y, area.Right, y);

                // Round Y to neareast min grid size multiple number
                float roundedGridY = (float)Math.Round(gridY / serie.MinGridSizeY) * serie.MinGridSizeY;

                // Draw Y grid label if it fits in area
                string gridYLabel = roundedGridY.ToString();
                SizeF strSize = gfx.MeasureString(gridYLabel, Font);
                float strHeightHalf = strSize.Height / 2.0f;

                if (((y - strHeightHalf) >= area.Top) && ((y + strHeightHalf) < area.Bottom))
                {
                    gfx.DrawString(gridYLabel, Font, new SolidBrush(textColor), area.Left + headerWidth - strSize.Width - 4, y - strHeightHalf);
                }
            }

            // Draw rotated series label            
            Font labelFont = new Font(Font, FontStyle.Bold);
            gfx.TranslateTransform(4, area.Bottom - 4);
            gfx.RotateTransform(-90.0f);
            gfx.DrawString(serie.Name, labelFont, new SolidBrush(textColor), 0, 0);
            gfx.ResetTransform();

            // Draw borders
            gfx.DrawLine(Pens.Black, area.Left + headerWidth, area.Top, area.Left + headerWidth, area.Bottom - 1);
            gfx.DrawLine(Pens.Black, area.Left, area.Bottom - 1, area.Right, area.Bottom - 1);

            // Draw lines and points
            gfx.SetClip(new Rectangle(area.Left + headerWidth, area.Top, area.Width - headerWidth, area.Height));
            for (i = startIndex; i < endIndex; i++)
            {                    
                PointF a = serie.Points[i];
                PointF b = serie.Points[i + 1];

                // Calculate X
                a.X = area.X + headerWidth + (a.X - startX) * zoomX;
                b.X = area.X + headerWidth + (b.X - startX) * zoomX;

                // Calculate Y
                a.Y = area.Bottom - (a.Y - startY) * serie.ZoomY;
                b.Y = area.Bottom - (b.Y - startY) * serie.ZoomY;

                // Draw line between points
                gfx.DrawLine(linePen, a, b);

                // Show points ?
                if (showPoints)
                {
                    gfx.DrawRectangle(pointPen, PointToRectangle(a, 1));

                    // Also draw last point marker
                    if (i == (endIndex - 1))
                    {
                        gfx.DrawRectangle(pointPen, PointToRectangle(b, 1));
                    }
                }
            }
        }

        /// <summary>
        /// Point to surrounding rectangle conversion
        /// </summary>
        /// <param name="point"></param>
        /// <param name="boxHalfSize"></param>
        /// <returns></returns>
        private Rectangle PointToRectangle(PointF point, int boxHalfSize)
        {
            return new Rectangle(
                (int)point.X - boxHalfSize,
                (int)point.Y - boxHalfSize,
                1 + 2 * boxHalfSize,
                1 + 2 * boxHalfSize);
        }

        /// <summary>
        /// Redraw
        /// </summary>
        private void Redraw()
        {
            if (!DesignMode)
            {
                Invalidate();
            }
        }
    }    
}
