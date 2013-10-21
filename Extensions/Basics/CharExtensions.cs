using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class CharExtensions
	{
		/// <summary>
		/// Check if a char symbol is in a char array
		/// </summary>
		/// <param name="instance">The char array.</param>
		/// <param name="symbol">The char symbol.</param>
		/// <returns></returns>
		public static bool Contains(this char[] instance, char symbol)
		{
			foreach(char c in instance)
			{
				if(c == symbol)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Converts a char array to a string list
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns></returns>
		public static List<string> ToStringList(this char[] instance)
		{
			return new List<char>(instance).ConvertAll<string>(
				item =>
				{
					return item.ToString();
				}
			);
		}
	}
}
