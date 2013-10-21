using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extensions.Basics;

namespace Extensions
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(Environment.OSVersion);
			Console.WriteLine(Environment.Version);



			//string text = "        ";

			//// Ohne Extension
			//if(text != null
			//|| !string.IsNullOrEmpty(text))
			//{
			//	// Tue etwas
			//}

			//// Mit Extension vor .NET 4.0
			//if(!text.IsNullOrWhiteSpace())
			//{
			//	// Tue etwas
			//}

			//// Ohne Extension seit .NET 4.0
			//if(!string.IsNullOrWhiteSpace(text))
			//{
			//	// Tue etwas
			//}

			Console.Read();
		}
	}
}
