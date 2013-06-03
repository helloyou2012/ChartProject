using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Defines a gradient.
	/// </summary>
	public interface IGradient
	{

		/// <summary>
		/// Gets a color corresponding to a number between 0.0 and 1.0 inclusive.
		/// </summary>
		/// <param name="prop">the number to get corresponding color for (between 0.0 and 1.0)</param>
		/// <returns>The color corresponding to the supplied number.</returns>
		Color GetColor( double prop );

	}
}
