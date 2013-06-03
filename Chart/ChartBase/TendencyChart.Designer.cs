using System;
namespace Chart.ChartBase
{
    partial class TendencyChart
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private xuzhenzhen.com.chart.Windows.PlotSurface2D chart1 = null;
        private xuzhenzhen.com.chart.Windows.PlotSurface2D chart2 = null;
        private xuzhenzhen.com.chart.Windows.PlotSurface2D chart3 = null;

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
            this.chart1 = new xuzhenzhen.com.chart.Windows.PlotSurface2D();
            this.chart2 = new xuzhenzhen.com.chart.Windows.PlotSurface2D();
            this.chart3 = new xuzhenzhen.com.chart.Windows.PlotSurface2D();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart1.Location = new System.Drawing.Point(13, 3);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(519, 125);
            this.chart1.TabIndex = 3;
            this.chart1.Title = "";
            this.chart1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart2.Location = new System.Drawing.Point(13, 134);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(519, 125);
            this.chart2.TabIndex = 2;
            this.chart2.Title = "";
            this.chart2.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart3.Location = new System.Drawing.Point(13, 265);
            this.chart3.Name = "chart3";
            this.chart3.Size = new System.Drawing.Size(519, 155);
            this.chart3.TabIndex = 2;
            this.chart3.Title = "";
            this.chart3.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(544, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 423);
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label12.Location = new System.Drawing.Point(38, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 20);
            this.label12.Text = "123.7";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 20);
            this.label11.Text = "SYS*HR:";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label10.Location = new System.Drawing.Point(38, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 20);
            this.label10.Text = "123.7";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 20);
            this.label9.Text = "HR:";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(38, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 20);
            this.label8.Text = "123.7";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 20);
            this.label7.Text = "MAP:";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label6.Location = new System.Drawing.Point(38, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.Text = "123.7";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.Text = "DIA:";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(38, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.Text = "123.7";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.Text = "SYS:";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(25, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.Text = "00:00:00";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.Text = "Time:";
            // 
            // TendencyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart3);
            this.Name = "TendencyChart";
            this.Size = new System.Drawing.Size(634, 423);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        protected override void OnResize(EventArgs e)
        {
            if (chart1 != null && chart2 != null && chart3 != null)
            {
                float height = this.Height;
                this.chart1.Top = 0;
                this.chart1.Left = -10;
                this.chart1.Height = (int)(0.35 * (height));
                this.chart1.Width = (int)(this.Width + 5-90);

                this.chart2.Top = chart1.Height;
                this.chart2.Left = -10;
                this.chart2.Height = (int)(0.28 * (height));
                this.chart2.Width = (int)(this.Width + 5-90);

                this.chart3.Top = chart1.Height + chart2.Height;
                this.chart3.Left = -10;
                this.chart3.Height = (int)(0.37 * (height));
                this.chart3.Width = (int)(this.Width + 5-90);
            }
            base.OnResize(e);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}
