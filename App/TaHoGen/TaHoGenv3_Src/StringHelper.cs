using System;
using System.Globalization;
namespace TaHoGen
{
	/// <summary>
	/// Summary description for StringHelper.
	/// </summary>
	public sealed class StringHelper
	{
		private StringHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Performs a case-insenstive comparison of two strings and returns
		/// true if they match.
		/// </summary>
		/// <param name="Left"></param>
		/// <param name="Right"></param>
		/// <returns></returns>
		public static bool AreEqual(string Left, string Right)
		{
			return string.Compare(Left, Right, true, CultureInfo.InvariantCulture) == 0;
		}
	}
}
