using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetVersion
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();

         string strConnectionString = @"server=ip;database=table;uid=sa;pwd=password";

         using (SqlConnection conn = new SqlConnection(strConnectionString))
         {
            conn.Open();
            //执行存储过程
            SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 [Version]
              FROM [tblVersion] where [VersionProject]='ILS'  AND [IsDeleted]=0 ORDER BY 1 desc ", conn);
            cmd.CommandType = CommandType.Text;
            string Version = cmd.ExecuteScalar().ToString();
            lblILSVersion.Text = "ILS:" + Version;


            cmd = new SqlCommand(@"SELECT TOP 1 [Version]
              FROM [tblVersion] where [VersionProject]='ISIS'  AND [IsDeleted]=0 ORDER BY 1 desc ", conn);
            cmd.CommandType = CommandType.Text;
            Version = cmd.ExecuteScalar().ToString();
            lblISISVersion.Text = "ISIS:" + Version;
         }

      }
   }
}
