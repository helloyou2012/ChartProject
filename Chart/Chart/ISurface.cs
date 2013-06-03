using System;
using System.Windows.Forms;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// All PlotSurface's implement this interface.
	/// </summary>
	/// <remarks>Some of the parameter lists will change to be made more uniform.</remarks>
	public interface ISurface
	{

		/// <summary>
		/// Provides functionality for drawing the control.
		/// </summary>
		/// <param name="pe">paint event args</param>
		/// <param name="width">width of the control.</param>
		/// <param name="height">height of the control.</param>
		void DoPaint( PaintEventArgs pe, int width, int height );
		
	
		/// <summary>
		/// Provides functionality for handling mouse up events.
		/// </summary>
		/// <param name="e">mouse event args</param>
		/// <param name="ctr">the control</param>
		void DoMouseUp( MouseEventArgs e, System.Windows.Forms.Control ctr );
		
		
		/// <summary>
		/// Provides functionality for handling mouse move events.
		/// </summary>
		/// <param name="e">mouse event args</param>
		/// <param name="ctr">the control</param>
		void DoMouseMove( MouseEventArgs e, System.Windows.Forms.Control ctr );
		
		
		/// <summary>
		/// Provides functionality for handling mouse down events.
		/// </summary>
		/// <param name="e">mouse event args</param>
		void DoMouseDown( MouseEventArgs e );
	
	}

}
