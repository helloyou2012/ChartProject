using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality for drawing a horizontal line on a plot surface.
	/// </summary>
	public class HorizontalLine : IPlot
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ordinateValue">ordinate (Y) value of line.</param>
		public HorizontalLine( double ordinateValue )
		{
			this.value_ = ordinateValue;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ordinateValue">ordinate (Y) value of line.</param>
		/// <param name="color">draw the line using this color.</param>
		public HorizontalLine( double ordinateValue, Color color )
		{
			this.value_ = ordinateValue;
			this.pen_ = new Pen( color );
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ordinateValue">ordinate (Y) value of line.</param>
		/// <param name="pen">Pen to use to draw the line.</param>
		public HorizontalLine( double ordinateValue, Pen pen )
		{
			this.value_ = ordinateValue;
			this.pen_ = pen;
		}

		
		/// <summary>
		/// Draws a representation of the horizontal line in the legend.
		/// </summary>
		/// <param name="g">The graphics surface on which to draw</param>
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
		/// Returns null indicating that x extremities of the line are variable.
		/// </summary>
		/// <returns>null</returns>
		public Axis SuggestXAxis()
		{
			return null;
		}


		/// <summary>
		/// Returns a y-axis that is suitable for drawing this plot.
		/// </summary>
		/// <returns>A suitable y-axis.</returns>
		public Axis SuggestYAxis()
		{
			return new LinearAxis( this.value_, this.value_ );
		}

		/// <summary>
		/// Writes text data describing the horizontal line object to the supplied string builder. It is 
		/// possible to specify that the data will be written only if the line is in the specified 
		/// region.
		/// </summary>
		/// <param name="sb">the StringBuilder object to write to.</param>
		/// <param name="region">a region used if onlyInRegion is true.</param>
		/// <param name="onlyInRegion">If true, data will be written only if the line is in the specified region.</param>
		public void WriteData(System.Text.StringBuilder sb, RectangleD region, bool onlyInRegion)
		{

			// return if line is not in plot region and 
			if (value_ > region.Y+region.Height || value_ < region.Y)
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
		/// Draws the horizontal line plot on a GDI+ surface against the provided x and y axes.
		/// </summary>
		/// <param name="g">The GDI+ surface on which to draw.</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public void Draw(System.Drawing.Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis)
		{
			int xMin = xAxis.PhysicalMin.X;
			int xMax = xAxis.PhysicalMax.X;
			
			xMin += pixelIndent_;
			xMax -= pixelIndent_;

			float length = Math.Abs(xMax - xMin);
			float lengthDiff = length - length*scale_;
			float indentAmount = lengthDiff/2;

			xMin += (int)indentAmount;
			xMax -= (int)indentAmount;

			int yPos = (int)yAxis.WorldToPhysical( value_, false ).Y;
		
			g.DrawLine( pen_, xMin, yPos ,xMax, yPos );
            Brush brush=new SolidBrush(Color.SlateBlue);
            g.DrawString(""+(int)value_, new Font(FontFamily.GenericSerif, 8f, FontStyle.Regular), brush, xMax-20, yPos-6);

			// todo:  clip and proper logic for flipped axis min max.
		}

		private double value_;
		/// <summary>
		/// ordinate (Y) value to draw horizontal line at.
		/// </summary>
		public double OrdinateValue
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

		private Pen pen_ = new Pen( Color.Black );
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
