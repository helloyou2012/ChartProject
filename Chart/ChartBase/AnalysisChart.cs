using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using xuzhenzhen.com.chart;

namespace Chart.ChartBase
{
    public partial class AnalysisChart : UserControl
    {
        public enum AnalysisType
        {
            DIA_SYS_TYPE, PULSE_SYS_TYPE,PULSE_DIA_TYPE,PULSE_MAP_TYPE
        }
        private Legend legend;//图例
        private Grid grid;//网格
        private LinePlot linePlot;//回归直线图
        private PointPlot pointPlot;//数据点图

        private string xLabel;//X轴的标签
        private string yLabel;//Y轴的标签
        private string xUnit;//X轴的单位
        private string yUnit;//Y轴的单位
        private ArrayList xList;//X轴数据
        private ArrayList yList;//Y轴数据
        private Statistics cart;//统计信息

        public AnalysisChart()
        {
            InitializeComponent();
            LoadData();
        }
        public AnalysisChart(DataTable dt, AnalysisType type)
        {
            InitializeComponent();
            LoadData(dt,type);
        }
        public AnalysisChart(ArrayList xlist, ArrayList ylist, AnalysisType type)
        {
            InitializeComponent();
            LoadData(xlist, ylist, type);
        }
        public void LoadData(DataTable dt, AnalysisType type)
        {
            xList = new ArrayList();
            yList = new ArrayList();
            if (type == AnalysisType.DIA_SYS_TYPE)
            {
                xLabel = "DIA";
                yLabel = "SYS";
                xUnit = "mmHg";
                yUnit = "mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    xList.Add(Convert.ToDouble(row["DIA"].ToString()));
                    yList.Add(Convert.ToDouble(row["SYS"].ToString()));
                }
            }
            if (type == AnalysisType.PULSE_SYS_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "SYS";
                xUnit = "BPM";
                yUnit = "mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    xList.Add(Convert.ToDouble(row["HeartRate"].ToString()));
                    yList.Add(Convert.ToDouble(row["SYS"].ToString()));
                }
            }
            if (type == AnalysisType.PULSE_DIA_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "DIA";
                xUnit = "BPM";
                yUnit = "mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    xList.Add(Convert.ToDouble(row["HeartRate"].ToString()));
                    yList.Add(Convert.ToDouble(row["DIA"].ToString()));
                }
            }
            if (type == AnalysisType.PULSE_MAP_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "MAP";
                xUnit = "BPM";
                yUnit = "mmHg";
                foreach (DataRow row in dt.Rows)
                {
                    xList.Add(Convert.ToDouble(row["HeartRate"].ToString()));
                    yList.Add(Convert.ToDouble(row["MAP"].ToString()));
                }
            }
            cart = new Statistics(xList, yList);
            InitDataToChart();
        }
        public void LoadData(ArrayList xlist,ArrayList ylist, AnalysisType type)
        {
            xList = xlist;
            yList = ylist;
            if (type == AnalysisType.DIA_SYS_TYPE)
            {
                xLabel = "DIA";
                yLabel = "SYS";
                xUnit = "mmHg";
                yUnit = "mmHg";
            }
            if (type == AnalysisType.PULSE_SYS_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "SYS";
                xUnit = "BPM";
                yUnit = "mmHg";
            }
            if (type == AnalysisType.PULSE_DIA_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "DIA";
                xUnit = "BPM";
                yUnit = "mmHg";
            }
            if (type == AnalysisType.PULSE_MAP_TYPE)
            {
                xLabel = "Pulse";
                yLabel = "MAP";
                xUnit = "BPM";
                yUnit = "mmHg";
            }
            cart = new Statistics(xList, yList);
            InitDataToChart();
        }
        public void LoadData()
        {
            xLabel = "DIA";
            yLabel = "SYS";
            xUnit = "mmHg";
            yUnit = "mmHg";
            xList = new ArrayList();
            yList = new ArrayList();
            Random random = new Random();
            for (float i = -11; i <= 14; i += 0.3f)
            {
                float dia = random.Next(40, 100);
                float sys = random.Next(30, 50) + dia;
                yList.Add(sys);
                xList.Add(dia);
            }
            cart = new Statistics(xList, yList);
            InitDataToChart();
        }
        public void InitDataToChart()
        {
            //图例
            legend = new Legend();
            legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Left);
            legend.VerticalEdgePlacement = Legend.Placement.Inside;
            legend.HorizontalEdgePlacement = Legend.Placement.Outside;
            legend.YOffset = -5;
            legend.BorderStyle = LegendBase.BorderType.Line;
            legend.NumberItemsHorizontally = 2;
            legend.Font = new Font(FontFamily.GenericSerif, 8, FontStyle.Regular);
            legend.AutoScaleText = false;
            //网格
            grid = new Grid();
            grid.HorizontalGridType = Grid.GridType.Fine;
            grid.VerticalGridType = Grid.GridType.Fine;
            grid.MajorGridPen.Color = Color.Silver;
            grid.MinorGridPen.Color = Color.Silver;
            /////////////////////////////////////////////
            //直线图
            linePlot = new LinePlot();
            linePlot.OrdinateData = cart.regressY;
            linePlot.AbscissaData = xList;
            linePlot.Color = Color.Orange;
            linePlot.Pen.Width = 2.0f;
            //点图
            Marker marker = new Marker(Marker.MarkerType.FilledCircle, 3, new Pen(Color.Blue));
            marker.FillBrush = new SolidBrush(Color.RoyalBlue);
            pointPlot = new PointPlot(marker);
            pointPlot.AbscissaData = xList;
            pointPlot.OrdinateData = yList;
            pointPlot.ShowInLegend = false;
            if(cart.regressXYB>0)
                linePlot.Label = yLabel + "=" + cart.regressXYA.ToString("F2") + "*" + xLabel + "+" + cart.regressXYB.ToString("F2")
                    + ",R=" + cart.correlationXY.ToString("F2");
            else
                linePlot.Label = yLabel + "=" + cart.regressXYA.ToString("F2") + "*" + xLabel + cart.regressXYB.ToString("F2")
                    + ",R=" + cart.correlationXY;
            label1.Text = xLabel + "(m:" + cart.xMean.ToString("F1") + ",s':" + cart.xVar.ToString("F1")
                + ")  " + yLabel + "(m:" + cart.yMean.ToString("F1") + ",s':" + cart.yVar.ToString("F1") + ")";
            //添加
            chart.Add(grid);
            chart.Add(linePlot);
            chart.Add(pointPlot);
            //设置属性
            chart.XAxis1.Label = xLabel+":"+xUnit;
            chart.YAxis1.Label = yLabel+":"+yUnit;
            chart.YAxis1.LabelOffsetAbsolute = true;
            chart.Padding = 5;
            chart.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalGuideline());
            chart.AddAxesConstraint(new AxesConstraint.AxisPosition(PlotSurface2D.YAxisPosition.Left, 60));
            chart.PlotBackColor = Color.White;
            chart.BackColor = System.Drawing.SystemColors.Control;
            chart.XAxis1.Color = Color.Black;
            chart.YAxis1.Color = Color.Black;
            chart.Legend = legend;
            chart.LegendZOrder = 1;
            chart.Refresh();
        }
        public Bitmap CreateBitmap()
        {
            Bitmap bi = new Bitmap(this.Width, this.Height - 20);
            Graphics g = Graphics.FromImage(bi);
            g.Clear(Color.White);
            chart.DrawToBitmap(bi, new Rectangle(-10, 0, this.Width - 1, this.Height - 21));
            return bi;
        }
    }
}
