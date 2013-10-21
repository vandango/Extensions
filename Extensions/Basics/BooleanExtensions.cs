using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class BooleanExtensions
	{
		/// <summary>
		/// Gets a value that describes a true (1) and a false (0) value
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static int ToInt32(this bool instance)
		{
			return (instance ? 1 : 0);
		}
	}
}
