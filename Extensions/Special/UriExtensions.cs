using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Special
{
	public static class UriExtensions
	{
		public class UriParameter
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}

		/// <summary>
		/// Parameterses the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns></returns>
		public static IList<UriParameter> Parameters(this Uri instance)
		{
			string[] _params = instance.Query.Split(
				new string[] { "&" },
				StringSplitOptions.None
			);

			List<UriParameter> list = new List<UriParameter>();

			foreach(string par in _params)
			{
				string[] _sep = par.Split(new char[] { '=' });
				string _key = "";
				string _value = "";

				if(_sep.Length > 0)
				{
					_key = _sep[0].Replace("?", "").Replace("&", "");
				}

				if(_sep.Length > 1)
				{
					_value = _sep[1].Replace("?", "").Replace("&", "");
				}

				list.Add(new UriParameter() { Key = _key, Value = _value });
			}

			return list;
		}

		/// <summary>
		/// Parameters the list.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns></returns>
		public static Dictionary<string, string> ParameterList(this Uri instance)
		{
			IList<UriParameter> list = instance.Parameters();
			Dictionary<string, string> dict = new Dictionary<string, string>();

			foreach(UriParameter param in list)
			{
				dict.Add(param.Key, param.Value);
			}

			return dict;
		}
	}
}
