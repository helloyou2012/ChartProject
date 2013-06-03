using System;

using System.Collections.Generic;
using System.Windows.Forms;

namespace ChartProject1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new PlotSurface2DDemo());
        }
    }
}