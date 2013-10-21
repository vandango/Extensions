using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class Int32Extensions
	{
		/// <summary>
		/// Checks if a number is a prim number
		/// </summary>
		/// <param name="instance">The number to check.</param>
		/// <returns>Returns a value that indicates if the number is a prim number.</returns>
		public static bool IsPrimeNumber(this int instance)
		{
			if(instance < 2)
			{
				return false;
			}

			if(instance % 2 == 0)
			{
				return false;
			}

			int upperBorder = (int)System.Math.Round(System.Math.Sqrt(instance), 0);

			for(int i = 3; i <= upperBorder; i = i + 2)
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
		public static int NextPrimNumber(this int instance)
		{
			int chk = instance + 1;

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
		public static int PreviousPrimNumber(this int instance)
		{
			int chk = instance - 1;

			while(!chk.IsPrimeNumber())
			{
				chk--;
			}

			return chk;
		}

		/// <summary>
		/// Returns a datetime representation of the integer as years
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a datetime instance representating the current value as years.</returns>
		public static DateTime Years(this int instance)
		{
			return DateTime.MinValue.AddYears(instance - 1);
		}

		/// <summary>
		/// Returns a datetime representation of the integer as months
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a datetime instance representating the current value as months.</returns>
		public static DateTime Months(this int instance)
		{
			return DateTime.MinValue.AddMonths(instance - 1);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as weeks
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as weeks.</returns>
		public static TimeSpan Weeks(this int instance)
		{
			return new TimeSpan((instance * 7), 0, 0, 0);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as days
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as days.</returns>
		public static TimeSpan Days(this int instance)
		{
			return new TimeSpan(instance, 0, 0, 0);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as hours
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as hours.</returns>
		public static TimeSpan Hours(this int instance)
		{
			return new TimeSpan(instance, 0, 0);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as minutes
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as minutes.</returns>
		public static TimeSpan Minutes(this int instance)
		{
			return new TimeSpan(0, instance, 0);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as seconds
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as seconds.</returns>
		public static TimeSpan Seconds(this int instance)
		{
			return new TimeSpan(0, 0, instance);
		}

		/// <summary>
		/// Returns a timespan representation of the integer as milliseconds
		/// </summary>
		/// <param name="instance">The integer instance.</param>
		/// <returns>Returns a timespan instance representating the current value as milliseconds.</returns>
		public static TimeSpan Milliseconds(this int instance)
		{
			return new TimeSpan(0, 0, 0, 0, instance);
		}
	}
}
