using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
	public class PrintTest
	{
		public static  void Load()
		{
         var server = new PrintServer();
         Console.WriteLine("Listing Shared Printers");
         var queues = server.GetPrintQueues(new[]
         { EnumeratedPrintQueueTypes.Shared, EnumeratedPrintQueueTypes.Connections });
         foreach (var item in queues)
         {
            Console.WriteLine(item.FullName);
         }
         Console.WriteLine("\nListing Local Printers Now");
         queues = server.GetPrintQueues(new[]
         { EnumeratedPrintQueueTypes.Local });
         foreach (var item in queues)
         {
            Console.WriteLine(item.FullName);
         }
        
      }

      public static void FindSharePrinter(string ServerName,string PrinterName)
		{
         string printerSever = string.Format("\\\\{0}", ServerName);
         var queues = new PrintServer(printerSever).GetPrintQueues();

         foreach (var item in queues)
         {
            Console.WriteLine(item.FullName);
         }



         



      }


      public static void FindSharePrinter()
		{
         // get available printers
         LocalPrintServer printServer = new LocalPrintServer();
         PrintQueue defaultPrintQueue = printServer.DefaultPrintQueue;

         // get all printers installed (from the users perspective)he t
         var printerNames = PrinterSettings.InstalledPrinters;
         var availablePrinters = printerNames.Cast<string>().Select(printerName =>
         {
            var match = Regex.Match(printerName, @"(?<machine>\\\\.*?)\\(?<queue>.*)");
            PrintQueue queue;
            if (match.Success)
            {
               queue = new PrintServer(match.Groups["machine"].Value).GetPrintQueue(match.Groups["queue"].Value);
            }
            else
            {
               queue = printServer.GetPrintQueue(printerName);
            }

            var capabilities = queue.GetPrintCapabilities();
            return new AvailablePrinterInfo()
            {
               Name = printerName,
               Default = queue.FullName == defaultPrintQueue.FullName,
               Duplex = capabilities.DuplexingCapability.Contains(Duplexing.TwoSidedLongEdge),
               Color = capabilities.OutputColorCapability.Contains(OutputColor.Color)
            };
         }).ToArray();

         var DefaultPrinter = availablePrinters.SingleOrDefault(x => x.Default);
      }


      public static void WMILoad()
		{
         // Use the ObjectQuery to get the list of configured printers
         System.Management.ObjectQuery oquery =
             new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

         System.Management.ManagementObjectSearcher mosearcher =
             new System.Management.ManagementObjectSearcher(oquery);

         System.Management.ManagementObjectCollection moc = mosearcher.Get();

         foreach (ManagementObject mo in moc)
         {
            System.Management.PropertyDataCollection pdc = mo.Properties;
            foreach (System.Management.PropertyData pd in pdc)
            {
					if ((bool)mo["Network"] == true)
					{
                  Console.WriteLine($"PrintName：{mo[pd.Name]}  IsNetwork:{((bool)mo["Network"]).ToString()}");
               }
          

               

            }
         }
      }

	}

	internal class AvailablePrinterInfo
	{
		public AvailablePrinterInfo()
		{
		}

		public string Name { get; set; }
		public bool Default { get; set; }
		public bool Duplex { get; set; }
		public bool Color { get; set; }
	}
}
