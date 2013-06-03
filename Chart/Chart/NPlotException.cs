using System;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// All exceptions thrown by NPlot are of this type.
	/// </summary>
	public class NPlotException : System.Exception
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public NPlotException( string message, System.Exception innerException )
			: base( message, innerException )
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public NPlotException( string message )
			: base( message )
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public NPlotException()
		{
		}

	}
}
