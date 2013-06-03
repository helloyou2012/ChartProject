using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using xuzhenzhen.com.chart;

namespace Chart.ChartBase
{
    public partial class FrequencyChart : UserControl
    {
        public enum FrequencyType
        {
            SYS_TYPE,DIA_TYPE,MAP_TYPE,HR_TYPE
        }

        private ArrayList dataList;//存储数据
        private string xLabel;//X轴标签

        private HistogramPlot histogramPlot;
        private Grid grid;

        public FrequencyChart()
        {
            InitializeComponent();
            LoadData();
        }
        public FrequencyChart(DataTable dt, FrequencyType type)
        {
            InitializeComponent();
            LoadData(dt,type);
        }
        public FrequencyChart(ArrayList list, FrequencyType type)
        {
            InitializeComponent();
            LoadData(list, type);
        }
        public void LoadData(DataTable dt, FrequencyType type)
        {
            dataList = new ArrayList();
            if (type == FrequencyType.SYS_TYPE)
            {
                xLabel = "SYS:mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    dataList.Add(Convert.ToDouble(row["SYS"].ToString()));
                }
            }
            if (type == FrequencyType.DIA_TYPE)
            {
                xLabel = "DIA:mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    dataList.Add(Convert.ToDouble(row["DIA"].ToString()));
                }
            }
            if (type == FrequencyType.MAP_TYPE)
            {
                xLabel = "MAP:mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    dataList.Add(Convert.ToDouble(row["MAP"].ToString()));
                }
            }
            if (type == FrequencyType.HR_TYPE)
            {
                xLabel = "HeartRate:BPM";
                foreach (DataRow row in dt.Rows)
                {
                    dataList.Add(Convert.ToDouble(row["HeartRate"].ToString()));
                }
            }
            InitDataToChart();
        }
        public void LoadData(ArrayList list, FrequencyType type)
        {
            dataList =list;
            if (type == FrequencyType.SYS_TYPE)
                xLabel = "SYS:mmHg";
            if (type == FrequencyType.DIA_TYPE)
                xLabel = "DIA:mmHg";
            if (type == FrequencyType.MAP_TYPE)
                xLabel = "MAP:mmHg";
            if (type == FrequencyType.HR_TYPE)
                xLabel = "HeartRate:BPM";
            InitDataToChart();
        }
        public void LoadData()
        {
            xLabel = "DIA:mmHg";
            dataList = new ArrayList();
            Random random = new Random();
            for (float i = -11; i <= 14; i += 0.3f)
            {
                float dia = random.Next(40, 100);
                dataList.Add(dia);
            }
            InitDataToChart();
        }
        public void InitDataToChart()
        {
            float min = (float)dataList[0];//最小值
            float max = (float)dataList[0];//最大值
            for (int i = 1; i < dataList.Count; i++)
            {
                if ((float)dataList[i] < min)
                    min = (float)dataList[i];
                if ((float)dataList[i] > max)
                    max = (float)dataList[i];
            }
            int start = (int)(min / 10) * 10;//x轴的起始值
            int end = (int)Math.Ceiling(max / 10) * 10;//x轴的末尾值
            int offset = 10;//x轴的偏移量
            int size = (int)Math.Ceiling((float)(end - start) / offset);//计算柱状图中柱体的个数
            double[] xValues = new double[size];
            double[] yValues = new double[size];
            for (int i = 0; i < size; i++)
            {
                xValues[i] = start + offset * i;
            }
            for (int i = 0; i < dataList.Count; i++)
            {
                int index = (int)(((float)dataList[i] - start) / offset);
                yValues[index]++;
            }
            for (int i = 0; i < size; i++)
            {
                yValues[i] = yValues[i] / dataList.Count * 100;
            }
            /*//图例
            legend = new Legend();
            legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Left);
            legend.VerticalEdgePlacement = Legend.Placement.Inside;
            legend.HorizontalEdgePlacement = Legend.Placement.Outside;
            legend.YOffset = -5;
            legend.BorderStyle = LegendBase.BorderType.Line;
            legend.NumberItemsHorizontally = 2;
            legend.Font = new Font(FontFamily.GenericSerif, 8, FontStyle.Regular);
            legend.AutoScaleText = false;*/
            //网格
            grid = new Grid();
            grid.HorizontalGridType = Grid.GridType.Fine;
            grid.VerticalGridType = Grid.GridType.Fine;
            grid.MajorGridPen.Color = Color.Silver;
            grid.MinorGridPen.Color = Color.Silver;
            /////////////////////////////////////////////
            //柱状图
            histogramPlot = new HistogramPlot();
            histogramPlot.AbscissaData = xValues;
            histogramPlot.OrdinateData = yValues;
            histogramPlot.RectangleBrush = new RectangleBrushes.Solid(Color.Orange);
            histogramPlot.Filled = true;
            histogramPlot.BaseWidth = 0.25f;

            //添加
            chart.Add(grid);
            chart.Add(histogramPlot);
            //
            chart.XAxis1.Label = xLabel;
            chart.XAxis1.TicksCrossAxis = true;
            chart.YAxis1.Label = "%";
            chart.YAxis1.WorldMin = 0;
            chart.YAxis1.LabelOffsetAbsolute = true;
            chart.Padding = 5;
            chart.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalGuideline());
            chart.AddAxesConstraint(new AxesConstraint.AxisPosition(PlotSurface2D.YAxisPosition.Left, 60));
            //
            chart.PlotBackColor = Color.White;
            chart.BackColor = System.Drawing.SystemColors.Control;
            chart.XAxis1.Color = Color.Black;
            chart.YAxis1.Color = Color.Black;
            //chart.Legend = legend;
            chart.LegendZOrder = 1;
            chart.Refresh();
        }
        public Bitmap CreateBitmap()
        {
            Bitmap bi = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bi);
            g.Clear(Color.White);
            chart.DrawToBitmap(bi, new Rectangle(-10, 0, this.Width - 1, this.Height - 1));
            return bi;
        }
    }
}
