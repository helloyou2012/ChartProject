using System;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Encapsulates a Start and Step value. This is useful for specifying a regularly spaced set of 
	/// abscissa values.
	/// </summary>
	public class StartStep
	{

		private double start_;
		private double step_;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="start">the first value of the set of points specified by this object.</param>
		/// <param name="step">the step that specifies the separation between successive points.</param>
		public StartStep( double start, double step )
		{
			this.Start = start;
			this.Step = step;
		}


		/// <summary>
		/// The first value of the set of points specified by this object.
		/// </summary>
		public double Start
		{
			get
			{
				return start_;
			}
			set
			{
				this.start_ = value;
			}
		}


		/// <summary>
		/// The step that specifies the separation between successive points.
		/// </summary>
		public double Step
		{
			get
			{
				return this.step_;
			}
			set
			{
				this.step_ = value;
			}
		}

	}
}
