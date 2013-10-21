using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class Int64Extensions
	{
		/// <summary>
		/// Checks if a number is a prim number
		/// </summary>
		/// <param name="instance">The number to check.</param>
		/// <returns>Returns a value that indicates if the number is a prim number.</returns>
		public static bool IsPrimeNumber(this long instance)
		{
			if(instance < 2)
			{
				return false;
			}

			if(instance % 2 == 0)
			{
				return false;
			}

			long upperBorder = (long)System.Math.Round(System.Math.Sqrt(instance), 0);

			for(long i = 3; i <= upperBorder; i = i + 2)
			{
				if(instance % i == 0)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Get the next prim number
		/// </summary>
		/// <param name="instance">The int instance.</param>
		/// <returns>Returns the next prime number.</returns>
		public static long NextPrimNumber(this long instance)
		{
			long chk = instance + 1;

			while(!chk.IsPrimeNumber())
			{
				chk++;
			}

			return chk;
		}

		/// <summary>
		/// Get the previous prim number
		/// </summary>
		/// <param name="instance">The int instance.</param>
		/// <returns>Returns the previous prime number.</returns>
		public static long PreviousPrimNumber(this long instance)
		{
			long chk = instance - 1;

			while(!chk.IsPrimeNumber())
			{
				chk--;
			}

			return chk;
		}
	}
}
