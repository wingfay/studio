using System;
using System.Collections.Generic;
using System.Text;
using ZK.NetCore.Util;

namespace ZK.NetCore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            int iteration = 100 * 1000;

            string s = "";
            CodeTimer.Time("String Concat", iteration, () => { s += "a"; });

            StringBuilder sb = new StringBuilder();
            CodeTimer.Time("StringBuilder", iteration, () => { sb.Append("a"); });



        }
    }
}
