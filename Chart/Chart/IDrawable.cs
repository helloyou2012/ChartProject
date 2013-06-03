using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{
	/// <summary>
	/// Defines a Draw method for drawing objects against an x and y
	/// Physical Axis.
	/// </summary>
	public interface IDrawable
	{
		/// <summary>
		/// Draws this object against an x and y PhysicalAxis.
		/// </summary>
		/// <param name="g">The graphics surface on which to draw.</param>
		/// <param name="xAxis">The physical x-axis to draw against.</param>
		/// <param name="yAxis">The physical y-axis to draw against.</param>
		void Draw( Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis );
	}
}
