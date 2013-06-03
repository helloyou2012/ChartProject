using System;
using System.Collections;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// The bare minimum needed to do world->physical and physical->world transforms for
	/// vertical axes. Also includes tick placements. Built for speed.
	/// </summary>
	/// <remarks>currently unused</remarks>
	public class PageAlignedPhysicalAxis
	{

		private int pMin_;
		private int pMax_;
		private int pLength_;      // cached.

		private double worldMin_;
		private double worldMax_;
		private double worldLength_; // cached.
		

		/// <summary>
		/// Construct from a fully-blown physical axis.
		/// </summary>
		/// <param name="physicalAxis">the physical axis to get initial values from.</param>
		public PageAlignedPhysicalAxis( PhysicalAxis physicalAxis )
		{
			worldMin_ = physicalAxis.Axis.WorldMin;
			worldMax_ = physicalAxis.Axis.WorldMax;
			worldLength_ = worldMax_ - worldMin_;

			if ( physicalAxis.PhysicalMin.X == physicalAxis.PhysicalMax.X )
			{
				pMin_ = physicalAxis.PhysicalMin.Y;
				pMax_ = physicalAxis.PhysicalMax.Y;
			}
			else if ( physicalAxis.PhysicalMin.Y == physicalAxis.PhysicalMax.Y )
			{
				pMin_ = physicalAxis.PhysicalMin.X;
				pMax_ = physicalAxis.PhysicalMax.X;
			}
			else
			{
				throw new NPlotException( "Physical axis is not page aligned" );
			}

			pLength_ = pMax_ - pMin_;

		}

		
		/// <summary>
		/// return the physical coordinate corresponding to the supplied world coordinate.
		/// </summary>
		/// <param name="world">world coordinate to determine physical coordinate for.</param>
		/// <returns>the physical coordinate corresoindng to the supplied world coordinate.</returns>
		public float WorldToPhysical( double world )
		{
			return (float)(((world-worldMin_) / worldLength_) * (float)pLength_ + (float)pMin_);
		}


		/// <summary>
		/// return the physical coordinate corresponding to the supplied world coordinate,
		/// clipped if it is outside the bounds of the axis
		/// </summary>
		/// <param name="world">world coordinate to determine physical coordinate for.</param>
		/// <returns>the physical coordinate corresoindng to the supplied world coordinate.</returns>
		public float WorldToPhysicalClipped( double world )
		{
			if (world > worldMax_)
			{
				return pMax_;
			}
			
			if (world < worldMin_)
			{
				return pMin_;
			}
			
			// is this quicker than returning WorldToPhysical?
			return (float)(((world-worldMin_) / worldLength_) * (float)pLength_ + (float)pMin_);
		}
	

		/// <summary>
		/// return the world coordinate corresponding to the supplied physical coordinate.
		/// </summary>
		/// <param name="physical">physical coordinate to determine world coordinate for.</param>
		/// <returns>the world coordinate corresponding to the supplied </returns>
		public double PhysicalToWorld( float physical )
		{
			return ((float)(physical-pMin_) / (float)pLength_) * worldLength_ + worldMin_;
		}


	}
}
