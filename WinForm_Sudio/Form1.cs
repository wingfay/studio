using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        }
    }
}
