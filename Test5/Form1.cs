using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Chart.ChartBase;
using System.Drawing.Imaging;

namespace Test5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text = @openFileDialog1.FileName;
                DataTable table = ChartTableFactory.CreateChartTable(openFileDialog1.FileName);
                this.tendencyChart1.LoadData(table);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "位图(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg;*.jpeg;*,jpe|Gif(*.gif)|*.gif|Png(*.png)|*.png|所有文件(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap bm = tendencyChart1.CreateBitmap();
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        bm.Save(saveFileDialog1.FileName,ImageFormat.Bmp);
                    }
                    if (saveFileDialog1.FilterIndex == 2)
                    {
                        bm.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    if (saveFileDialog1.FilterIndex == 3)
                    {
                        bm.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                    }
                    if (saveFileDialog1.FilterIndex == 5)
                    {
                        bm.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}