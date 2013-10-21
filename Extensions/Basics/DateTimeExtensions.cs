using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Returns the very end of the given day 
		/// (the last millisecond of the last hour for the given <see cref="DateTime"/>).
		/// </summary>
		/// <param name="date">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime EndOfDay(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
		}

		/// <summary>
		/// Returns the Start of the given day 
		/// (the first millisecond of the given <see cref="DateTime"/>).
		/// </summary>
		/// <param name="date">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime BeginningOfDay(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
		}

		/// <summary>
		/// Returns the same date (same Day, Month, Hour, Minute, Second etc) 
		/// in the next calendar year.
		/// If that day does not exist in next year in same month, 
		/// number of missing days is added to the last day in same month next year.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime NextYear(this DateTime start)
		{
			var nextYear = start.Year + 1;
			var numberOfDaysInSameMonthNextYear = DateTime.DaysInMonth(nextYear, start.Month);

			if(numberOfDaysInSameMonthNextYear < start.Day)
			{
				var differenceInDays = start.Day - numberOfDaysInSameMonthNextYear;
				var dateTime = new DateTime(nextYear, start.Month, numberOfDaysInSameMonthNextYear, start.Hour, start.Minute, start.Second, start.Millisecond);
				return dateTime + differenceInDays.Days();
			}

			return new DateTime(nextYear, start.Month, start.Day, start.Hour, start.Minute, start.Second, start.Millisecond);
		}

		/// <summary>
		/// Returns the same date (same Day, Month, Hour, Minute, Second etc) 
		/// in the previous calendar year.
		/// If that day does not exist in previous year in same month, 
		/// number of missing days is added to the last day in same month previous year.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime PreviousYear(this DateTime start)
		{
			var previousYear = start.Year - 1;
			var numberOfDaysInSameMonthPreviousYear = DateTime.DaysInMonth(previousYear, start.Month);

			if(numberOfDaysInSameMonthPreviousYear < start.Day)
			{
				var differenceInDays = start.Day - numberOfDaysInSameMonthPreviousYear;
				var dateTime = new DateTime(previousYear, start.Month, numberOfDaysInSameMonthPreviousYear, start.Hour, start.Minute, start.Second, start.Millisecond);
				return dateTime + differenceInDays.Days();
			}

			return new DateTime(previousYear, start.Month, start.Day, start.Hour, start.Minute, start.Second, start.Millisecond);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> increased by 24 hours ie Next Day.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime NextDay(this DateTime start)
		{
			return start + 1.Days();
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> decreased by 24h period ie Previous Day.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime PreviousDay(this DateTime start)
		{
			return start - 1.Days();
		}

		/// <summary>
		/// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="day">The target day of week.</param>
		public static DateTime Next(this DateTime start, DayOfWeek day)
		{
			do
			{
				start = start.NextDay();
			}
			while(start.DayOfWeek != day);

			return start;
		}

		/// <summary>
		/// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="day">The target day of week.</param>
		public static DateTime Previous(this DateTime start, DayOfWeek day)
		{
			do
			{
				start = start.PreviousDay();
			}
			while(start.DayOfWeek != day);

			return start;
		}

		/// <summary>
		/// Increases supplied <see cref="DateTime"/> for 7 days ie returns the Next Week.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime WeekAfter(this DateTime start)
		{
			return start + 1.Weeks();
		}

		/// <summary>
		/// Decreases supplied <see cref="DateTime"/> for 7 days ie returns the Previous Week.
		/// </summary>
		/// <param name="start">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime WeekEarlier(this DateTime start)
		{
			return start - 1.Weeks();
		}

		/// <summary>
		/// Increases the <see cref="DateTime"/> object with given <see cref="TimeSpan"/> value.
		/// </summary>
		/// <param name="startDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="toAdd">The timespan to add on a date.</param>
		public static DateTime IncreaseTime(this DateTime startDate, TimeSpan toAdd)
		{
			return startDate + toAdd;
		}

		/// <summary>
		/// Decreases the <see cref="DateTime"/> object with given <see cref="TimeSpan"/> value.
		/// </summary>
		/// <param name="startDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="toSubtract">The timespan to suvstract on a date.</param>
		public static DateTime DecreaseTime(this DateTime startDate, TimeSpan toSubtract)
		{
			return startDate - toSubtract;
		}

		/// <summary>
		/// Returns the original <see cref="DateTime"/> with Hour part changed to supplied hour parameter.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="hour">The hour to set on a date.</param>
		public static DateTime SetTime(this DateTime originalDate, int hour)
		{
			return new DateTime(
				originalDate.Year, 
				originalDate.Month, 
				originalDate.Day, 
				hour, 
				originalDate.Minute, 
				originalDate.Second, 
				originalDate.Millisecond
			);
		}

		/// <summary>
		/// Returns the original <see cref="DateTime"/> with Hour and 
		/// Minute parts changed to supplied hour and minute parameters.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="hour">The hour to set on a date.</param>
		/// <param name="minute">The minute to set on a date.</param>
		public static DateTime SetTime(this DateTime originalDate, int hour, int minute)
		{
			return new DateTime(
				originalDate.Year, 
				originalDate.Month, 
				originalDate.Day, 
				hour, 
				minute, 
				originalDate.Second, 
				originalDate.Millisecond
			);
		}

		/// <summary>
		/// Returns the original <see cref="DateTime"/> with Hour, 
		/// Minute and Second parts changed to supplied hour, minute 
		/// and second parameters.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="hour">The hour to set on a date.</param>
		/// <param name="minute">The minute to set on a date.</param>
		/// <param name="second">The second to set on a date.</param>
		public static DateTime SetTime(this DateTime originalDate, int hour, int minute, int second)
		{
			return new DateTime(
				originalDate.Year, 
				originalDate.Month, 
				originalDate.Day, 
				hour, 
				minute, 
				second, 
				originalDate.Millisecond
			);
		}

		/// <summary>
		/// Returns the original <see cref="DateTime"/> with Hour, 
		/// Minute, Second and Millisecond parts changed to supplied hour, 
		/// minute, second and millisecond parameters.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="hour">The hour to set on a date.</param>
		/// <param name="minute">The minute to set on a date.</param>
		/// <param name="second">The second to set on a date.</param>
		/// <param name="millisecond">The millisecond to set on a date.</param>
		public static DateTime SetTime(this DateTime originalDate, int hour, int minute, int second, int millisecond)
		{
			return new DateTime(
				originalDate.Year, 
				originalDate.Month, 
				originalDate.Day, 
				hour, 
				minute, 
				second, 
				millisecond
			);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Hour part.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="hour">The hour to set on a date.</param>
		public static DateTime SetHour(this DateTime originalDate, int hour)
		{
			return originalDate.SetTime(hour);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Minute part.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="minute">The minute to set on a date.</param>
		public static DateTime SetMinute(this DateTime originalDate, int minute)
		{
			return originalDate.SetTime(originalDate.Hour, minute);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Second part.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="second">The second to set on a date.</param>
		public static DateTime SetSecond(this DateTime originalDate, int second)
		{
			return originalDate.SetTime(originalDate.Hour, originalDate.Minute, second);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Millisecond part.
		/// </summary>
		/// <param name="originalDate">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="millisecond">The millisecond to set on a date.</param>
		public static DateTime SetMillisecond(this DateTime originalDate, int millisecond)
		{
			return originalDate.SetTime(
				originalDate.Hour, 
				originalDate.Minute,
				originalDate.Second, 
				millisecond
			);
		}

		/// <summary>
		/// Returns original <see cref="DateTime"/> value with time part set to 
		/// midnight (alias for <see cref="BeginningOfDay"/> method).
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		public static DateTime Midnight(this DateTime value)
		{
			return value.BeginningOfDay();
		}

		/// <summary>
		/// Returns original <see cref="DateTime"/> value with time part set to Noon (12:00:00h).
		/// </summary>
		/// <param name="value">The <see cref="DateTime"/> find Noon for.</param>
		/// <returns>A <see cref="DateTime"/> value with time part set to Noon (12:00:00h).</returns>
		public static DateTime Noon(this DateTime value)
		{
			return value.SetTime(12, 0, 0, 0);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Year part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="year">The year to set on a date.</param>
		public static DateTime SetDate(this DateTime value, int year)
		{
			return new DateTime(
				year, 
				value.Month, 
				value.Day, 
				value.Hour, 
				value.Minute, 
				value.Second, 
				value.Millisecond
			);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Year and Month part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="year">The year to set on a date.</param>
		/// <param name="month">The month to set on a date.</param>
		public static DateTime SetDate(this DateTime value, int year, int month)
		{
			return new DateTime(
				year, 
				month, 
				value.Day, 
				value.Hour, 
				value.Minute, 
				value.Second, 
				value.Millisecond
			);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Year, Month and Day part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="year">The year to set on a date.</param>
		/// <param name="month">The month to set on a date.</param>
		/// <param name="day">The day to set on a date.</param>
		public static DateTime SetDate(this DateTime value, int year, int month, int day)
		{
			return new DateTime(
				year,
				month,
				day,
				value.Hour,
				value.Minute,
				value.Second,
				value.Millisecond
			);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Year part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="year">The year to set on a date.</param>
		public static DateTime SetYear(this DateTime value, int year)
		{
			return value.SetDate(year);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Month part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="month">The month to set on a date.</param>
		public static DateTime SetMonth(this DateTime value, int month)
		{
			return value.SetDate(value.Year, month);
		}

		/// <summary>
		/// Returns <see cref="DateTime"/> with changed Day part.
		/// </summary>
		/// <param name="value">The instance of the <see cref="DateTime"/> object.</param>
		/// <param name="day">The day to set on a date.</param>
		public static DateTime SetDay(this DateTime value, int day)
		{
			return value.SetDate(value.Year, value.Month, day);
		}

		/// <summary>
		/// Determines whether the specified <see cref="DateTime"/> is before then current value.
		/// </summary>
		/// <param name="current">The current value.</param>
		/// <param name="toCompareWith">Value to compare with.</param>
		/// <returns>
		/// <c>true</c> if the specified current is before; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsBefore(this DateTime current, DateTime toCompareWith)
		{
			return current < toCompareWith;
		}

		/// <summary>
		/// Determines whether the specified <see cref="DateTime"/> value is After then current value.
		/// </summary>
		/// <param name="current">The current value.</param>
		/// <param name="toCompareWith">Value to compare with.</param>
		/// <returns>
		/// <c>true</c> if the specified current is after; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsAfter(this DateTime current, DateTime toCompareWith)
		{
			return current > toCompareWith;
		}

		/// <summary>
		/// Returns the given <see cref="DateTime"/> with hour and minutes set At given values.
		/// </summary>
		/// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
		/// <param name="hour">The hour to set time to.</param>
		/// <param name="minute">The minute to set time to.</param>
		/// <returns><see cref="DateTime"/> with hour and minute set to given values.</returns>
		public static DateTime At(this DateTime current, int hour, int minute)
		{
			return current.SetTime(hour, minute);
		}

		/// <summary>
		/// Returns the given <see cref="DateTime"/> with hour and minutes and seconds set At given values.
		/// </summary>
		/// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
		/// <param name="hour">The hour to set time to.</param>
		/// <param name="minute">The minute to set time to.</param>
		/// <param name="second">The second to set time to.</param>
		/// <returns><see cref="DateTime"/> with hour and minutes and seconds set to given values.</returns>
		public static DateTime At(this DateTime current, int hour, int minute, int second)
		{
			return current.SetTime(hour, minute, second);
		}

		/// <summary>
		/// Sets the day of the <see cref="DateTime"/> to the first day in that month.
		/// </summary>
		/// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
		/// <returns>given <see cref="DateTime"/> with the day part set to the first day in that month.</returns>
		public static DateTime FirstDayOfMonth(this DateTime current)
		{
			return current.SetDay(1);
		}

		/// <summary>
		/// Sets the day of the <see cref="DateTime"/> to the last day in that month.
		/// </summary>
		/// <param name="current">The current DateTime to be changed.</param>
		/// <returns>given <see cref="DateTime"/> with the day part set to the last day in that month.</returns>
		public static DateTime LastDayOfMonth(this DateTime current)
		{
			return current.SetDay(DateTime.DaysInMonth(current.Year, current.Month));
		}

		/// <summary>
		/// Adds the given number of business days to the <see cref="DateTime"/>.
		/// </summary>
		/// <param name="current">The date to be changed.</param>
		/// <param name="days">Number of business days to be added.</param>
		/// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
		public static DateTime AddBusinessDays(this DateTime current, int days)
		{
			var sign = Math.Sign(days);
			var unsignedDays = Math.Abs(days);
			for(var i = 0; i < unsignedDays; i++)
			{
				do
				{
					current = current.AddDays(sign);
				}
				while(current.DayOfWeek == DayOfWeek.Saturday ||
				current.DayOfWeek == DayOfWeek.Sunday);
			}
			return current;
		}

		/// <summary>
		/// Subtracts the given number of business days to the <see cref="DateTime"/>.
		/// </summary>
		/// <param name="current">The date to be changed.</param>
		/// <param name="days">Number of business days to be subtracted.</param>
		/// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
		public static DateTime SubtractBusinessDays(this DateTime current, int days)
		{
			return AddBusinessDays(current, -days);
		}

		/// <summary>
		/// Determine if a <see cref="DateTime"/> is in the future.
		/// </summary>
		/// <param name="dateTime">The date to be checked.</param>
		/// <returns><c>true</c> if <paramref name="dateTime"/> is in the future; otherwise <c>false</c>.</returns>
		public static bool IsInFuture(this DateTime dateTime)
		{
			return dateTime > DateTime.Now;
		}

		/// <summary>
		/// Determine if a <see cref="DateTime"/> is in the past.
		/// </summary>
		/// <param name="dateTime">The date to be checked.</param>
		/// <returns><c>true</c> if <paramref name="dateTime"/> is in the past; otherwise <c>false</c>.</returns>
		public static bool IsInPast(this DateTime dateTime)
		{
			return dateTime < DateTime.Now;
		}
	}
}
