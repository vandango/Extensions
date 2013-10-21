using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extensions.Basics;

namespace Extensions.Collections
{
	public static class DictionaryExtensions
	{
		public static string ToParamString<TKey, TValue>(this Dictionary<TKey, TValue> instance, bool asAbsolut)
		{
			StringBuilder str = new StringBuilder();

			if(asAbsolut)
			{
				str.Append("?");
			}

			bool isFirst = true;

			foreach(KeyValuePair<TKey, TValue> item in instance)
			{
				string _key = (string)((object)item.Key);
				string _value = (string)((object)item.Value);

				if(!_key.IsNullOrWhiteSpace())
				{
					if(isFirst && !asAbsolut)
					{
						str.Append("&");
					}
					else if(!isFirst)
					{
						str.Append("&");
					}

					str.Append(_key);
				}

				if(!_value.IsNullOrWhiteSpace())
				{
					str.Append("=");
					str.Append(_value);
				}

				isFirst = false;
			}

			return str.ToString();
		}

		/// <summary>
		/// Add a range of items to the instance of this dictionary
		/// </summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
		/// <param name="instance">The instance of the dictionary.</param>
		/// <param name="list">The dictionary to add.</param>
		public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> instance, Dictionary<TKey, TValue> list)
		{
			foreach(KeyValuePair<TKey, TValue> item in list)
			{
				instance.Add(item.Key, item.Value);
			}
		}

		/// <summary>
		/// Remove a range of items from the instance of this dictionary
		/// </summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
		/// <param name="instance">The instance of the dictionary.</param>
		/// <param name="list">The dictionary to remove.</param>
		public static void RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> instance, Dictionary<TKey, TValue> list)
		{
			foreach(KeyValuePair<TKey, TValue> item in list)
			{
				instance.Remove(item.Key);
			}
		}

		/// <summary>
		/// Get an entry from a specific index
		/// </summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
		/// <param name="instance">The instance of the dictionary.</param>
		/// <param name="index">The index of the target object.</param>
		/// <returns>Returns a entry of the dictionary from a specific index.</returns>
		public static KeyValuePair<TKey, TValue> FromIndex<TKey, TValue>(this Dictionary<TKey, TValue> instance, int index)
		{
			if(instance != null && instance.Count > 0)
			{
				int count = 0;

				foreach(KeyValuePair<TKey, TValue> item in instance)
				{
					if(index == count)
					{
						return item;
					}

					count++;
				}

				return default(KeyValuePair<TKey, TValue>);
			}
			else
			{
				return default(KeyValuePair<TKey, TValue>);
			}
		}
	}
}
