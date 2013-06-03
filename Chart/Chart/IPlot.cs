using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Defines the interface for objects that (a) can draw a representation of 
	/// themselves in the legend and (b) can recommend a good axis to draw themselves
	/// against.
	/// </summary>
	public interface IPlot : IDrawable
	{	
	
		/// <summary>
		/// Method used to draw a representation of the plot in a legend.
		/// </summary>
		void DrawInLegend( Graphics g, Rectangle startEnd );

		
		/// <summary>
		/// The label associated with the plot [used in legend]
		/// </summary>
		string Label { get; set; }

		
		/// <summary>
		/// Whether or not to include an entry for this plot in the legend if it exists.
		/// </summary>
		bool ShowInLegend { get; set; }


		/// <summary>
		/// The method used to set the default abscissa axis.
		/// </summary>
		Axis SuggestXAxis();

		
		/// <summary>
		/// The method used to set the default ordinate axis.
		/// </summary>
		Axis SuggestYAxis();


		/// <summary>
		/// Write data associated with the plot as text.
		/// </summary>
		/// <param name="sb">the string builder to write to.</param>
		/// <param name="region">Only write out data in this region if onlyInRegion is true.</param>
		/// <param name="onlyInRegion">If true, only data in region is written, else all data is written.</param>
		void WriteData( System.Text.StringBuilder sb, RectangleD region, bool onlyInRegion );

	}
}
