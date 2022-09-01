using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	public class NetworkAdapters
	{
      public static void ListAllNetworkAdapters()
      {
         ManagementObjectSearcher networkAdapterSearcher = new ManagementObjectSearcher("root\\cimv2", "select * from Win32_NetworkAdapterConfiguration");
         ManagementObjectCollection objectCollection = networkAdapterSearcher.Get();

         Console.WriteLine("There are {0} network adapaters: ", objectCollection.Count);

         foreach (ManagementObject networkAdapter in objectCollection)
         {
            PropertyDataCollection networkAdapterProperties = networkAdapter.Properties;
            foreach (PropertyData networkAdapterProperty in networkAdapterProperties)
            {
               if (networkAdapterProperty.Value != null)
               {
                  Console.WriteLine("Network adapter property name: {0}", networkAdapterProperty.Name);
                  Console.WriteLine("Network adapter property value: {0}", networkAdapterProperty.Value);
               }
            }
            Console.WriteLine("---------------------------------------");
         }
      }

      public static string GetLocalIPAddress()
      {
         var host = Dns.GetHostEntry(Dns.GetHostName());
         foreach (var ip in host.AddressList)
         {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
               Console.WriteLine($"IP: {ip}");
               return ip.ToString();
            }

            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
            {
               Console.WriteLine($"IP: {ip}");
               return ip.ToString();
            }
         }
         throw new Exception("No network adapters with an IPv4 address in the system!");
      }
   }
}
