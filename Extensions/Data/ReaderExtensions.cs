using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Data
{
	public static class ReaderExtensions
	{
		/// <summary>
		/// returns a safe value in case database value is DBNull
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="reader">data reader object</param>
		/// <param name="name">field name</param>
		/// <returns>safe casted value</returns>
		public static TValue GetSafeValue<TValue>(this IDataReader reader, string name)
		{
			object value;
			try
			{
				value = reader[name];
				if(DBNull.Value.Equals(value))
				{
					return default(TValue);
				}
			}
			catch
			{
				value = default(TValue);
			}
			return (TValue)value;
		}

		/// <summary>
		/// Returns a safe default value in case database value is DBNull
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="reader">data reader object</param>
		/// <param name="name">field name</param>
		/// <param name="defaultValue">default value to return if field value is DBNull</param>
		/// <returns>safe casted value</returns>
		public static TValue GetSafeValue<TValue>(this IDataReader reader, string name, TValue defaultValue)
		{
			object value = reader[name];
			if(DBNull.Value.Equals(value))
			{
				return defaultValue;
			}
			return (TValue)value;
		}
	}
}
