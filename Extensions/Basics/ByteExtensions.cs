using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Basics
{
	public static class ByteExtensions
	{
		/// <summary>
		/// Converts the instance of this byte array to an converted ascii string
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static string ToAsciiString(this byte[] instance)
		{
			ASCIIEncoding enc = new ASCIIEncoding();
			return enc.GetString(instance);
		}

		/// <summary>
		/// Converts the instance of this byte array to an simple string.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="instance">Instance.</param>
		public static string ToString(this byte[] instance)
		{
			char[] chars = new char[instance.Length / sizeof(char)];
			System.Buffer.BlockCopy(instance, 0, chars, 0, instance.Length);
			return new string(chars);
		}

		/// <summary>
		/// Converts the instance of this byte array to a stream.
		/// </summary>
		/// <param name="instance">The instance of the byte array.</param>
		/// <returns>A stream with the byte array as content.</returns>
		public static Stream ToStream(this byte[] instance)
		{
			MemoryStream memStream = new MemoryStream(instance);
			return memStream;
		}
	}
}
