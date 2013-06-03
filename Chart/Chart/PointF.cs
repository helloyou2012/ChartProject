using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{
	public struct PointF
	{
		private float cx, cy;

		public static readonly PointF Empty;

		public static PointF operator + (PointF pt, Size sz)
		{
			return new PointF (pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static bool operator == (PointF pt_a, PointF pt_b)
		{
			return ((pt_a.X == pt_b.X) && (pt_a.Y == pt_b.Y));
		}

		public static bool operator != (PointF pt_a, PointF pt_b)
		{
			return ((pt_a.X != pt_b.X) || (pt_a.Y != pt_b.Y));
		}

		public static PointF operator - (PointF pt, Size sz)
		{
			return new PointF (pt.X - sz.Width, pt.Y - sz.Height);
		}
		
		public PointF (float x, float y)
		{
			cx = x;
			cy = y;
		}

		public bool IsEmpty 
		{
			get 
			{
				return ((cx == 0.0) && (cy == 0.0));
			}
		}
		
		public float X 
		{
			get 
			{
				return cx;
			}
			set 
			{
				cx = value;
			}
		}

		public float Y 
		{
			get 
			{
				return cy;
			}
			set 
			{
				cy = value;
			}
		}

		public override bool Equals (object o)
		{
			if (!(o is PointF))
				return false;

			return (this == (PointF) o);
		}

		public override int GetHashCode ()
		{
			return (int) cx ^ (int) cy;
		}

		public override string ToString ()
		{
			return String.Format ("{{X={0}, Y={1}}}", cx, cy);
		}
	}
}
