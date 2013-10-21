using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Clears the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		public static void Clear(this StringBuilder instance)
		{
			instance.Remove(0, instance.Length);
		}
	}
}
