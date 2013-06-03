using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality relating to markers used by the PointPlot class.
	/// </summary>
	public class Marker
	{

		/// <summary>
		/// Enumeration of all different types of marker.
		/// </summary>
		public enum MarkerType
		{
			/// <summary>
			/// A simple cross marker (x).
			/// </summary>
			Cross1,
			/// <summary>
			/// Another simple cross marker (+).
			/// </summary>
			Cross2,
			/// <summary>
			/// A circle marker.
			/// </summary>
			Circle,
			/// <summary>
			/// A square marker.
			/// </summary>
			Square,
			/// <summary>
			/// A triangle marker (upwards).
			/// </summary>
			Triangle,
			/// <summary>
			/// A triangle marker (upwards).
			/// </summary>
			TriangleUp,
			/// <summary>
			/// A triangle marker (upwards).
			/// </summary>
			TriangleDown,
			/// <summary>
			/// A diamond,
			/// </summary>
			Diamond,
			/// <summary>
			/// A filled circle
			/// </summary>
			FilledCircle,
			/// <summary>
			/// A filled square
			/// </summary>
			FilledSquare,
			/// <summary>
			/// A filled triangle
			/// </summary>
			FilledTriangle,
			/// <summary>
			/// A small flag (up)
			/// </summary>
			Flag,
			/// <summary>
			/// A small flag (up)
			/// </summary>
			FlagUp,
			/// <summary>
			/// A small flag (down)
			/// </summary>
			FlagDown,
			/// <summary>
			/// No marker
			/// </summary>
			None
		}
        public static String[] getTypes()
        {
            String[] types = { "Cross1", "Cross2", "Circle", "Square", "Triangle", "TriangleUp", "TriangleDown", "Diamond", "FilledCircle", "FilledSquare", "FilledTriangle", "Flag", "FlagUp", "FlagDown", "None" };
            return types;
        }

		private MarkerType markerType_;
		private int size_;
		private int h_;
		private System.Drawing.Pen pen_ = new Pen( Color.Black );
		private System.Drawing.Brush brush_ = new SolidBrush( Color.Black );
		private bool filled_ = false;
		private bool dropLine_ = false;


		/// <summary>
		/// The type of marker.
		/// </summary>
		public MarkerType Type
		{
			get
			{
				return markerType_;
			}
			set
			{
				markerType_ = value;
			}
		}


		/// <summary>
		/// Whether or not to draw a dropline.
		/// </summary>
		public bool DropLine
		{
			get
			{
				return dropLine_;
			}
			set
			{
				dropLine_ = value;
			}
		}


        /// <summary>
		/// The marker size.
		/// </summary>
		public int Size
		{
			get
			{
				return size_;
			}
			set
			{
				size_ = value;
				h_ = size_/2;
			}
		}


		/// <summary>
		/// The brush used to fill the marker.
		/// </summary>
		public Brush FillBrush
		{
			get
			{
				return brush_;
			}
			set
			{
				brush_ = value;
			}
		}


		/// <summary>
		/// Fill with color.
		/// </summary>
		public bool Filled
		{
			get
			{
				return filled_;
			}
			set
			{
				filled_ = value;
			}
		}


		/// <summary>
		/// Sets the pen color and fill brush to be solid with the specified color.
		/// </summary>
		public System.Drawing.Color Color
		{
			set
			{
				pen_.Color = value;
				brush_ = new SolidBrush( value );
			}
			get
			{
				return pen_.Color;
			}
		}


		/// <summary>
		/// The Pen used to draw the marker.
		/// </summary>
		public System.Drawing.Pen Pen
		{
			set
			{
				pen_ = value;
			}
			get
			{
				return pen_;
			}
		}


		/// <summary>
		/// Default constructor.
		/// </summary>
		public Marker()
		{
			markerType_ = MarkerType.Square;
			Size = 4;
			filled_ = false;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="markertype">The marker type.</param>
		public Marker( MarkerType markertype )
		{
			markerType_ = markertype;
			Size = 4;
			filled_ = false;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="markertype">The marker type.</param>
		/// <param name="size">The marker size.</param>
		public Marker( MarkerType markertype, int size )
		{
			markerType_ = markertype;
			Size = size;
			filled_ = false;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="markertype">The marker type.</param>
		/// <param name="size">The marker size.</param>
		/// <param name="color">The marker color.</param>
		public Marker( MarkerType markertype, int size, Color color )
		{
			markerType_ = markertype;
			Size = size;
			Color = color;
			filled_ = false;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="markertype">The marker type.</param>
		/// <param name="size">The marker size.</param>
		/// <param name="pen">The marker Pen.</param>
		public Marker( MarkerType markertype, int size, Pen pen )
		{
			markerType_ = markertype;
			Size = size;
			Pen = pen;
			filled_ = false;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="markertype">The marker type.</param>
		/// <param name="size">The marker size.</param>
		/// <param name="pen">The marker Pen.</param>
		/// <param name="fill">The fill flag.</param>
		public Marker( MarkerType markertype, int size, Pen pen, bool fill )
		{
			markerType_ = markertype;
			Size = size;
			Pen = pen;
			filled_ = fill;
		}



		/// <summary>
		/// Draws the marker at the given position
		/// </summary>
		/// <param name="g">The graphics surface on which to draw.</param>
		/// <param name="x">The [physical] x position to draw the marker.</param>
		/// <param name="y">The [physical] y position to draw the marker.</param>
		public void Draw( Graphics g, int x, int y )
		{

			switch (markerType_)
			{

				case MarkerType.Cross1:
					g.DrawLine( pen_, x-h_, y+h_, x+h_, y-h_ );
					g.DrawLine( pen_, x+h_, y+h_, x-h_, y-h_ );
					break;

				case MarkerType.Cross2:
					g.DrawLine( pen_, x, y-h_, x, y+h_ );
					g.DrawLine( pen_, x-h_, y, x+h_, y );
					break;

				case MarkerType.Circle:
					g.DrawEllipse( pen_, x-h_, y-h_, size_, size_ );
					if ( this.filled_ ) 
					{
						g.FillEllipse( brush_, x-h_, y-h_, size_, size_ );
					}
					break;

				case MarkerType.Square:
					g.DrawRectangle( pen_, x-h_, y-h_, size_, size_ );
					if ( this.filled_ ) 
					{
						g.FillRectangle( brush_, x-h_, y-h_, size_, size_ );
					}
					break;

				case MarkerType.Triangle:
				case MarkerType.TriangleDown:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.TriangleUp:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.FilledCircle:
					g.DrawEllipse( pen_, x-h_, y-h_, size_, size_ );
					g.FillEllipse( brush_, x-h_, y-h_, size_, size_ );
					break;

				case MarkerType.FilledSquare:
					g.DrawRectangle( pen_, x-h_, y-h_, size_, size_ );
					g.FillRectangle( brush_, x-h_, y-h_, size_, size_ );
					break;

				case MarkerType.FilledTriangle:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.Diamond:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.Flag:
				case MarkerType.FlagUp:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.FlagDown:
				{
                    g.DrawEllipse(pen_, x - h_, y - h_, size_, size_);
                    if (this.filled_)
                    {
                        g.FillEllipse(brush_, x - h_, y - h_, size_, size_);
                    }
                    break;
				}
				case MarkerType.None:
					break;
			}

		}


	}
}
