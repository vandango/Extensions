using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class EnumExtension
	{
		/// <summary>
		/// Dictionary
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e"></param>
		/// <returns></returns>
		static public Dictionary<string, object> ToDictionary<T>(this Enum e)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();

			foreach(int s in Enum.GetValues(typeof(T)))
			{
				dict.Add(Enum.GetName(typeof(T), s), s);
			}

			return dict;
		}
	}
}
