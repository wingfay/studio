using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio
{
   public partial class FormLDAP : Form
   {
      public FormLDAP()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         LDAPHelper lDAPHelper = new LDAPHelper();

         lDAPHelper.login();

      }
   }


   public class LDAPHelper
   {
      string ldapUrl = "LDAP://192.168.23.193/o=sa,c=org";
      string ldapUserName = "cn=root,o=niko,c=org";
      string ldapPassword = "Design2020";

      public LDAPHelper()
      {
      }
      public LDAPHelper(string ldap_url, string ldap_user, string ldap_pwd)
      {
         ldapUrl = ldap_url;
         ldapUserName = ldap_user;
         ldapPassword = ldap_pwd;
      }

      public bool login()
      {
         DirectoryEntry root = null;
         try
         {
            root = new DirectoryEntry(ldapUrl, ldapUserName, ldapPassword, AuthenticationTypes.None);
            string strName = root.Name;//失败，会抛出异常
            root.Close();
            root = null;
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }
   }

}
