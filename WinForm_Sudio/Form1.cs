using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    }
}
