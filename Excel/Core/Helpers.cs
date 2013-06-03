using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Core
{
	/// <summary>
	/// Helpers class
	/// </summary>
	internal static class Helpers
	{
		/// <summary>
		/// Determines whether [is single byte] [the specified encoding].
		/// </summary>
		/// <param name="encoding">The encoding.</param>
		/// <returns>
		/// 	<c>true</c> if [is single byte] [the specified encoding]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSingleByteEncoding(Encoding encoding)
		{
			return encoding.GetChars(new byte[] { 0xc2, 0xb5 }).Length == 1;
		}

		public static double Int64BitsToDouble(long value)
		{
			return BitConverter.ToDouble(BitConverter.GetBytes(value), 0);
		}

	}
}