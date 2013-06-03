using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using xuzhenzhen.com.chart;

namespace Chart.ChartBase
{
    public partial class TendencyChart : UserControl
    {
        private ArrayList sysList = null;//SYS数据
        private ArrayList diaList = null;//DIA数据
        private ArrayList mapList = null;//MAP数据
        private ArrayList hrList = null;//HeartRate数据
        private ArrayList sys_hrList = null;//RPP数据
        private ArrayList timeList = null;//时间数据（单位小时）

        private VerticalLine vline = null;//垂直线
        private Legend legend = null;//图例
        private Grid grid = null;//网格
        private FilledRegion fill = null;//Sleep区域

        private LinePlot sysPlot = null;//SYS线
        private LinePlot diaPlot = null;//DIA线
        private LinePlot mapPlot = null;//MAP线
        private LinePlot hrPlot = null;//HeartRate线
        private LinePlot sys_hrPlot = null;//RPP线
        //水平线
        private HorizontalLine hline1 = null;
        private HorizontalLine hline2 = null;
        private HorizontalLine hline3 = null;
        private HorizontalLine hline4 = null;

        public TendencyChart()
        {
            InitializeComponent();
            LoadData();
        }
        public TendencyChart(DataTable dt)
        {
            InitializeComponent();
            LoadData(dt);
        }
        public TendencyChart(ArrayList sys, ArrayList dia, ArrayList map, ArrayList hr, ArrayList sys_hr, ArrayList date, ArrayList time)
        {
            InitializeComponent();
            LoadData(sys, dia, map, hr, sys_hr, date, time);
        }
        public void LoadData(DataTable dt)
        {
            sysList = new ArrayList();
            diaList = new ArrayList();
            mapList = new ArrayList();
            hrList = new ArrayList();
            sys_hrList = new ArrayList();
            timeList = new ArrayList();
            DateTime begin = DateTime.Parse(dt.Rows[0]["Date"].ToString());
            foreach (DataRow row in dt.Rows)
            {
                sysList.Add(Convert.ToDouble(row["SYS"].ToString()));
                diaList.Add(Convert.ToDouble(row["DIA"].ToString()));
                mapList.Add(Convert.ToDouble(row["MAP"].ToString()));
                hrList.Add(Convert.ToDouble(row["HeartRate"].ToString()));
                sys_hrList.Add(Convert.ToDouble(row["RPP"].ToString())/100);
                DateTime temp = DateTime.Parse(row["Date"].ToString()+" "+row["Time"].ToString());
                double hours = (temp - begin).TotalHours - 24;
                timeList.Add(hours);
            }
            InitDataToChart();
        }
        public void LoadData(ArrayList sys,ArrayList dia,ArrayList map,ArrayList hr,ArrayList sys_hr,ArrayList date,ArrayList time)
        {
            sysList = sys;
            diaList = dia;
            mapList = map;
            hrList = hr;
            sys_hrList = sys_hr;
            timeList = new ArrayList();
            DateTime begin = DateTime.Parse(date[0].ToString());
            for (int i = 0; i < date.Count; i++)
            {
                DateTime temp = DateTime.Parse(date[i].ToString() + " " + time[i].ToString());
                double hours = (temp - begin).TotalHours - 24;
                timeList.Add(hours);
            }
            InitDataToChart();
        }
        public void LoadData()
        {
            sysList = new ArrayList();
            diaList = new ArrayList();
            mapList = new ArrayList();
            hrList = new ArrayList();
            sys_hrList = new ArrayList();
            timeList = new ArrayList();
            Random random = new Random();
            for (double i = -11; i <= 14; i+=0.3f)
            {
                double dia = random.Next(40, 100);
                double sys = random.Next(30, 50) + dia;
                double map = (sys + 2 * dia) / 3;
                double pr = (float)random.Next(40, 90);
                hrList.Add(pr);
                sys_hrList.Add(sys * pr / 100);
                sysList.Add(sys);
                diaList.Add(dia);
                mapList.Add(map);
                timeList.Add(i);
            }
            InitDataToChart();
        }
        public void InitDataToChart()
        {
            //清空
            chart1.Clear();
            chart2.Clear();
            chart3.Clear();
            //垂直线
            vline = new VerticalLine(0, Color.Blue);
            //图例
            legend = new Legend();
            legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Left);
            legend.VerticalEdgePlacement = Legend.Placement.Inside;
            legend.HorizontalEdgePlacement = Legend.Placement.Outside;
            legend.YOffset = -5;
            legend.BorderStyle = LegendBase.BorderType.Line;
            legend.NumberItemsHorizontally = 4;
            //网格
            grid = new Grid();
            grid.HorizontalGridType = Grid.GridType.Fine;
            grid.VerticalGridType = Grid.GridType.Fine;
            grid.MajorGridPen.Color = Color.Silver;
            grid.MinorGridPen.Color = Color.Silver;
            /////区域着色////////           
            fill = new FilledRegion(new VerticalLine(-1), new VerticalLine(8));
            fill.Brush = new SolidBrush(Color.FromArgb(250, 218, 169));
            /////////////////////////////////////////////
            //直线图
            sysPlot = new LinePlot();
            sysPlot.OrdinateData = sysList;
            sysPlot.AbscissaData = timeList;
            sysPlot.Color = Color.RoyalBlue;
            sysPlot.Pen.Width = 2.0f;
            sysPlot.Label = "SYS";

            diaPlot = new LinePlot();
            diaPlot.OrdinateData = diaList;
            diaPlot.AbscissaData = timeList;
            diaPlot.Color = Color.OrangeRed;
            diaPlot.Pen.Width = 2.0f;
            diaPlot.Label = "DIA";

            mapPlot = new LinePlot();
            mapPlot.OrdinateData = mapList;
            mapPlot.AbscissaData = timeList;
            mapPlot.Color = Color.Chartreuse;
            mapPlot.Pen.Width = 2.0f;
            mapPlot.Label = "MAP";

            hrPlot = new LinePlot();
            hrPlot.OrdinateData = hrList;
            hrPlot.AbscissaData = timeList;
            hrPlot.Color = Color.DarkSlateBlue;
            hrPlot.Pen.Width = 2.0f;

            sys_hrPlot = new LinePlot();
            sys_hrPlot.OrdinateData = sys_hrList;
            sys_hrPlot.AbscissaData = timeList;
            sys_hrPlot.Color = Color.Green;
            sys_hrPlot.Pen.Width = 2.0f;

            //水平线
            hline1 = new HorizontalLine(70, Color.Gray);
            hline2 = new HorizontalLine(90, Color.Gray);
            hline3 = new HorizontalLine(120, Color.Gray);
            hline4 = new HorizontalLine(140, Color.Gray);

            ///////////////////////
            chart1.Add(fill);
            chart1.Add(grid);
            chart1.Add(sysPlot);
            chart1.Add(diaPlot);
            chart1.Add(mapPlot);
            chart1.Add(hline1);
            chart1.Add(hline2);
            chart1.Add(hline3);
            chart1.Add(hline4);
            chart1.Add(vline);
            chart1.XAxis1.HideTickText = true;
            chart1.YAxis1.Label = "血压:mmHg";
            chart1.YAxis1.LabelOffsetAbsolute = true;
            chart1.YAxis1.WorldMin = chart1.YAxis1.WorldMin - 20;
            chart1.YAxis1.WorldMax = chart1.YAxis1.WorldMax + 20;
            chart1.Padding = 5;
            chart1.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            chart1.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalDrag());
            chart1.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalGuideline());
            chart1.AddAxesConstraint(new AxesConstraint.AxisPosition(PlotSurface2D.YAxisPosition.Left, 60));
            chart1.InteractionOccured += new xuzhenzhen.com.chart.Windows.PlotSurface2D.InteractionHandler(chart1_InteractionOccured);
            chart1.PlotBackColor = Color.White;
            chart1.BackColor = System.Drawing.SystemColors.Control;
            chart1.XAxis1.Color = Color.Black;
            chart1.YAxis1.Color = Color.Black;
            chart1.Legend = legend;
            chart1.LegendZOrder = 1;
            chart1.Refresh();
            ///////////////////////////
            chart2.Add(fill);
            chart2.Add(grid);
            chart2.Add(hrPlot);
            chart2.Add(vline);
            chart2.XAxis1.HideTickText = true;
            chart2.YAxis1.Label = "心率:BPM";
            chart2.YAxis1.LabelOffsetAbsolute = true;
            chart2.Padding = 5;
            chart2.AddAxesConstraint(new AxesConstraint.AxisPosition(PlotSurface2D.YAxisPosition.Left, 60));
            chart2.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            chart2.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalDrag());
            chart2.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalGuideline());
            chart2.InteractionOccured += new xuzhenzhen.com.chart.Windows.PlotSurface2D.InteractionHandler(chart2_InteractionOccured);
            chart2.PlotBackColor = Color.White;
            chart2.BackColor = System.Drawing.SystemColors.Control;
            chart2.XAxis1.Color = Color.Black;
            chart2.YAxis1.Color = Color.Black;
            chart2.Refresh();
            ///////////////////////////
            chart3.Add(fill);
            chart3.Add(grid);
            chart3.Add(sys_hrPlot);
            chart3.Add(vline);
            LabelAxis axis = new LabelAxis(chart3.XAxis1);
            int tick = 1;
            if (chart2.XAxis1.WorldMax - chart2.XAxis1.WorldMin > 30)
                tick = 2;
            for (int i = (int)chart2.XAxis1.WorldMin; i <= chart2.XAxis1.WorldMax; i+=tick)
            {
                int j = i % 24;
                if (j < 0) j += 24;
                axis.AddLabel(Convert.ToString(j), i);
            }
            chart3.XAxis1 = axis;
            chart3.XAxis1.Label = "Time:Hour";
            chart3.YAxis1.Label = "SYS*PR/100";
            chart3.YAxis1.LabelOffsetAbsolute = true;
            chart3.Padding = 5;
            chart3.AddAxesConstraint(new AxesConstraint.AxisPosition(PlotSurface2D.YAxisPosition.Left, 60));
            chart3.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            chart3.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalDrag());
            chart3.AddInteraction(new xuzhenzhen.com.chart.Windows.PlotSurface2D.Interactions.VerticalGuideline());
            chart3.InteractionOccured += new xuzhenzhen.com.chart.Windows.PlotSurface2D.InteractionHandler(chart3_InteractionOccured);
            chart3.PlotBackColor = Color.White;
            chart3.BackColor = System.Drawing.SystemColors.Control;
            chart3.XAxis1.Color = Color.Black;
            chart3.YAxis1.Color = Color.Black;
            chart3.Refresh();
            SetStatusNow();
        }
        private void chart1_InteractionOccured(object sender)
        {
            LinearAxis axis1 = new LinearAxis(chart1.XAxis1);
            axis1.Label = "";
            axis1.HideTickText = true;
            this.chart2.XAxis1 = axis1;
            this.chart2.Refresh();
            LabelAxis axis2 = new LabelAxis(chart1.XAxis1);
            int tick = 1;
            if (chart1.XAxis1.WorldMax - chart1.XAxis1.WorldMin > 30)
                tick = 2;
            for (int i = (int)chart1.XAxis1.WorldMin; i <= chart1.XAxis1.WorldMax; i+=tick)
            {
                int j = i % 24;
                if (j < 0) j += 24;
                axis2.AddLabel(Convert.ToString(j), i);
            }
            axis2.Label = "Time:Hour";
            axis2.HideTickText = false;
            chart3.XAxis1 = axis2;
            this.chart3.Refresh();
            SetStatusNow();
        }
        private void chart2_InteractionOccured(object sender)
        {
            LinearAxis axis1 = new LinearAxis(chart2.XAxis1);
            axis1.Label = "";
            axis1.HideTickText = true;
            this.chart1.XAxis1 = axis1;
            this.chart1.Refresh();
            LabelAxis axis2 = new LabelAxis(chart2.XAxis1);
            int tick = 1;
            if (chart2.XAxis1.WorldMax - chart2.XAxis1.WorldMin > 30)
                tick = 2;
            for (int i = (int)chart2.XAxis1.WorldMin; i <= chart2.XAxis1.WorldMax; i+=tick)
            {
                int j = i % 24;
                if (j < 0) j += 24;
                axis2.AddLabel(Convert.ToString(j), i);
            }
            axis2.Label = "Time:Hour";
            axis2.HideTickText = false;
            chart3.XAxis1 = axis2;
            this.chart3.Refresh();
            SetStatusNow();
        }
        private void chart3_InteractionOccured(object sender)
        {
            LinearAxis axis1 = new LinearAxis(chart3.XAxis1);
            axis1.Label = "";
            axis1.HideTickText = true;
            this.chart2.XAxis1 = axis1;
            this.chart2.Refresh();
            LinearAxis axis2 = new LinearAxis(chart3.XAxis1);
            axis2.Label = "";
            axis2.HideTickText = true;
            this.chart1.XAxis1 = axis2;
            this.chart1.Refresh();
            SetStatusNow();
        }
        public void RefreshChart()
        {
            chart1.Refresh();
            chart2.Refresh();
            chart3.Refresh();
        }
        public Bitmap CreateBitmap()
        {
            Bitmap bi = new Bitmap(this.Width-90, this.Height);
            Graphics g = Graphics.FromImage(bi);
            g.Clear(Color.White);
            chart1.DrawToBitmap(bi, new Rectangle(-10, chart1.Top, chart1.Width - 1, chart1.Height - 1));
            chart2.DrawToBitmap(bi, new Rectangle(-10, chart2.Top, chart2.Width - 1, chart2.Height - 1));
            chart3.DrawToBitmap(bi, new Rectangle(-10, chart3.Top, chart3.Width - 1, chart3.Height - 1));
            return bi;
        }
        public string GetTimeNow()
        {
            double t = vline.AbscissaValue;
            DateTime dt = DateTime.Parse("00:00:00");
            dt = dt.AddHours(t);
            return dt.ToString("HH:mm:ss");
        }
        public double GetYValueAt(double x1,double y1,double x2,double y2,double x)
        {
            if ((x2 - x1) > 0)
            {
                return (y2 - y1) * (x - x1) / (x2 - x1) + y1;
            }
            return y1;
        }
        public void SetStatusNow()
        {
            label2.Text = GetTimeNow();
            if (timeList.Count > 1)
            {
                double t = vline.AbscissaValue;
                int flag = -1;
                for (int i = 1; i < timeList.Count; i++)
                {
                    if (t <= (double)timeList[i] && t >= (double)timeList[i - 1])
                    {
                        flag = i-1;
                        break;
                    }
                }
                if (flag>=0)
                {
                    double data= GetYValueAt((double)timeList[flag], (double)sysList[flag], (double)timeList[flag + 1], (double)sysList[flag + 1], t);
                    label4.Text = data.ToString("F1");
                    data = GetYValueAt((double)timeList[flag], (double)diaList[flag], (double)timeList[flag + 1], (double)diaList[flag + 1], t);
                    label6.Text = data.ToString("F1");
                    data = GetYValueAt((double)timeList[flag], (double)mapList[flag], (double)timeList[flag + 1], (double)mapList[flag + 1], t);
                    label8.Text = data.ToString("F1");
                    data = GetYValueAt((double)timeList[flag], (double)hrList[flag], (double)timeList[flag + 1], (double)hrList[flag + 1], t);
                    label10.Text = data.ToString("F1");
                    data = GetYValueAt((double)timeList[flag], (double)sys_hrList[flag], (double)timeList[flag + 1], (double)sys_hrList[flag + 1], t);
                    label12.Text = data.ToString("F1");
                }
            }
        }
    }
}
