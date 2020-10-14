using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Net;
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
         //LDAPHelper lDAPHelper = new LDAPHelper();

         //lDAPHelper.login();

         IsConnection();

      }


      public bool IsConnection()
      {

         string ldapUrl = "LDAP://LDAPSTEST/rootDSE";
         //string ldapUrl = "LDAP://LDAPSTEST:636";
         string ldapUserName = "Niko";
         string ldapPassword = "Design2020";

         string strADIPAddress = "LDAPSTEST:389";
            string RootPath = string.Format("LDAP://{0}/rootDSE", strADIPAddress);
         //DirectoryEntry oDE = new DirectoryEntry(RootPath);

         DirectoryEntry oDE = new DirectoryEntry(RootPath);
         oDE.Dispose();

         oDE = new DirectoryEntry(RootPath, ldapUserName, ldapPassword, AuthenticationTypes.Secure);



         try
            {

               string strldapServiceName = oDE.Properties["ldapServiceName"][0].ToString();


               string[] ldapServiceName = strldapServiceName.Split(Convert.ToChar(":"));
               string[] DoaminController = ldapServiceName[1].Split(Convert.ToChar("$"));

               string UsrDomain = GetUserLDAPDomain(ldapServiceName[0]);

               string[] LDAPDC = ldapServiceName[0].Split(Convert.ToChar("."));

               string strADPath = string.Format("LDAP://{0}/{1}", DoaminController[0], UsrDomain);
               strADIPAddress = string.Format("LDAP://{0}/{1}", strADIPAddress, UsrDomain);
               object native = oDE.NativeObject;
               var AdServerAuthenticationType = oDE.AuthenticationType;

               ///log 
              // CreateLogo("ADManager IsConnection Pass ", string.Empty, string.Empty, string.Empty, strADPath, AdServerAuthenticationType);

               //Debug("IsConnection 3");

               oDE.Close();
               oDE.Dispose();

               //EnumerateOU();  ///for test ldap user info


               return true;
            }
            catch (System.Exception ex)
            {
            string Error = ex.Message;
               ///log 
               //CreateLogo("ADManager IsConnection Exception ", GetInnerException(ex), string.Empty, string.Empty, string.Empty, null);
            }
            return false;

        
         return false;
      }

      string GetUserLDAPDomain(string serverName)
      {



         StringBuilder LDAPDomain = new StringBuilder();

         string[] LDAPDC = serverName.Split(Convert.ToChar("."));
         int index = 0;
         while (index < LDAPDC.GetUpperBound(0) + 1)
         {
            LDAPDomain.Append("DC=" + LDAPDC[index]);
            if (index < LDAPDC.GetUpperBound(0))
            {
               LDAPDomain.Append(",");
            }
            index += 1;
         }
         return LDAPDomain.ToString();
      }
   }


   public class LDAPHelper
   {
     // string ldapUrl = "LDAP://LDAPSTEST/rootDSE";
      //string ldapUrl = "LDAP://LDAPSTEST";
      string ldapUrl = "LDAP://LDAPSTEST:636";
      string ldapUserName = "Niko";
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
            root = new DirectoryEntry(ldapUrl, ldapUserName, ldapPassword, AuthenticationTypes.SecureSocketsLayer);
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

      //public string CheckLogin636(string UserName, string Password)
      //{
      //   var host = "IP或域名:636";
      //   var baseDN = "baseDN";
      //   var adminName = @"用户DN";
      //   var adminPass = @"用户密码";// "管理密码";
      //   var identifier = new LdapDirectoryIdentifier(host);
      //   var conn = new LdapConnection(identifier, new NetworkCredential
      //   {
      //      UserName = adminName,
      //      Password = adminPass
      //   });
      //   conn.SessionOptions.SecureSocketLayer = true;//开启使用ssl
      //   conn.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(ServerCallback);// 信任服务器是可以默认返回true：+= delegate { return true; };
      //   conn.AuthType = AuthType.Basic;
      //   conn.Bind();//这里不报错就是连接成功
      //   var request = new SearchRequest(baseDN, "(userPrincipalName=" + UserName + ")", System.DirectoryServices.Protocols.SearchScope.Subtree);
      //   SearchResponse response = conn.SendRequest(request) as SearchResponse;
      //   if (response.Entries != null && response.Entries.Count > 0)
      //   {
      //      //这里就表示 用户存在
      //      try
      //      {
      //         var connUser = new LdapConnection(identifier, new NetworkCredential
      //         {
      //            UserName = response.Entries[0].DistinguishedName,
      //            Password = Password
      //         });
      //         connUser.SessionOptions.SecureSocketLayer = true;
      //         connUser.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(ServerCallback);// 信任服务器是可以默认返回true：+= delegate { return true; };
      //         connUser.AuthType = AuthType.Basic;
      //         connUser.Bind();//这里不报错就是 验证登录成功
      //         var attr = response.Entries[0].Attributes;//用户信息
      //         string UID = attr["samAccountName"][0].ToString();
      //         string UName = attr["name"][0].ToString();
      //         string Email = attr["userPrincipalName"][0].ToString();
      //         connUser.Dispose();
      //         conn.Dispose();
      //         return "登录成功";
      //      }
      //      catch (Exception e)
      //      {
      //         conn.Dispose();
      //         if (e.Message == "The supplied credential is invalid.")
      //         {
      //            return "用户名或密码不正确";
      //         }
      //         else
      //         {
      //            return e.Message;
      //         }
      //      }
      //   }
      //   else
      //   {
      //      conn.Dispose();
      //      return "未找到用户";
      //   }
      //}
   }

}
