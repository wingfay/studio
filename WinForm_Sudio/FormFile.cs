using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinForm_Sudio
{
   public partial class FormFile : Form
   {
      public FormFile()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         try
         {
            var path = "C:\\";
            if (!Directory.Exists(path + "Test"))
            {
               Directory.CreateDirectory(path + "Test");
            }
            else
            {

               Directory.Delete(path + "Test",true);
            }
         }
         catch (Exception ex)
         {
            
            throw ex;
         }
         
      }
   }
}
