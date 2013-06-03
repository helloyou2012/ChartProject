using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates functionality for transforming world to physical coordinates optimally.
	/// </summary>
	/// <remarks>The existence of the whole ITransform2D thing might need revising. Not convinced it's the best way.</remarks>
	public class Transform2D
	{

		/// <summary>
		/// Constructs the optimal ITransform2D object for the supplied x and y axes.
		/// </summary>
		/// <param name="xAxis">The xAxis to use for the world to physical transform.</param>
		/// <param name="yAxis">The yAxis to use for the world to physical transform.</param>
		/// <returns>An ITransform2D derived object for converting from world to physical coordinates.</returns>
		public static ITransform2D GetTransformer( PhysicalAxis xAxis, PhysicalAxis yAxis )
		{
			ITransform2D ret = null;

//			if (xAxis.Axis.IsLinear && yAxis.Axis.IsLinear && !xAxis.Axis.Reversed && !yAxis.Axis.Reversed)
//				ret = new FastTransform2D( xAxis, yAxis );
//			else 
//				ret = new DefaultTransform2D( xAxis, yAxis );

			ret = new DefaultTransform2D( xAxis, yAxis );

			return ret;
		}


		/// <summary>
		/// This class does world -> physical transforms for the general case
		/// </summary>
		public class DefaultTransform2D : ITransform2D
		{
			private PhysicalAxis xAxis_;
			private PhysicalAxis yAxis_;

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="xAxis">The x-axis to use for transforms</param>
			/// <param name="yAxis">The y-axis to use for transforms</param>
			public DefaultTransform2D( PhysicalAxis xAxis, PhysicalAxis yAxis )
			{
				xAxis_ = xAxis;
				yAxis_ = yAxis;
			}


			/// <summary>
			/// Transforms the given world point to physical coordinates
			/// </summary>
			/// <param name="x">x coordinate of world point to transform.</param>
			/// <param name="y">y coordinate of world point to transform.</param>
			/// <returns>the corresponding physical point.</returns>
			public Point Transform( double x, double y )
			{
				return new Point(
					(int)xAxis_.WorldToPhysical( x, false ).X,
					(int)yAxis_.WorldToPhysical( y, false ).Y );
			}


			/// <summary>
			/// Transforms the given world point to physical coordinates
			/// </summary>
			/// <param name="worldPoint">the world point to transform</param>
			/// <returns>the corresponding physical point</returns>
			public Point Transform( PointD worldPoint )
			{
				return new Point( 
					(int)xAxis_.WorldToPhysical( worldPoint.X, false ).X,
					(int)yAxis_.WorldToPhysical( worldPoint.Y, false ).Y );
			}

		}
	



		/// <summary>
		/// This class does highly efficient world->physical and physical->world transforms
		/// for linear axes. 
		/// </summary>
		public class FastTransform2D : ITransform2D
		{

			private PageAlignedPhysicalAxis xAxis_;
			private PageAlignedPhysicalAxis yAxis_;


			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="xAxis">The x-axis to use for transforms</param>
			/// <param name="yAxis">The y-axis to use for transforms</param>
			public FastTransform2D( PhysicalAxis xAxis, PhysicalAxis yAxis )
			{
				xAxis_ = new PageAlignedPhysicalAxis( xAxis );
				yAxis_ = new PageAlignedPhysicalAxis( yAxis );
			}


			/// <summary>
			/// Transforms the given world point to physical coordinates
			/// </summary>
			/// <param name="x">x coordinate of world point to transform.</param>
			/// <param name="y">y coordinate of world point to transform.</param>
			/// <returns>the corresponding physical point.</returns>
			public Point Transform( double x, double y )
			{
				return new Point(
					(int)xAxis_.WorldToPhysicalClipped( x ),
					(int)yAxis_.WorldToPhysicalClipped( y ) );
			}


			/// <summary>
			/// Transforms the given world point to physical coordinates
			/// </summary>
			/// <param name="worldPoint">the world point to transform</param>
			/// <returns>the corresponding physical point</returns>
			public Point Transform( PointD worldPoint )
			{
				return new Point( 
					(int)xAxis_.WorldToPhysical( worldPoint.X ),
					(int)yAxis_.WorldToPhysical( worldPoint.Y ) );
			}

		}


	}
}
