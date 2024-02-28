using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	public class LinqHelper
	{
		/// <summary>
		/// 差集
		/// </summary>
		public static void Difference()
		{
			List<int> list1 = new List<int>();
			list1.Add(1);
			//list1.Add(2);
			list1.Add(3);
			List<int> list2 = new List<int>();
			//list2.Add(1);
			list2.Add(2);
			list2.Add(3);
			//list2.Add(4);
			//list2.Add(5);

			List<int> list3 = list1.Except(list2).ToList();


			foreach(var i in list3)
			{
				System.Console.WriteLine(i);
			}

			List<int> list4 = list2.Except(list1).ToList();


			foreach (var i in list4)
			{
				System.Console.WriteLine(i);
			}

			System.Console.ReadKey();
		}



		/// <summary>
		/// 交集
		/// </summary>
		public static void Intersect()
		{
			List<int> list1 = new List<int>();
			list1.Add(1);
			//list1.Add(2);
			list1.Add(3);
			List<int> list2 = new List<int>();
			//list2.Add(1);
			list2.Add(2);
			list2.Add(3);
			//list2.Add(4);
			//list2.Add(5);

			List<int> list3 = list1.Intersect(list2).ToList();


			foreach (var i in list3)
			{
				System.Console.WriteLine(i);
			}


			System.Console.ReadKey();
		}

	}
}
