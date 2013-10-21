using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class StringExtensions
	{
		#region Validation

		/// <summary>
		/// Check an email adress for its correctness
		/// </summary>
		/// <param name="instance">A email adress to check.</param>
		/// <exception cref="ArgumentNullException">If inputEmail is null.</exception>
		/// <returns>A value that indicates if the string is a real email adress or not.</returns>
		public static bool IsEmail(this string instance)
		{
			if(instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}"
				+ @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\"
				+ @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

			Regex pattern = new Regex(strRegex);

			if(pattern.IsMatch(instance))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether [is null or empty] [the specified instance].
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns a value that indicates if this string is null, empty or trimmed empty.</returns>
		public static bool IsNullOrWhiteSpace(this string instance)
		{
			// use own implementation because of .net version compatibility
			if(instance != null)
			{
				for(int i = 0; i < instance.Length; i++)
				{
					if(instance[i] != ' ')
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Check if a string is a number
		/// </summary>
		/// <param name="instance">The string itself.</param>
		/// <exception cref="ArgumentNullException">If value is null.</exception>
		/// <returns>Returns a value that indicates if this string is a numeric string.</returns>
		public static bool IsNumeric(this string instance)
		{
			if(instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			Regex pattern = new Regex(@"^[0-9,.]+$");
			return pattern.IsMatch(instance.Trim());
		}

		/// <summary>
		/// Check if a expression is a value that indicates a boolean value,
		/// like "true", or "1".
		/// </summary>
		/// <param name="instance">The string to check</param>
		/// <returns>A value that indicates if the expression fo the string is true or false.</returns>
		public static bool IsExpressionTrue(this string instance)
		{
			// null is false
			if(instance == null)
			{
				return false;
			}

			// the string should be trimmed, the value " true " should also be true
			string expression = instance.Trim().ToLower(CultureInfo.CurrentCulture);

			// first check if the string is empty
			if(expression.IsNullOrWhiteSpace())
			{
				return false;
			}

			// check if the string is a number, positive numbers are always a true boolean
			if(expression.IsNumeric())
			{
				// check the number for a number decimal digits seperator
				// and if the has a seperator, convert the string as double
				// otherwise convert it as integer
				if(expression.Contains(",")
				|| expression.Contains("."))
				{
					double expD = expression.ToDouble();

					if(expD > 0)
					{
						return true;
					}
				}
				else
				{
					int expI = expression.ToInt32();

					if(expI > 0)
					{
						return true;
					}
				}
			}
			else
			{
				// contains a list of expressions that are all values for "true"
				List<string> trueExpressions = new List<string>() {
					"1", "true", "wahr", "richtig", "korrekt",
					"valid", "correct", "accurate", "proper",
					"respectable", "positiv", "positive",
					"wow", "100pro", "100%", "yes", "ja",
					"si", "ok", "legal", "legitime", "legitim"
				};

				// contains a list of expressions that are all values for "false"
				List<string> falseExpressions = new List<string>() {
					"0", "false", "unwahr", "nicht wahr", "falsch", "inkorrekt",
					"incorrect", "inaccurate", "illegitimate", "fake",
					"invalid", "negativ", "negative", "nein",
					"no", "non", "not", "illegitime", "illegitim"
				};

				// check the string for this values
				if(trueExpressions.Contains(expression))
				{
					return true;
				}
				else if(falseExpressions.Contains(expression))
				{
					return false;
				}
			}

			return false;
		}

		#endregion

		#region Safe Convert

		/// <summary>
		/// Converts a string into a specific type
		/// </summary>
		/// <typeparam name="T">The specific target type.</typeparam>
		/// <param name="instance">The string itself.</param>
		/// <returns>Returns a new object of the specific type with the value parsed from the given string.</returns>
		public static T ToType<T>(this string instance)
		{
			// compare the type of the string instance with
			// the given type T and use the specific string.ToXYZ
			// extension method
			if(typeof(T) == typeof(bool))
			{
				return (T)(object)instance.ToBoolean();
			}
			else if(typeof(T) == typeof(short))
			{
				return (T)(object)instance.ToInt16();
			}
			else if(typeof(T) == typeof(int))
			{
				return (T)(object)instance.ToInt32();
			}
			else if(typeof(T) == typeof(long))
			{
				return (T)(object)instance.ToInt64();
			}
			else if(typeof(T) == typeof(byte[]))
			{
				return (T)(object)instance.ToByteArray();
			}
			else if(typeof(T) == typeof(DateTime))
			{
				return (T)(object)instance.ToDateTime();
			}
			else if(typeof(T) == typeof(decimal))
			{
				return (T)(object)instance.ToDecimal();
			}
			else if(typeof(T) == typeof(double))
			{
				return (T)(object)instance.ToDouble();
			}
			else if(typeof(T) == typeof(float))
			{
				return (T)(object)instance.ToFloat();
			}
			else
			{
				return (T)(object)instance;
			}
		}

		/// <summary>
		/// Converts a string value on a save way into a enum value
		/// </summary>
		/// <typeparam name="T">The target enum type.</typeparam>
		/// <param name="instance">The string itself.</param>
		/// <returns>Returns a enum type of the given string.</returns>
		public static T ToEnum<T>(this string instance)
		{
			if(instance == null)
			{
				throw new ArgumentNullException("instance", "The parameter [value] cannot be null.");
			}

			try
			{
				return (T)Enum.Parse(typeof(T), instance);
			}
			catch(Exception)
			{
				return default(T);
			}
		}

		/// <summary>
		/// Converts a string value on a save way into a byte array value
		/// </summary>
		/// <param name="instance">The string itself.</param>
		/// <returns>Returns the string converted as a byte array.</returns>
		public static byte[] ToAsciiByteArray(this string instance)
		{
			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
			return enc.GetBytes(instance);
		}

		/// <summary>
		/// Converts a string value into a byte array.
		/// </summary>
		/// <returns>The byte array.</returns>
		/// <param name="instance">Instance.</param>
		public static byte[] ToByteArray(this string instance)
		{
			byte[] bytes = new byte[instance.Length * sizeof(char)];
			System.Buffer.BlockCopy(instance.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		/// <summary>
		/// Converts a string value on a save way into a int16 value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a short (Int16) object.</returns>
		public static short ToInt16(this string instance)
		{
			short result = 0;

			if(short.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(short);
			}
		}

		/// <summary>
		/// Converts a string value on a save way into a int32 value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a int (Int32) object.</returns>
		public static int ToInt32(this string instance)
		{
			int result = 0;

			if(int.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(int);
			}
		}

		/// <summary>
		/// Convert a string value on a save way into a int64 value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a long (Int64) object.</returns>
		public static long ToInt64(this string instance)
		{
			long result = 0;

			if(long.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(long);
			}
		}

		/// <summary>
		/// Convert a string value on a save way into a double value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a double object.</returns>
		public static double ToDouble(this string instance)
		{
			double result = 0;

			if(double.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(double);
			}
		}

		/// <summary>
		/// Convert a string value on a save way into a decimal value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a decimal object.</returns>
		public static decimal ToDecimal(this string instance)
		{
			decimal result = 0;

			if(decimal.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(decimal);
			}
		}

		/// <summary>
		/// Convert a string value on a save way into a float value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a float object.</returns>
		public static float ToFloat(this string instance)
		{
			float result = 0;

			if(float.TryParse(instance, out result))
			{
				return result;
			}
			else
			{
				return default(float);
			}
		}

		/// <summary>
		/// Convert a string value on a save way into a boolean value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a boolean object.</returns>
		public static bool ToBoolean(this string instance)
		{
			bool result = false;

			// if the string equals the values "true" or "false"
			// convert it using the Boolean.Parse method, otherwise
			// use the string.IsExpressionTrue extension method
			if((instance.Equals("True", StringComparison.OrdinalIgnoreCase)
			|| instance.Equals("False", StringComparison.OrdinalIgnoreCase))
			&& bool.TryParse(instance, out result))
			{
				return result;
			}

			return instance.IsExpressionTrue();
		}

		/// <summary>
		/// Convert a string value on a save way into a DateTime value
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string converted as a datetime object.</returns>
		public static DateTime ToDateTime(this string instance)
		{
			DateTime result = default(DateTime);

			if(DateTime.TryParse(
				instance,
				CultureInfo.CurrentCulture.DateTimeFormat,
				DateTimeStyles.AllowWhiteSpaces,
				out result
			))
			{
				return result;
			}

			return result;
		}

		#endregion

		#region Format

		/// <summary>
		/// Formats the given string using the string.Format() method
		/// </summary>
		/// <param name="format">The format pattern.</param>
		/// <param name="arg0">The first argument for the string.</param>
		/// <returns>A string that is formated using the string.Format() method.</returns>
		public static string FormatIt(string format, object arg0)
		{
			return string.Format(format, arg0);
		}

		/// <summary>
		/// Formats the given string using the string.Format() method
		/// </summary>
		/// <param name="format">The format pattern.</param>
		/// <param name="arg0">The first argument for the string.</param>
		/// <param name="arg1">The second argument for the string.</param>
		/// <returns>A string that is formated using the string.Format() method.</returns>
		public static string FormatIt(string format, object arg0, object arg1)
		{
			return string.Format(format, arg0, arg1);
		}

		/// <summary>
		/// Formats the given string using the string.Format() method
		/// </summary>
		/// <param name="format">The format pattern.</param>
		/// <param name="arg0">The first argument for the string.</param>
		/// <param name="arg1">The second argument for the string.</param>
		/// <param name="arg2">The third argument for the string.</param>
		/// <returns>A string that is formated using the string.Format() method.</returns>
		public static string FormatIt(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(format, arg0, arg1, arg2);
		}

		/// <summary>
		/// Formats the given string using the string.Format() method
		/// </summary>
		/// <param name="format">The format pattern.</param>
		/// <param name="parameters">A dynamic list of parameters.</param>
		/// <returns>A string that is formated using the string.Format() method.</returns>
		public static string FormatIt(this string format, params object[] parameters)
		{
			return string.Format(format, parameters);
		}

		/// <summary>
		/// Formats the given string using the string.Format() method
		/// </summary>
		/// <param name="format">The format pattern.</param>
		/// <param name="provider">The provider to format the output.</param>
		/// <param name="args">A dynamic list of parameters.</param>
		/// <returns>A string that is formated using the string.Format() method.</returns>
		public static string FormatIt(this string format, IFormatProvider provider, params object[] args)
		{
			return string.Format(provider, format, args);
		}

		#endregion

		#region Split

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, char seperator)
		{
			return instance.Split(seperator.ToString(), int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="count">The maximum number of substrings to return.</param>
		/// <exception cref="ArgumentOutOfRangeException">The parameter count is negative.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, char seperator, int count)
		{
			if(count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count is negative");
			}

			return instance.Split(seperator.ToString(), count, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="options">Specify System.StringSplitOptions.RemoveEmptyEntries to omit
		/// empty array elements from the array returned, or System.StringSplitOptions.None to
		/// include empty array elements in the array returned.</param>
		/// <exception cref="ArgumentException">The parameter options is not on of the <see cref="System.StringSplitOptions"/> values.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, char seperator, StringSplitOptions options)
		{
			if((options < StringSplitOptions.None)
			|| (options > StringSplitOptions.RemoveEmptyEntries))
			{
				throw new ArgumentException(string.Format("option [{0}] is not one of the System.StringSplitOptions values.", options));
			}

			return instance.Split(seperator.ToString(), int.MaxValue, options);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="count">The maximum number of substrings to return.</param>
		/// <param name="options">Specify System.StringSplitOptions.RemoveEmptyEntries to omit
		/// empty array elements from the array returned, or System.StringSplitOptions.None to
		/// include empty array elements in the array returned.</param>
		/// <exception cref="ArgumentOutOfRangeException">The parameter count is negative.</exception>
		/// <exception cref="ArgumentException">The parameter options is not on of the <see cref="System.StringSplitOptions"/> values.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, char seperator, int count, StringSplitOptions options)
		{
			if(count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count is negative");
			}

			if((options < StringSplitOptions.None)
			|| (options > StringSplitOptions.RemoveEmptyEntries))
			{
				throw new ArgumentException(string.Format("option [{0}] is not one of the System.StringSplitOptions values.", options));
			}

			return instance.Split(seperator.ToString(), count, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <exception cref="ArgumentNullException">The parameter seperator cannot be null.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, string seperator)
		{
			if(seperator == null)
			{
				throw new ArgumentNullException("seperator", "Parameter seperator cannot be null!");
			}

			return instance.Split(seperator, int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="count">The maximum number of substrings to return.</param>
		/// <exception cref="ArgumentNullException">The parameter seperator cannot be null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The parameter count is negative.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, string seperator, int count)
		{
			if(seperator == null)
			{
				throw new ArgumentNullException("seperator", "Parameter seperator cannot be null!");
			}

			if(count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count is negative");
			}

			return instance.Split(seperator, count, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="options">Specify System.StringSplitOptions.RemoveEmptyEntries to omit
		/// empty array elements from the array returned, or System.StringSplitOptions.None to
		/// include empty array elements in the array returned.</param>
		/// <exception cref="ArgumentNullException">The parameter seperator cannot be null.</exception>
		/// <exception cref="ArgumentException">The parameter options is not on of the <see cref="System.StringSplitOptions"/> values.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, string seperator, StringSplitOptions options)
		{
			if(seperator == null)
			{
				throw new ArgumentNullException("seperator", "Parameter seperator cannot be null!");
			}

			if((options < StringSplitOptions.None)
			|| (options > StringSplitOptions.RemoveEmptyEntries))
			{
				throw new ArgumentException(string.Format("option [{0}] is not one of the System.StringSplitOptions values.", options));
			}

			return instance.Split(seperator, int.MaxValue, options);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this instance that are delimited
		/// by elements of a specified Unicode character array. A parameter specifies the maximum
		/// number of substrings to return.
		/// </summary>
		/// <param name="instance">The instance of the current string.</param>
		/// <param name="seperator">A unicode characters that delimit the substrings in this instance.</param>
		/// <param name="count">The maximum number of substrings to return.</param>
		/// <param name="options">Specify System.StringSplitOptions.RemoveEmptyEntries to omit
		/// empty array elements from the array returned, or System.StringSplitOptions.None to
		/// include empty array elements in the array returned.</param>
		/// <exception cref="ArgumentNullException">The parameter seperator cannot be null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The parameter count is negative.</exception>
		/// <exception cref="ArgumentException">The parameter options is not on of the <see cref="System.StringSplitOptions"/> values.</exception>
		/// <returns>
		/// An array whose elements contain the substrings in this instance that are delimited by
		/// one or more characters in separator. For more information, see the Remarks section.
		/// </returns>
		public static List<string> Split(this string instance, string seperator, int count, StringSplitOptions options)
		{
			if(seperator == null)
			{
				throw new ArgumentNullException("seperator", "Parameter seperator cannot be null!");
			}

			if(count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count is negative");
			}

			if((options < StringSplitOptions.None)
			|| (options > StringSplitOptions.RemoveEmptyEntries))
			{
				throw new ArgumentException(string.Format("option [{0}] is not one of the System.StringSplitOptions values.", options));
			}

			if(count == int.MaxValue)
			{
				return new List<string>(instance.Split(new string[] { seperator }, options));
			}
			else
			{
				return new List<string>(instance.Split(new string[] { seperator }, count, options));
			}
		}

		#endregion

		#region Work

		/// <summary>
		/// Trims a string, equal if it's null or not
		/// </summary>
		/// <param name="instance">The string itself.</param>
		/// <returns>Returns a trimmed string, or null, if the instance was null.</returns>
		public static string NullTrim(this string instance)
		{
			if(!instance.IsNullOrWhiteSpace())
			{
				return instance.Trim();
			}
			else
			{
				return instance;
			}
		}

		/// <summary>
		/// Parse a string to MD5.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Returns the string as MD5 string.</returns>
		public static string MD5(this string instance)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider provider 
				= new System.Security.Cryptography.MD5CryptoServiceProvider();

			byte[] bytes = Encoding.UTF8.GetBytes(instance);
			StringBuilder builder = new StringBuilder();

			bytes = provider.ComputeHash(bytes);

			foreach(byte b in bytes)
			{
				builder.Append(b.ToString("x2").ToLower(CultureInfo.CurrentCulture));
			}

			return builder.ToString();
		}

		#endregion
	}
}
