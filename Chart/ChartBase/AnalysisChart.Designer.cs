using System;
namespace Chart.ChartBase
{
    partial class AnalysisChart
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private xuzhenzhen.com.chart.Windows.PlotSurface2D chart = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.chart = new xuzhenzhen.com.chart.Windows.PlotSurface2D();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(438, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart.Location = new System.Drawing.Point(13, 38);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(535, 337);
            this.chart.TabIndex = 3;
            this.chart.Title = "";
            this.chart.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            // 
            // AnalysisChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart);
            this.Name = "AnalysisChart";
            this.Size = new System.Drawing.Size(634, 423);
            this.ResumeLayout(false);

        }
        protected override void OnResize(EventArgs e)
        {
            if (chart != null)
            {
                this.chart.Top = 0;
                this.chart.Left = -10;
                this.chart.Height = this.Height-20;
                this.chart.Width = this.Width + 5;
                this.label1.Top = this.Height - 20;
                this.label1.Left = 40;
                this.label1.Height = 20;
                this.label1.Width = this.Width - 40;
            }
            base.OnResize(e);
        }

        #endregion

        private System.Windows.Forms.Label label1;


    }
}
