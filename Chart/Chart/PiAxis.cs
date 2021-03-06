using System;
using System.Drawing;
using System.Collections;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Axis with labels in multiples of Pi. Maybe needs a better name.
	/// Lots of functionality still to be added - currently only puts labels
	/// at whole increments of pi, want arbitrary increments, automatically
	/// determined and dependance on physical length. 
	/// Volunteers? 
	/// </summary>
	public class PiAxis : Axis
	{

		/// <summary>
		/// Deep copy of PiAxis.
		/// </summary>
		/// <returns>A copy of the LinearAxis Class.</returns>
		public override object Clone()
		{
			PiAxis a = new PiAxis();
			// ensure that this isn't being called on a derived type. If it is, then oh no!
			if (this.GetType() != a.GetType())
			{
				throw new NPlotException( "Error. Clone method is not defined in derived type." );
			}
			DoClone( this, a );
			return a;
		}


		/// <summary>
		/// Helper method for Clone.
		/// </summary>
		/// <param name="a">The original object to clone.</param>
		/// <param name="b">The cloned object.</param>
		protected static void DoClone( PiAxis b, PiAxis a )
		{
			Axis.DoClone( b, a );
		}


		/// <summary>
		/// Initialise PiAxis to default state.
		/// </summary>
		private void Init()
		{
		}


		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="a">The Axis to clone.</param>
		/// <remarks>TODO: [review notes] I don't think this will work as desired.</remarks>
		public PiAxis( Axis a )
			: base( a )
		{
			Init();
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		public PiAxis()
			: base()
		{
			Init();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="worldMin">Minimum world value</param>
		/// <param name="worldMax">Maximum world value</param>
		public PiAxis( double worldMin, double worldMax )
			: base( worldMin, worldMax )
		{
			Init();
		}


		/// <summary>
		/// Given Graphics surface, and physical extents of axis, draw ticks and
		/// associated labels.
		/// </summary>
		/// <param name="g">The GDI+ Graphics surface on which to draw.</param>
		/// <param name="physicalMin">The physical location of the world min point</param>
		/// <param name="physicalMax">The physical location of the world max point</param>
		/// <param name="boundingBox">out: smallest box that completely encompasses all of the ticks and tick labels.</param>
		/// <param name="labelOffset">out: a suitable offset from the axis to draw the axis label.</param>
		protected override void DrawTicks( 
			Graphics g, 
			Point physicalMin, 
			Point physicalMax, 
			out object labelOffset,
			out object boundingBox )
		{
			Point tLabelOffset;
			Rectangle tBoundingBox;

			labelOffset = this.getDefaultLabelOffset( physicalMin, physicalMax );
			boundingBox = null;

			int start = (int)Math.Ceiling( this.WorldMin / Math.PI );
			int end = (int)Math.Floor( this.WorldMax / Math.PI );
			
			// sanity checking.
			if ( end - start < 0 || end - start > 30 )
			{
				return;
			}

			for (int i=start; i<=end; ++i)
			{
				string label = i.ToString() + "Pi";

				if (i == 0)
					label = "0";
				else if (i == 1)
					label = "Pi";

				this.DrawTick( g, i*Math.PI, this.LargeTickSize, 
					label,
					new Point(0,0), 
					physicalMin, physicalMax,
					out tLabelOffset, out tBoundingBox );

				Axis.UpdateOffsetAndBounds( 
					ref labelOffset, ref boundingBox, 
					tLabelOffset, tBoundingBox );
			}

		}


		/// <summary>
		/// Determines the positions, in world coordinates, of the large ticks. 
		/// 
		/// Label axes do not have small ticks.
		/// </summary>
		/// <param name="physicalMin">The physical position corresponding to the world minimum of the axis.</param>
		/// <param name="physicalMax">The physical position corresponding to the world maximum of the axis.</param>
		/// <param name="largeTickPositions">ArrayList containing the positions of the large ticks.</param>
		/// <param name="smallTickPositions">null</param>
		internal override void WorldTickPositions_FirstPass(
			Point physicalMin, 
			Point physicalMax,
			out ArrayList largeTickPositions,
			out ArrayList smallTickPositions
			)
		{
			smallTickPositions = null;
			largeTickPositions = new ArrayList();
	
			int start = (int)Math.Ceiling( this.WorldMin / Math.PI );
			int end = (int)Math.Floor( this.WorldMax / Math.PI );

			// sanity checking.
			if ( end - start < 0 || end - start > 30 )
			{
				return;
			}

			for (int i=start; i<end; ++i)
			{
				largeTickPositions.Add( i*Math.PI ); 
			}

		}


	}
}
