using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGraph;

namespace DataGraphTest
{
    public partial class TestForm : Form
    {
        private Random rnd = new Random();

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            AddSinusSeries("Sine A");
            AddSinusSeries("Sine B");
            AddSinusSeries("Sine C");
            AddRandomData("Random A");

            // Set zoom
            graph.ZoomX = 1.0f;
            graph.ScrollX = 0.0f;
            graph.MinGridSizeX = 0.5f;
            graph.FitX();

            //graph.Refresh();
        }

        private void AddSinusSeries(string name)
        {
            Serie serie = new Serie(name);
            float x, y;

            for (x = 0.00f; x < 10.00f; x += 0.05f)
            {
                y = (float)(50.0f + 50.0f * Math.Sin(x * 3.0f));
                if (x > 500)
                {
                    //y += 100.0f;
                }

                serie.Points.Add(new PointF(x, y));
            }

            serie.ZoomY = 1.0f;
            serie.ScrollY = 50.0f;
            serie.MinGridSizeY = 10.0f;


            graph.Series.Add(serie);
        }

        private void AddRandomData(string name)
        {
            Serie serie = new Serie(name);
            
            float x, y;

            for (x = -20.00f; x < 120.00f; x += 0.05f)
            {
                y = (float)rnd.NextDouble();

                if (x > 500)
                {
                    y += 100.0f;
                }

                serie.Points.Add(new PointF(x, y));
            }

            serie.MinGridSizeY = 0.1f;
            
            graph.Series.Add(serie);
        }

        private void liveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Serie serie in graph.Series)
            {                
                serie.Points.Add(new PointF(serie.Points.Last.X + (float)rnd.NextDouble() * 10.0f, (float)rnd.NextDouble() * 100.0f));
            }
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            graph.ScrollX = (float)trackBar.Value / 1000.0f;
            Text = graph.ScrollX.ToString();
        }

        private void buttonFitX_Click(object sender, EventArgs e)
        {
            graph.FitX();
        }

        private void buttonFitY_Click(object sender, EventArgs e)
        {
            graph.Series.ForEach(serie => serie.FitY());
        }

        private void buttonFitXY_Click(object sender, EventArgs e)
        {
            graph.FitXY();
        }

        private void buttonDrawPoints_CheckStateChanged(object sender, EventArgs e)
        {
            graph.DrawPoints = buttonDrawPoints.Checked;
        }

        private void buttonHighQuality_CheckStateChanged(object sender, EventArgs e)
        {
            graph.DrawHighQuality = buttonHighQuality.Checked;
        }

        private void buttonDrawXScroll_CheckStateChanged(object sender, EventArgs e)
        {
            graph.DrawScrollX = buttonDrawXScroll.Checked;
        }

        private void buttonLiveData_CheckStateChanged(object sender, EventArgs e)
        {
            liveTimer.Enabled = buttonLiveData.Checked;
        }        
    }
}
