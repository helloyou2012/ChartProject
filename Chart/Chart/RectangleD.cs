using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Stores a set of four double numbers that represent the location and size of
	/// a rectangle. TODO: implement more functionality similar to Drawing.RectangleF.
	/// </summary>
	public struct RectangleD
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RectangleD( double x, double y, double width, double height )
		{
			x_ = x;
			y_ = y;
			width_ = width;
			height_ = height;
		}

		/// <summary>
		/// The rectangle height.
		/// </summary>
		public double Height
		{
			get
			{
				return height_;
			}
			set
			{
				height_ = value;
			}
		}

		/// <summary>
		/// The rectangle width.
		/// </summary>
		public double Width
		{
			get
			{
				return width_;
			}
			set
			{
				width_ = value;
			}
		}

		/// <summary>
		/// The minimum x coordinate of the rectangle.
		/// </summary>
		public double X
		{
			get
			{
				return x_;
			}
			set
			{
				x_ = value;
			}
		}


		/// <summary>
		/// The minimum y coordinate of the rectangle.
		/// </summary>
		public double Y
		{
			get
			{
				return y_;
			}
			set
			{
				y_ = value;
			}
		}


		private double x_;
		private double y_;
		private double width_;
		private double height_;

	}
}
