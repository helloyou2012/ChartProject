using System;
using System.Drawing;
using System.Diagnostics;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality for plotting data as a line chart.
	/// </summary>
	public class LinePlot : BaseSequencePlot, IPlot, ISequencePlot
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public LinePlot()
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dataSource">The data source to associate with this plot</param>
		public LinePlot( object dataSource )
		{
			this.DataSource = dataSource;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ordinateData">the ordinate data to associate with this plot.</param>
		/// <param name="abscissaData">the abscissa data to associate with this plot.</param>
		public LinePlot( object ordinateData, object abscissaData )
		{
			this.OrdinateData = ordinateData;
			this.AbscissaData = abscissaData;
		}


		/// <summary>
		/// Draws the line plot on a GDI+ surface against the provided x and y axes.
		/// </summary>
		/// <param name="g">The GDI+ surface on which to draw.</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		/// <param name="drawShadow">If true draw the shadow for the line. If false, draw line.</param>
		public void DrawLineOrShadow( Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis, bool drawShadow )
		{
			Pen shadowPen = null;
			if (drawShadow)
			{
				shadowPen = (Pen)this.Pen.Clone();
				shadowPen.Color = this.ShadowColor;
			}

			SequenceAdapter data = 
				new SequenceAdapter( this.DataSource, this.DataMember, this.OrdinateData, this.AbscissaData );

			ITransform2D t = Transform2D.GetTransformer( xAxis, yAxis );
			
			int numberPoints = data.Count;
			
			if (data.Count == 0)
			{
				return;
			}

			// clipping is now handled assigning a clip region in the
			// graphic object before this call
			if (numberPoints == 1)
			{
				Point physical = t.Transform( data[0] );
				
				if (drawShadow)
				{
					g.DrawLine( shadowPen, 
						(int)(physical.X - 0.5f + this.ShadowOffset.X),
						physical.Y + this.ShadowOffset.Y,
						(int)(physical.X + 0.5f + this.ShadowOffset.X),
						physical.Y + this.ShadowOffset.Y );
				}
				else
				{
					g.DrawLine( Pen, (int)(physical.X-0.5f), physical.Y, (int)(physical.X+0.5f), physical.Y);
				}
			}
			else
			{

                // prepare for clipping
                double leftCutoff = xAxis.PhysicalToWorld(xAxis.PhysicalMin, false);
                double rightCutoff = xAxis.PhysicalToWorld(xAxis.PhysicalMax, false);
				if (leftCutoff > rightCutoff)
				{
					Utils.Swap(ref leftCutoff, ref rightCutoff);
				}
				if (drawShadow)
                {
                    // correct cut-offs
                    double shadowCorrection =
                        xAxis.PhysicalToWorld(ShadowOffset, false) - xAxis.PhysicalToWorld(new Point(0,0), false);
                    leftCutoff -= shadowCorrection;
                    rightCutoff -= shadowCorrection;
                }

                for (int i = 1; i < numberPoints; ++i)
				{
					// check to see if any values null. If so, then continue.
					double dx1 = data[i-1].X;
					double dx2 = data[i].X;
					double dy1 = data[i-1].Y;
					double dy2 = data[i].Y;
					if ( Double.IsNaN(dx1) || Double.IsNaN(dy1) ||
						Double.IsNaN(dx2) || Double.IsNaN(dy2) )
					{
						continue;
					}

                    // do horizontal clipping here, to speed up
                    if ((dx1 < leftCutoff || rightCutoff < dx1) &&
                        (dx2 < leftCutoff || rightCutoff < dx2))
                    {
                        continue;
                    }

					// else draw line.
					Point p1 = t.Transform( data[i-1] );
					Point p2 = t.Transform( data[i] );
                    
                    // when very far zoomed in, points can fall ontop of each other,
                    // and g.DrawLine throws an overflow exception
                    if (p1.Equals(p2))
                        continue;

					if (drawShadow)
					{
						g.DrawLine( shadowPen, 
							p1.X + ShadowOffset.X,
							p1.Y + ShadowOffset.Y,
							p2.X + ShadowOffset.X,
							p2.Y + ShadowOffset.Y );
					}
					else
					{
						g.DrawLine( Pen, p1.X, p1.Y, p2.X, p2.Y );
					}
				}
			}

		}


		/// <summary>
		/// Draws the line plot on a GDI+ surface against the provided x and y axes.
		/// </summary>
		/// <param name="g">The GDI+ surface on which to draw.</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public void Draw( Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis )
		{
			if (this.shadow_)
			{
				this.DrawLineOrShadow( g, xAxis, yAxis, true );
			}

			this.DrawLineOrShadow( g, xAxis, yAxis, false );
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
		/// If true, draw a shadow under the line.
		/// </summary>
		public bool Shadow
		{
			get
			{
				return shadow_;
			}
			set
			{
				shadow_ = value;
			}
		}
		private bool shadow_ = false;
	

		/// <summary>
		/// Color of line shadow if drawn. Use Shadow method to turn shadow on and off.
		/// </summary>
		public Color ShadowColor
		{
			get
			{
				return shadowColor_;
			}
			set
			{
				shadowColor_ = value;
			}
		}
		private Color shadowColor_ = Color.FromArgb(100,100,100);


		/// <summary>
		/// Offset of shadow line from primary line.
		/// </summary>
		public Point ShadowOffset
		{
			get
			{
				return shadowOffset_;
			}
			set
			{
				shadowOffset_ = value;
			}
		}
		private Point shadowOffset_ = new Point( 1, 1 );


        /// <summary>
        /// Draws a representation of this plot in the legend.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw.</param>
        /// <param name="startEnd">A rectangle specifying the bounds of the area in the legend set aside for drawing.</param>
        public virtual void DrawInLegend(Graphics g, Rectangle startEnd)
        {
            g.DrawLine(pen_, startEnd.Left, (startEnd.Top + startEnd.Bottom) / 2,
                startEnd.Right, (startEnd.Top + startEnd.Bottom) / 2);
        }


        /// <summary>
        /// The pen used to draw the plot
        /// </summary>
        public System.Drawing.Pen Pen
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
        private System.Drawing.Pen pen_ = new Pen(Color.Black);


        /// <summary>
        /// The color of the pen used to draw lines in this plot.
        /// </summary>
        public System.Drawing.Color Color
        {
            set
            {
                if (pen_ != null)
                {
                    pen_.Color = value;
                }
                else
                {
                    pen_ = new Pen(value);
                }
            }
            get
            {
                return pen_.Color;
            }
        }

    
    }
}
