using System;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Defines an mix-in style interface for plots that use SequenceAdapter to interpret supplied data.
	/// </summary>
	public interface ISequencePlot
	{

		/// <summary>
		/// Gets or sets the source containing a list of values used to populate the plot object.
		/// </summary>
		object DataSource { get; set; }

		/// <summary>
		/// Gets or sets the specific data member in a multimember data source to get data from.
		/// </summary>
		string DataMember { get; set; }

		/// <summary>
		/// Gets or sets the data, or column name for the abscissa [x] axis.
		/// </summary>
		object AbscissaData { get; set; }

		/// <summary>
		/// Gets or sets the data, or column name for the ordinate [y] axis.
		/// </summary>
		object OrdinateData { get; set; }

	}

}
