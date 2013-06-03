using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Supplies implementation of basic legend handling properties, and
	/// basic data specifying properties which are used by all plots.
	/// </summary>
	/// <remarks>If C# had multiple inheritance, the heirachy would be different.</remarks>
	public abstract class BasePlot 
	{

		/// <summary>
		/// A label to associate with the plot - used in the legend.
		/// </summary>
		public string Label
		{
			get
			{
				return label_;
			}
			set
			{
				this.label_ = value;
			}
		}
		
		private string label_ = "";


		/// <summary>
		/// Whether or not to include an entry for this plot in the legend if it exists.
		/// </summary>
		public bool ShowInLegend
		{
			get
			{
				return showInLegend_;
			}
			set
			{
				this.showInLegend_ = value;
			}
		}
		private bool showInLegend_ = true;


		/// <summary>
		/// Gets or sets the source containing a list of values used to populate the plot object.
		/// </summary>
		public object DataSource
		{
			get
			{
				return this.dataSource_;
			}
			set
			{
				this.dataSource_ = value;
			}
		}
		private object dataSource_ = null;


		/// <summary>
		/// Gets or sets the specific data member in a multimember data source to get data from.
		/// </summary>
		public string DataMember
		{
			get
			{
				return this.dataMember_;
			}
			set
			{
				this.dataMember_ = value;
			}
		}
		private string dataMember_ = null;



	}
}
