using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using WinForm_Sudio.AOP;
using ZK.NetStandard.Util;

namespace WinForm_Sudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strBool = "dsftrue234";

           // MessageBox.Show(strBool.GetBool().ToString());

            MessageBox.Show(strBool.GetInt().ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BusinessHandler handler = new BusinessHandler();
            handler.DoSomething();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //List<string> a = new List<string> { "A", "N", "C" };

            //MessageBox.Show(string.Join(",", a));

            string[] arrayShortcutKey ={"","Ctrl+A","Ctrl+B"};


            List<string> Ex = new List<string> { "Ctrl+A" };

            var s= arrayShortcutKey.Except(Ex);

            MessageBox.Show(string.Join(",", s));


//            string json = @"{
//  'Name': 'Bad Boys',
//  'ReleaseDate': '1995-4-7T00:00:00',
//  'Genres': [
//    'Action',
//    'Comedy'
//  ]
//}";

//            var s = JsonConvert.DeserializeObject<Movie>(json);

//            MessageBox.Show(s.Name);



        }

        public class Movie
        {
            public string Name { get; set; }

            public DateTime ReleaseDate { get; set; }
        }

      private void button5_Click(object sender, EventArgs e)
      {
         //string s = "http://localhost/Webopac2015/onedriver/";

         //string ss = "https://www.ssss.com/ondriver/";

         //s = s.Replace("http://", "").Replace("https://", "");
         //s=s.Substring(s.IndexOf("/"));

         //ss = ss.Replace("http://", "").Replace("https://", "");
         //ss=ss.Substring(ss.IndexOf("/"));




         //MessageBox.Show("s:" + s + ",ss:" + ss);

         var s = "111".Sha1();
         var s1 = "admin".Sha1();
         var s2 = "11111111".Sha1();
         MessageBox.Show(s);


      }

   }

   public static class EncryptHelper
   {
      /// <summary>
      /// 基于Md5的自定义加密字符串方法：输入一个字符串，返回一个由32个字符组成的十六进制的哈希散列（字符串）。
      /// </summary>
      /// <param name="str">要加密的字符串</param>
      /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
      public static string Md5(this string str)
      {
         //将输入字符串转换成字节数组
         var buffer = Encoding.Default.GetBytes(str);
         //接着，创建Md5对象进行散列计算
         var data = MD5.Create().ComputeHash(buffer);

         //创建一个新的Stringbuilder收集字节
         var sb = new StringBuilder();

         //遍历每个字节的散列数据 
         foreach (var t in data)
         {
            //格式每一个十六进制字符串
            sb.Append(t.ToString("X2"));
         }

         //返回十六进制字符串
         return sb.ToString();
      }

      /// <summary>
      /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
      /// </summary>
      /// <param name="str">要加密的字符串</param>
      /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
      public static string Sha1(this string str)
      {
         var buffer = Encoding.UTF8.GetBytes(str);
         var data = SHA1.Create().ComputeHash(buffer);

         var sb = new StringBuilder();
         foreach (var t in data)
         {
            sb.Append(t.ToString("X2"));
         }

         return sb.ToString();
      }
   }
}
