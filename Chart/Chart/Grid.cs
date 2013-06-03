using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates a Grid IDrawable object. Instances of this  to a PlotSurface2D 
	/// instance to produce a grid.
	/// </summary>
	public class Grid : IDrawable
	{

		/// <summary>
		/// 
		/// </summary>
		public enum GridType
		{
			/// <summary>
			/// No grid.
			/// </summary>
			None = 0,
			/// <summary>
			/// Coarse grid. Lines at large tick positions only.
			/// </summary>
			Coarse = 1,
			/// <summary>
			/// Fine grid. Lines at both large and small tick positions.
			/// </summary>
			Fine = 2
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		public Grid()
		{
			minorGridPen_ = new Pen( Color.LightGray );
            minorGridPen_.DashStyle = DashStyle.Dash;
			
			majorGridPen_ = new Pen( Color.LightGray );

			horizontalGridType_ = GridType.Coarse;
			
			verticalGridType_ = GridType.Coarse;
		}


		/// <summary>
		/// Specifies the horizontal grid type (none, coarse or fine).
		/// </summary>
		public GridType HorizontalGridType
		{
			get
			{
				return horizontalGridType_;
			}
			set
			{
				horizontalGridType_ = value;
			}
		}
		GridType horizontalGridType_;


		/// <summary>
		/// Specifies the vertical grid type (none, coarse, or fine).
		/// </summary>
		public GridType VerticalGridType
		{
			get
			{
				return verticalGridType_;
			}
			set
			{
				verticalGridType_ = value;
			}
		}
		GridType verticalGridType_;


		/// <summary>
		/// The pen used to draw major (coarse) grid lines.
		/// </summary>
		public System.Drawing.Pen MajorGridPen
		{
			get
			{
				return majorGridPen_;
			}
			set
			{
				majorGridPen_ = value;
			}
		}
		private Pen majorGridPen_;


		/// <summary>
		/// The pen used to draw minor (fine) grid lines.
		/// </summary>
		public System.Drawing.Pen MinorGridPen
		{
			get
			{
				return minorGridPen_;
			}
			set
			{
				minorGridPen_ = value;
			}
		}
		private Pen minorGridPen_;


		/// <summary>
		/// Does all the work in drawing grid lines.
		/// </summary>
		/// <param name="g">The graphics surface on which to render.</param>
		/// <param name="axis">TODO</param>
		/// <param name="orthogonalAxis">TODO</param>
		/// <param name="a">the list of world values to draw grid lines at.</param>
		/// <param name="horizontal">true if want horizontal lines, false otherwise.</param>
		/// <param name="p">the pen to use to draw the grid lines.</param>
		private void DrawGridLines( 
			Graphics g, PhysicalAxis axis, PhysicalAxis orthogonalAxis,
			System.Collections.ArrayList a, bool horizontal, Pen p )
		{
			for (int i=0; i<a.Count; ++i)
			{
				PointF p1 = axis.WorldToPhysical((double)a[i], true);
				PointF p2 = p1;
				Point p3 = orthogonalAxis.PhysicalMax;
				Point p4 = orthogonalAxis.PhysicalMin;
				if (horizontal)
				{
					p1.Y = p4.Y;
					p2.Y = p3.Y;
				}
				else
				{
					p1.X = p4.X;
					p2.X = p3.X;
				}
				// note: casting all drawing was necessary for sane display. why?
				g.DrawLine( p, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y );
			}
		}

		/// <summary>
		/// Draws the grid
		/// </summary>
		/// <param name="g">The graphics surface on which to draw</param>
		/// <param name="xAxis">The physical x axis to draw horizontal lines parallel to.</param>
		/// <param name="yAxis">The physical y axis to draw vertical lines parallel to.</param>
		public void Draw( Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis )
		{

			ArrayList xLargePositions = null;
			ArrayList yLargePositions = null;
			ArrayList xSmallPositions = null;
			ArrayList ySmallPositions = null;

			if (this.horizontalGridType_ != GridType.None)
			{
				xAxis.Axis.WorldTickPositions_FirstPass( xAxis.PhysicalMin, xAxis.PhysicalMax, out xLargePositions, out xSmallPositions );
				DrawGridLines( g, xAxis, yAxis, xLargePositions, true, this.MajorGridPen );	
			}

			if (this.verticalGridType_ != GridType.None)
			{
				yAxis.Axis.WorldTickPositions_FirstPass( yAxis.PhysicalMin, yAxis.PhysicalMax, out yLargePositions, out ySmallPositions );
				DrawGridLines( g, yAxis, xAxis, yLargePositions, false, this.MajorGridPen );
			}


			if (this.horizontalGridType_ == GridType.Fine)
			{
				xAxis.Axis.WorldTickPositions_SecondPass( xAxis.PhysicalMin, xAxis.PhysicalMax, xLargePositions, ref xSmallPositions );
				DrawGridLines( g, xAxis, yAxis, xSmallPositions, true, this.MinorGridPen );
			}

			if (this.verticalGridType_ == GridType.Fine)
			{
				yAxis.Axis.WorldTickPositions_SecondPass( yAxis.PhysicalMin, yAxis.PhysicalMax, yLargePositions, ref ySmallPositions );
				DrawGridLines( g, yAxis, xAxis, ySmallPositions, false, this.MinorGridPen );
			}

		}

	}
}
