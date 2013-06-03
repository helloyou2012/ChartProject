using System;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chart.ChartBase
{
    public partial class EditDataForm : Form
    {
        private bool isOk = false;
        public EditDataForm()
        {
            InitializeComponent();
            this.Text = "添加数据";
        }
        public EditDataForm(DataRow row)
        {
            InitializeComponent();
            this.Text = "修改数据";
            textBox1.ReadOnly = true;
            textBox1.Text = row[0].ToString();
            dateTimePicker1.Text = row[1].ToString();
            dateTimePicker2.Text = row[2].ToString();
            numericUpDown1.Value = Convert.ToDecimal(row[3].ToString());
            numericUpDown2.Value = Convert.ToDecimal(row[5].ToString());
            numericUpDown3.Value = Convert.ToDecimal(row[4].ToString());
            numericUpDown4.Value = Convert.ToDecimal(row[6].ToString());
            numericUpDown5.Value = Convert.ToDecimal(row[8].ToString());
            numericUpDown6.Value = Convert.ToDecimal(row[7].ToString());
            textBox2.Text = row[9].ToString();
        }
        public object[] CreateRowData()
        {
            object[] objs = null;
            if (isOk)
            {
                objs = new object[10];
                objs[0] = textBox1.Text;
                objs[1] = dateTimePicker1.Text;
                objs[2] = dateTimePicker2.Text;
                objs[3] = numericUpDown1.Value.ToString();
                objs[4] = numericUpDown3.Value.ToString();
                objs[5] = numericUpDown2.Value.ToString();
                objs[6] = numericUpDown4.Value.ToString();
                objs[7] = numericUpDown6.Value.ToString();
                objs[8] = numericUpDown5.Value.ToString();
                objs[9] = textBox2.Text;
            }
            return objs;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown5.Value = numericUpDown1.Value * numericUpDown4.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown5.Value = numericUpDown1.Value * numericUpDown4.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isOk = true;
            this.Close();
        }
    }
}