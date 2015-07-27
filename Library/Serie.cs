using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace DataGraph
{
    /// <summary>
    /// Data graphics point series 
    /// </summary>
    public class Serie
    {
        // Configuration
        private string name;
        private float minGridSizeY = 10.0f;
        private Color lineColor = Color.Navy;
        private Color pointColor = Color.Black;
        private float lineThickness = 1.0f;

        // Variables        
        private PointList points = new PointList();
        private float scrollY = 0.0f;
        private float zoomY = 1.0f;        
        private object tag;

        // Events
        public delegate void OnRedrawEvent();
        public event OnRedrawEvent OnRedraw;

        /// <summary>
        /// Constructor 
        /// </summary>
        public Serie()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Serie name</param>
        public Serie(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Points serie name 
        /// </summary>
        [DisplayName("Name"), Description("Points serie name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Line color
        /// </summary>
        [DisplayName("Line color"), Description("Line color")]
        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        /// <summary>
        /// Line thickness
        /// </summary>
        [DisplayName("Line thickness"), Description("Line thickness (default 1.0)")]
        public float LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        /// <summary>
        /// Point color
        /// </summary>
        [DisplayName("Point color"), Description("Point color (if points are drawn)")]
        public Color PointColor
        {
            get { return pointColor; }
            set { pointColor = value; }
        }

        /// <summary>
        /// Points list 
        /// </summary>
        [DisplayName("Points list"), Description("Points list")]
        public PointList Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        /// <summary>
        /// Scolle position on Y axis
        /// </summary>
        [DisplayName("Scroll Y"), Description("Scroll postion on Y axis")]
        public float ScrollY
        {
            get { return scrollY; }
            set { scrollY = value; }
        }

        /// <summary>
        /// Zoom factor on Y axis
        /// </summary>
        [DisplayName("Zoom Y"), Description("Zoom factor on Y axis")]
        public float ZoomY
        {
            get { return zoomY; }
            set { zoomY = value; }
        }

        /// <summary>
        /// Minimal grid size on Y axis
        /// </summary>
        [DisplayName("Min grid Y"), Description("Minimal grid size on Y axis")]
        public float MinGridSizeY
        {
            get { return minGridSizeY; }
            set { minGridSizeY = value; }
        }

        /// <summary>
        /// General purpose tag object
        /// </summary>        
        [DisplayName("Tag"), Description("General purpose tag")]
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        /// <summary>
        /// Fit points on Y axis
        /// </summary>
        public void FitY()
        {
            float minY = float.MaxValue, maxY = float.MinValue;

            // Find min and max Y value
            for (int i = 0; i < points.Count; i++)
            {
                minY = Math.Min(minY, points[i].Y);
                maxY = Math.Max(maxY, points[i].Y);
            }

            // Calculate Y zoom and Y scroll
            if (maxY > minY)
            {
                float diff = maxY - minY;

                zoomY = (float)(80) / (maxY - minY);
            }
            else
            {
                zoomY = 1.0f;
            }

            scrollY = minY + (maxY - minY) / 2.0f;

            Redraw();
        }

        /// <summary>
        /// Redraw invoking
        /// </summary>
        private void Redraw()
        {
            if (OnRedraw != null)
            {
                OnRedraw();
            }
        }
    }
}
