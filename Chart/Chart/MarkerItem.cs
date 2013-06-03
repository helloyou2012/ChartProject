using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Class for placement of a single marker.
	/// </summary>
	public class MarkerItem : IDrawable
	{

		private Marker marker_; 
		private double x_;
		private double y_; 


		/// <summary>
		/// Constructs a square marker at the (world) point point.
		/// </summary>
		/// <param name="point">the world position at which to place the marker</param>
		public MarkerItem( PointD point )
		{
			marker_ = new Marker( Marker.MarkerType.Square );
			x_ = point.X;
			y_ = point.Y;
		}


		/// <summary>
		/// Default constructor - a square black marker.
		/// </summary>
		/// <param name="x">The world x position of the marker</param>
		/// <param name="y">The world y position of the marker</param>
		public MarkerItem( double x, double y )
		{
			marker_ = new Marker( Marker.MarkerType.Square );
			x_ = x;
			y_ = y;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="marker">The marker to place on the chart.</param>
		/// <param name="x">The world x position of the marker</param>
		/// <param name="y">The world y position of the marker</param>
		public MarkerItem( Marker marker, double x, double y )
		{
			marker_ = marker;
			x_ = x;
			y_ = y;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="marker">The marker to place on the chart.</param>
		/// <param name="point">The world position of the marker</param>
		public MarkerItem( Marker marker, PointD point )
		{
			marker_ = marker;
			x_ = point.X;
			y_ = point.Y;
		}

		/// <summary>
		/// Draws the marker on a plot surface.
		/// </summary>
		/// <param name="g">graphics surface on which to draw</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public void Draw( System.Drawing.Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis )
		{
			PointF point = new PointF( 
				xAxis.WorldToPhysical( x_, true ).X,
				yAxis.WorldToPhysical( y_, true ).Y );

			marker_.Draw( g, (int)point.X, (int)point.Y );
		}
	

	}
}
