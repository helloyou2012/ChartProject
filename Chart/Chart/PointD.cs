namespace xuzhenzhen.com.chart
{
	/// <summary>
	/// Represtents a point in two-dimensional space. Used for representation
	/// of points world coordinates.
	/// </summary>
	public struct PointD
	{
		/// <summary>
		/// X-Coordinate of the point.
		/// </summary>
		public double X;

		/// <summary>
		/// Y-Coordinate of the point.
		/// </summary>
		public double Y;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">X-Coordinate of the point.</param>
		/// <param name="y">Y-Coordinate of the point.</param>
		public PointD( double x, double y )
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// returns a string representation of the point.
		/// </summary>
		/// <returns>string representation of the point.</returns>
		public override string ToString()
		{
			return X.ToString() + "\t" + Y.ToString();
		}

	}
}
