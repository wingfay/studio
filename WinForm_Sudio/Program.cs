﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio
{
   static class Program
   {
      /// <summary>
      /// 应用程序的主入口点。
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         //Application.Run(new Form1());
         //Application.Run(new FormSQL());
         //Application.Run(new FormPdf());
         //Application.Run(new FormLDAP());
         //Application.Run(new FormFile());
         //Application.Run(new FormWCAGCheck());
         Application.Run(new FormJSTOR());
      }
   }
}
