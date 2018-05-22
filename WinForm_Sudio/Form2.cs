using Common.Cache.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();



            
        }

        /// <summary>
        /// 写入Redis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            List<UserModel> users = new List<UserModel>();

            users.Add(new UserModel
            {
                Id=210000,
                UserName="张三"
            });
            users.Add(new UserModel
            {
                Id = 210001,
                UserName = "曹操"
            });
            users.Add(new UserModel
            {
                Id = 210002,
                UserName = "應征"
            });
            users.Add(new UserModel
            {
                Id = 210003,
                UserName = "哈哈哈漢Σ( ° △ °|||)︴"
            });

            if(RedisCache.Set("user",users))
            {
                MessageBox.Show("寫入成功");
            }
            else
            {
                MessageBox.Show("寫入失敗");
            }
        }

        /// <summary>
        /// 读取Redis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            var users = RedisCache.Get<List<UserModel>>("user");


            MessageBox.Show(users.ToJson());


            var all = RedisCache.Get<List<Dictionary<string,string>>>("GetAll");

            MessageBox.Show(all.ToJson());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ConfigurationManager.AppSettings["hello"]);

            var obj = ConfigurationManager.GetSection("redisCacheClient");

            MessageBox.Show(obj.ToString());
        }
    }

    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }


    }
}
