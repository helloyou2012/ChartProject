using Chart.ChartBase;
namespace Test4
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chartDataView1 = new Chart.ChartBase.ChartDataView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // chartDataView1
            // 
            this.chartDataView1.Location = new System.Drawing.Point(40, 28);
            this.chartDataView1.Name = "chartDataView1";
            this.chartDataView1.Size = new System.Drawing.Size(546, 377);
            this.chartDataView1.TabIndex = 0;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.Add(this.menuItem1);
            this.contextMenu1.MenuItems.Add(this.menuItem2);
            this.contextMenu1.MenuItems.Add(this.menuItem3);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "右键1";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "右键2";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "右键3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.ContextMenu = this.contextMenu1;
            this.Controls.Add(this.chartDataView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ChartDataView chartDataView1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;


    }
}

