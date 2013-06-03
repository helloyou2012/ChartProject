using System;
using System.Collections;

namespace Chart.ChartBase
{
    public class Statistics
    {
        public float xMean = 0;
        public float yMean = 0;
        public float xVar = 0;
        public float yVar = 0;
        public float regressXYA = 0;
        public float regressXYB = 0;
        public ArrayList regressY = null;
        public float correlationXY = 0;
        public Statistics(ArrayList x,ArrayList y)
        {
            xMean = mean(x);
            yMean = mean(y);
            //
            xVar = 0;
            yVar = 0;
            for (int i = 0; i < x.Count; i++)
            {
                xVar += ((float)x[i] - xMean) * ((float)x[i] - xMean);
                yVar += ((float)y[i] - yMean) * ((float)y[i] - yMean);
            }
            //
            regressXYA = 0;
            for (int i = 0; i < x.Count; i++)
            {
                regressXYA += ((float)x[i] - xMean) * ((float)y[i] - yMean);
            }
            regressXYA = regressXYA / xVar;
            //
            regressXYB = yMean - xMean * regressXYA;
            //
            regressY = new ArrayList();
            for (int i = 0; i < x.Count; i++)
            {
                regressY.Add(regressXYA * (float)x[i] + regressXYB);
            }
            //
            correlationXY = 0;
            for (int i = 0; i < x.Count; i++)
            {
                correlationXY += ((float)x[i] - xMean) * ((float)y[i] - yMean);
            }
            correlationXY = correlationXY / (float)Math.Sqrt(xVar * yVar);
            xVar = (float)Math.Sqrt(xVar / x.Count);
            yVar = (float)Math.Sqrt(yVar / y.Count);
        }
        public static float mean(float[] x)
        {//平均值
            float m=0;
            int i;
            for(i=0;i<x.Length;i++)
            {
                m+=x[i];
            }
            m=m/x.Length;
            return m;
        }
        public static double mean(double[] x)
        {//平均值
            double m=0;
            int i;
            for(i=0;i<x.Length;i++)
            {
                m+=x[i];
            }
            m=m/x.Length;
            return m;
        }
        public static float mean(ArrayList x)
        {
            float m = 0;
            int i;
            for (i = 0; i < x.Count; i++)
            {
                m += (float)x[i];
            }
            m = m / x.Count;
            return m;
        }
        public static float var(float[] x)
        {//方差
            float v=0;
            float m=mean(x);
            int i;
            for(i=0;i<x.Length;i++)
            {
                v+=(x[i]-m)*(x[i]-m);
            }
            return v;
        }
        public static double var(double[] x)
        {//方差
            double v=0;
            double m=mean(x);
            int i;
            for(i=0;i<x.Length;i++)
            {
                v+=(x[i]-m)*(x[i]-m);
            }
            return v;
        }
        public static float var(ArrayList x)
        {//方差
            float v = 0;
            float m = mean(x);
            int i;
            for (i = 0; i < x.Count; i++)
            {
                v += ((float)x[i] - m) * ((float)x[i] - m);
            }
            return v;
        }
        public static float regressA(float[] x,float[] y)
        {//回归系数y=a*x+b中的a
            float reg=0;
            float mx=mean(x);
            float my=mean(y);
            int i;
            for(i=0;i<x.Length;i++)
            {
                reg+=(x[i]-mx)*(y[i]-my);
            }
            reg=reg/var(x);
            return reg;
        }
        public static float regressA(ArrayList x, ArrayList y)
        {//回归系数y=a*x+b中的a
            float reg = 0;
            float mx = mean(x);
            float my = mean(y);
            int i;
            for (i = 0; i < x.Count; i++)
            {
                reg += ((float)x[i] - mx) * ((float)y[i] - my);
            }
            reg = reg / var(x);
            return reg;
        }
        public static double regressA(double[] x,double[] y)
        {//回归系数y=a*x+b中的a
            double reg=0;
            double mx=mean(x);
            double my=mean(y);
            int i;
            for(i=0;i<x.Length;i++)
            {
                reg+=(x[i]-mx)*(y[i]-my);
            }
            reg=reg/var(x);
            return reg;
        }
        public static float regressB(float[] x,float[] y)
        {//回归系数y=a*x+b中的b
            return (mean(y)-mean(x)*regressA(x,y));
        }
        public static double regressB(double[] x,double[] y,int size)
        {//回归系数y=a*x+b中的b
            return (mean(y)-mean(x)*regressA(x,y));
        }
        public static float regressB(ArrayList x, ArrayList y)
        {//回归系数y=a*x+b中的b
            return (mean(y) - mean(x) * regressA(x, y));
        }
        public static float[] regress(float[] x, float[] y)
        {
            float[] y0=new float[x.Length];
            float a = regressA(x, y);
            float b = mean(y) - mean(x) * a;
            for (int i = 0; i < x.Length; i++)
            {
                y0[i] = a * x[i] + b;
            }
            return y0;
        }
        public static double[] regress(double[] x, double[] y)
        {
            double[] y0 = new double[x.Length];
            double a = regressA(x, y);
            double b = mean(y) - mean(x) * a;
            for (int i = 0; i < x.Length; i++)
            {
                y0[i] = a * x[i] + b;
            }
            return y0;
        }
        public static ArrayList regress(ArrayList x, ArrayList y)
        {
            ArrayList y0 = new ArrayList();
            float a = regressA(x, y);
            float b = mean(y) - mean(x) * a;
            for (int i = 0; i < x.Count; i++)
            {
                y0.Add(a * (float)x[i] + b);
            }
            return y0;
        }
        public static float correlation(float[] x, float[] y)
        {//相关系数
            float reg = 0;
            float mx = mean(x);
            float my = mean(y);
            int i;
            for (i = 0; i < x.Length; i++)
            {
                reg += (x[i] - mx) * (y[i] - my);
            }
            reg = reg / (float)Math.Sqrt(var(x)*var(y));
            return reg;
        }
        public static double correlation(double[] x, double[] y)
        {//相关系数
            double reg = 0;
            double mx = mean(x);
            double my = mean(y);
            int i;
            for (i = 0; i < x.Length; i++)
            {
                reg += (x[i] - mx) * (y[i] - my);
            }
            reg = reg / Math.Sqrt(var(x) * var(y));
            return reg;
        }
        public static float correlation(ArrayList x, ArrayList y)
        {//相关系数
            float reg = 0;
            float mx = mean(x);
            float my = mean(y);
            int i;
            for (i = 0; i < x.Count; i++)
            {
                reg += ((float)x[i] - mx) * ((float)y[i] - my);
            }
            reg = reg / (float)Math.Sqrt(var(x) * var(y));
            return reg;
        }
    }
}
