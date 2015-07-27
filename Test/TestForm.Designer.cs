namespace DataGraphTest
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonFitX = new System.Windows.Forms.ToolStripButton();
            this.buttonFitY = new System.Windows.Forms.ToolStripButton();
            this.buttonFitXY = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonDrawPoints = new System.Windows.Forms.ToolStripButton();
            this.buttonHighQuality = new System.Windows.Forms.ToolStripButton();
            this.buttonDrawXScroll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonLiveData = new System.Windows.Forms.ToolStripButton();
            this.liveTimer = new System.Windows.Forms.Timer(this.components);
            this.graph = new DataGraph.DataGraph();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar.Location = new System.Drawing.Point(0, 487);
            this.trackBar.Maximum = 10000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(892, 45);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 1000;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonFitX,
            this.buttonFitY,
            this.buttonFitXY,
            this.toolStripSeparator1,
            this.buttonDrawPoints,
            this.buttonHighQuality,
            this.buttonDrawXScroll,
            this.toolStripSeparator2,
            this.buttonLiveData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(892, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonFitX
            // 
            this.buttonFitX.Image = global::DataGraphTest.Properties.Resources.ZoomToWidth;
            this.buttonFitX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFitX.Name = "buttonFitX";
            this.buttonFitX.Size = new System.Drawing.Size(50, 22);
            this.buttonFitX.Text = "Fit X";
            this.buttonFitX.Click += new System.EventHandler(this.buttonFitX_Click);
            // 
            // buttonFitY
            // 
            this.buttonFitY.Image = ((System.Drawing.Image)(resources.GetObject("buttonFitY.Image")));
            this.buttonFitY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFitY.Name = "buttonFitY";
            this.buttonFitY.Size = new System.Drawing.Size(50, 22);
            this.buttonFitY.Text = "Fit Y";
            this.buttonFitY.Click += new System.EventHandler(this.buttonFitY_Click);
            // 
            // buttonFitXY
            // 
            this.buttonFitXY.Image = global::DataGraphTest.Properties.Resources.ZoomToFit;
            this.buttonFitXY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFitXY.Name = "buttonFitXY";
            this.buttonFitXY.Size = new System.Drawing.Size(67, 22);
            this.buttonFitXY.Text = "Fit X&&Y";
            this.buttonFitXY.Click += new System.EventHandler(this.buttonFitXY_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonDrawPoints
            // 
            this.buttonDrawPoints.CheckOnClick = true;
            this.buttonDrawPoints.Image = global::DataGraphTest.Properties.Resources.PencilTool_206;
            this.buttonDrawPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDrawPoints.Name = "buttonDrawPoints";
            this.buttonDrawPoints.Size = new System.Drawing.Size(90, 22);
            this.buttonDrawPoints.Text = "Draw points";
            this.buttonDrawPoints.CheckStateChanged += new System.EventHandler(this.buttonDrawPoints_CheckStateChanged);
            // 
            // buttonHighQuality
            // 
            this.buttonHighQuality.CheckOnClick = true;
            this.buttonHighQuality.Image = global::DataGraphTest.Properties.Resources.XrayView;
            this.buttonHighQuality.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHighQuality.Name = "buttonHighQuality";
            this.buttonHighQuality.Size = new System.Drawing.Size(92, 22);
            this.buttonHighQuality.Text = "High quality";
            this.buttonHighQuality.CheckStateChanged += new System.EventHandler(this.buttonHighQuality_CheckStateChanged);
            // 
            // buttonDrawXScroll
            // 
            this.buttonDrawXScroll.CheckOnClick = true;
            this.buttonDrawXScroll.Image = global::DataGraphTest.Properties.Resources._2_two_columns_9708;
            this.buttonDrawXScroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDrawXScroll.Name = "buttonDrawXScroll";
            this.buttonDrawXScroll.Size = new System.Drawing.Size(110, 22);
            this.buttonDrawXScroll.Text = "Draw X position";
            this.buttonDrawXScroll.CheckStateChanged += new System.EventHandler(this.buttonDrawXScroll_CheckStateChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonLiveData
            // 
            this.buttonLiveData.CheckOnClick = true;
            this.buttonLiveData.Image = global::DataGraphTest.Properties.Resources.GotoNextRow_289;
            this.buttonLiveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonLiveData.Name = "buttonLiveData";
            this.buttonLiveData.Size = new System.Drawing.Size(74, 22);
            this.buttonLiveData.Text = "Live data";
            this.buttonLiveData.CheckStateChanged += new System.EventHandler(this.buttonLiveData_CheckStateChanged);
            // 
            // liveTimer
            // 
            this.liveTimer.Tick += new System.EventHandler(this.liveTimer_Tick);
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.White;
            this.graph.BorderColor = System.Drawing.Color.Black;
            this.graph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.DrawHighQuality = false;
            this.graph.DrawPoints = false;
            this.graph.DrawScrollX = false;
            this.graph.GridColor = System.Drawing.Color.Silver;
            this.graph.Location = new System.Drawing.Point(0, 25);
            this.graph.MinGridSizeX = 1F;
            this.graph.Name = "graph";
            this.graph.ScrollSpeedX = 1F;
            this.graph.ScrollX = 0F;
            this.graph.Size = new System.Drawing.Size(892, 462);
            this.graph.TabIndex = 0;
            this.graph.TextColor = System.Drawing.Color.Black;
            this.graph.ZoomX = 1F;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 532);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.trackBar);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data graph test";
            this.Load += new System.EventHandler(this.TestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGraph.DataGraph graph;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonFitX;
        private System.Windows.Forms.ToolStripButton buttonFitY;
        private System.Windows.Forms.ToolStripButton buttonFitXY;
        private System.Windows.Forms.ToolStripButton buttonDrawPoints;
        private System.Windows.Forms.ToolStripButton buttonHighQuality;
        private System.Windows.Forms.ToolStripButton buttonDrawXScroll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonLiveData;
        private System.Windows.Forms.Timer liveTimer;
    }
}

