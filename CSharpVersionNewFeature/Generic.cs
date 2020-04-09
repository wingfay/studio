using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpVersionNewFeature
{
   public class GenericList<T>
   {
      public void Add(T input) { }
   }

   
   class TestGenericList
   {
      private class ExampleClass { }

      static void Main()
      {
         GenericList<int> list1 = new GenericList<int>();
         list1.Add(1);

         Console.WriteLine(list1);

      }
   }
}
