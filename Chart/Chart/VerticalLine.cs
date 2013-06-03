using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality for drawing a vertical line on a plot surface.
	/// </summary>
	public class VerticalLine : IPlot
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="abscissaValue">abscissa (X) value of line.</param>
		public VerticalLine( double abscissaValue )
		{
			this.value_ = abscissaValue;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="abscissaValue">abscissa (X) value of line.</param>
		/// <param name="color">draw the line using this color.</param>
		public VerticalLine( double abscissaValue, Color color )
		{
			this.value_ = abscissaValue;
			this.pen_ = new Pen( color );
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="abscissaValue">abscissa (X) value of line.</param>
		/// <param name="pen">Pen to use to draw the line.</param>
		public VerticalLine( double abscissaValue, Pen pen )
		{
			this.value_ = abscissaValue;
			this.pen_ = pen;
		}

		/// <summary>
		/// Draws a representation of the line in the legend
		/// </summary>
		/// <param name="g">The graphics surface on which to draw.</param>
		/// <param name="startEnd">A rectangle specifying the bounds of the area in the legend set aside for drawing.</param>
		public void DrawInLegend(System.Drawing.Graphics g, System.Drawing.Rectangle startEnd)
		{
			g.DrawLine( pen_, startEnd.Left, (startEnd.Top + startEnd.Bottom)/2, 
				startEnd.Right, (startEnd.Top + startEnd.Bottom)/2 );
		}


		/// <summary>
		/// A label to associate with the plot - used in the legend.
		/// </summary>
		public string Label
		{
			get
			{
				return label_;
			}
			set
			{
				this.label_ = value;
			}
		}
		
		private string label_ = "";


		/// <summary>
		/// Whether or not to include an entry for this plot in the legend if it exists.
		/// </summary>
		public bool ShowInLegend
		{
			get
			{
				return showInLegend_;
			}
			set
			{
				this.showInLegend_ = value;
			}
		}
		private bool showInLegend_ = false;

		/// <summary>
		/// Returns an x-axis that is suitable for drawing this plot.
		/// </summary>
		/// <returns>A suitable x-axis.</returns>
		public Axis SuggestXAxis()
		{
			return new LinearAxis( value_, value_ );
		}


		/// <summary>
		/// Returns null indicating that y extremities of the line are variable.
		/// </summary>
		/// <returns>null</returns>
		public Axis SuggestYAxis()
		{
			return null;
		}

		
		/// <summary>
		/// Writes text data describing the vertical line object to the supplied string builder. It is 
		/// possible to specify that the data will be written only if the line is in the specified 
		/// region.
		/// </summary>
		/// <param name="sb">the StringBuilder object to write to.</param>
		/// <param name="region">a region used if onlyInRegion is true.</param>
		/// <param name="onlyInRegion">If true, data will be written only if the line is in the specified region.</param>
		public void WriteData(System.Text.StringBuilder sb, RectangleD region, bool onlyInRegion)
		{

			// return if line is not in plot region and 
			if (value_ > region.X+region.Width || value_ < region.X)
			{
				if (onlyInRegion)
				{
					return;
				}
			}

			sb.Append( "Label: " );
			sb.Append( this.Label );
			sb.Append( "\r\n" );
			sb.Append( value_.ToString() );
			sb.Append( "\r\n" );

		}


		/// <summary>
		/// Draws the vertical line plot on a GDI+ surface against the provided x and y axes.
		/// </summary>
		/// <param name="g">The GDI+ surface on which to draw.</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public void Draw(System.Drawing.Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis)
		{
			int yMin = yAxis.PhysicalMin.Y;
			int yMax = yAxis.PhysicalMax.Y;
			
			yMin -= pixelIndent_;
			yMax += pixelIndent_;

			float length = Math.Abs(yMax - yMin);
			float lengthDiff = length - length*scale_;
			float indentAmount = lengthDiff/2;

			yMin -= (int)indentAmount;
			yMax += (int)indentAmount;

			int xPos = (int)xAxis.WorldToPhysical( value_, false ).X;
		
			g.DrawLine( pen_,  xPos, yMin , xPos, yMax );

			// todo:  clip and proper logic for flipped axis min max.
		}


		/// <summary>
		/// abscissa (X) value to draw horizontal line at.
		/// </summary>
		public double AbscissaValue
		{
			get
			{
				return value_;
			}
			set
			{
				value_ = value;
			}
		}

		/// <summary>
		/// Pen to use to draw the horizontal line.
		/// </summary>
		public Pen Pen
		{
			get
			{
				return pen_;
			}
			set
			{
				pen_ = value;
			}
		}

		
		private double value_;
		private Pen pen_ = new Pen( Color.Black );


		/// <summary>
		/// Each end of the line is indented by this many pixels. 
		/// </summary>
		public int PixelIndent
		{
			get
			{
				return pixelIndent_;
			}
			set
			{
				pixelIndent_ = value;
			}
		}
		private int pixelIndent_ = 0;


		/// <summary>
		/// The line length is multiplied by this amount. Default
		/// corresponds to a value of 1.0.
		/// </summary>
		public float LengthScale
		{
			get
			{
				return scale_;
			}
			set
			{
				scale_ = value;
			}
		}
		private float scale_ = 1.0f;


	}
}
