using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality for drawing data as a series of points.
	/// </summary>
	public class PointPlot : BaseSequencePlot, ISequencePlot, IPlot
	{
		private Marker marker_;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public PointPlot()
		{
			marker_ = new Marker();
		}

		/// <summary>
		/// Constructor for the marker plot.
		/// </summary>
		/// <param name="marker">The marker to use.</param>
		public PointPlot( Marker marker )
		{
			marker_ = marker;
		}


		/// <summary>
		/// Draws the point plot on a GDI+ surface against the provided x and y axes.
		/// </summary>
		/// <param name="g">The GDI+ surface on which to draw.</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public virtual void Draw( Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis )
		{
			SequenceAdapter data_ = 
				new SequenceAdapter( this.DataSource, this.DataMember, this.OrdinateData, this.AbscissaData );

            float leftCutoff_ = xAxis.PhysicalMin.X - marker_.Size;
            float rightCutoff_ = xAxis.PhysicalMax.X + marker_.Size;

			for (int i=0; i<data_.Count; ++i)
			{
				if ( !Double.IsNaN(data_[i].X) && !Double.IsNaN(data_[i].Y) )
				{
					PointF xPos = xAxis.WorldToPhysical( data_[i].X, false);
                    if (xPos.X < leftCutoff_ || rightCutoff_ < xPos.X)
						continue;

					PointF yPos = yAxis.WorldToPhysical( data_[i].Y, false);
					marker_.Draw( g, (int)xPos.X, (int)yPos.Y );
					if (marker_.DropLine)
					{
						PointD yMin = new PointD( data_[i].X, Math.Max( 0.0f, yAxis.Axis.WorldMin ) );
						PointF yStart = yAxis.WorldToPhysical( yMin.Y, false );
						g.DrawLine( marker_.Pen, (int)xPos.X,(int)yStart.Y, (int)xPos.X,(int)yPos.Y );
					}
				}
			}
		}


		/// <summary>
		/// Returns an x-axis that is suitable for drawing this plot.
		/// </summary>
		/// <returns>A suitable x-axis.</returns>
		public Axis SuggestXAxis()
		{
			SequenceAdapter data_ = 
				new SequenceAdapter( this.DataSource, this.DataMember, this.OrdinateData, this.AbscissaData );

			return data_.SuggestXAxis();
		}


		/// <summary>
		/// Returns a y-axis that is suitable for drawing this plot.
		/// </summary>
		/// <returns>A suitable y-axis.</returns>
		public Axis SuggestYAxis()
		{
			SequenceAdapter data_ = 
				new SequenceAdapter( this.DataSource, this.DataMember, this.OrdinateData, this.AbscissaData );

			return data_.SuggestYAxis();
		}


		/// <summary>
		/// Draws a representation of this plot in the legend.
		/// </summary>
		/// <param name="g">The graphics surface on which to draw.</param>
		/// <param name="startEnd">A rectangle specifying the bounds of the area in the legend set aside for drawing.</param>
		public void DrawInLegend( Graphics g, Rectangle startEnd )
		{
			if (marker_.Size > 0)
            {
                marker_.Draw(g, (startEnd.Left + startEnd.Right) / 2, (startEnd.Top + startEnd.Bottom) / 2);
            }
            else if (marker_.Pen.Width > 0)
            {
                g.DrawLine(marker_.Pen,
                    (startEnd.Left + startEnd.Right) / 2, (int)((startEnd.Top + startEnd.Bottom - marker_.Pen.Width) / 2),
                    (startEnd.Left + startEnd.Right) / 2, (int)((startEnd.Top + startEnd.Bottom + marker_.Pen.Width) / 2));
            }
		}


		/// <summary>
		/// The Marker object used for the plot.
		/// </summary>
		public Marker Marker
		{
			set
			{
				marker_ = value;
			}
			get
			{
				return marker_;
			}
		}



	}
}