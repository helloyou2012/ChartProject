using System;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Adds additional basic functionality to BasePlot that is common to all
	/// plots that implement the ISequencePlot interface.
	/// </summary>
	/// <remarks>If C# had multiple inheritance, the heirachy would be different. The way it is isn't very nice.</remarks>
	public class BaseSequencePlot : BasePlot, ISequencePlot
	{

		/// <summary>
		/// Gets or sets the data, or column name for the ordinate [y] axis.
		/// </summary>
		public object OrdinateData
		{
			get
			{
				return this.ordinateData_;
			}
			set
			{
				this.ordinateData_ = value;
			}
		}
		private object ordinateData_ = null;


		/// <summary>
		/// Gets or sets the data, or column name for the abscissa [x] axis.
		/// </summary>
		public object AbscissaData
		{
			get
			{
				return this.abscissaData_;
			}
			set
			{
				this.abscissaData_ = value;
			}
		}
		private object abscissaData_ = null;


		/// <summary>
		/// Writes text data of the plot object to the supplied string builder. It is 
		/// possible to specify that only data in the specified range be written.
		/// </summary>
		/// <param name="sb">the StringBuilder object to write to.</param>
		/// <param name="region">a region used if onlyInRegion is true.</param>
		/// <param name="onlyInRegion">If true, only data enclosed in the provided region will be written.</param>
		public void WriteData( System.Text.StringBuilder sb, RectangleD region, bool onlyInRegion )
		{
			SequenceAdapter data_ = 
				new SequenceAdapter( this.DataSource, this.DataMember, this.OrdinateData, this.AbscissaData );

			sb.Append( "Label: " );
			sb.Append( this.Label );
			sb.Append( "\r\n" );
			data_.WriteData( sb, region, onlyInRegion );
		}

	}
}
