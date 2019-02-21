using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZK.NetCore.ConsoleApp
{
   public class LinqTest
   {
      public static void Dict()
      {
         var dict = new Dictionary<string, string>
         {
            ["Hello"] = "ValueOfHello",
            ["World"] = "ValueOfWorld"
         };


         var Result = dict.Where(key => key.Key.StartsWith("Hell"));

         if(Result.Any())
         {
            Console.WriteLine($"Key:Hello,Value:{Result.First().Value}");
         }
         else
         {
            Console.WriteLine($"Key:Hello,Value is Not Exist!");
         }


         var Result1 = dict.Where(key => key.Key.StartsWith("Test"));

         if (Result1.Any())
         {
            Console.WriteLine($"Key:Test,Value:{Result1.First().Value}");
         }
         else
         {
            Console.WriteLine($"Key:Test,Value is Not Exist!");
         }

      }
   }
}
