using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	public class DictionaryTest
	{
		public static void Test()
		{
         Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();

         System.Console.WriteLine(keyValuePairs.Count);

         //System.Console.WriteLine(keyValuePairs.ElementAt(0).Key);


         keyValuePairs["a"] = 1;

         System.Console.WriteLine(keyValuePairs.Count);

         System.Console.WriteLine(keyValuePairs.ElementAt(0).Key);

         keyValuePairs["b"] = 1;

         System.Console.WriteLine(keyValuePairs.Count);

         System.Console.WriteLine(keyValuePairs.ElementAt(0).Key);

         Console.ReadKey();
      }
	}
}
