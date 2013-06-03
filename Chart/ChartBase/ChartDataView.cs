using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Chart.ChartBase
{
    public partial class ChartDataView : UserControl
    {
        private DataTable table;
        public ChartDataView()
        {
            InitializeComponent();
            table = ChartTableFactory.CreateChartTable();
            this.dataGrid1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text = @openFileDialog1.FileName;
                table = ChartTableFactory.CreateChartTable(openFileDialog1.FileName);
                this.dataGrid1.DataSource = table;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditDataForm addForm = new EditDataForm();
            addForm.ShowDialog();
            object[] obj = addForm.CreateRowData();
            if (obj != null)
            {
                ((DataTable)dataGrid1.DataSource).Rows.Add(obj);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGrid1.CurrentRowIndex;
            if (dataGrid1.DataSource != null && i >= 0)
            {
                EditDataForm addForm = new EditDataForm(((DataTable)dataGrid1.DataSource).Rows[i]);
                addForm.ShowDialog();
                object[] obj = addForm.CreateRowData();
                if (obj != null)
                {
                    ((DataTable)dataGrid1.DataSource).Rows[i].ItemArray = obj;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGrid1.CurrentRowIndex;
            if (MessageBox.Show("是否删除了第" + (i+1) + "行？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if(dataGrid1.DataSource!=null&&i>=0)
                    ((DataTable)dataGrid1.DataSource).Rows.RemoveAt(i);
                else
                    MessageBox.Show("删除失败！","提示");
            }
        }
    }
}
