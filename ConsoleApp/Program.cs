using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceExample.CA;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CanadaPurchaseTest.Test(Global.StoreName,Global.Token,string.Empty);
        }
    }
}
