﻿namespace WinForm_Sudio
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.button3 = new System.Windows.Forms.Button();
         this.button4 = new System.Windows.Forms.Button();
         this.button5 = new System.Windows.Forms.Button();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.button6 = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.textBox3 = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(12, 42);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 25);
         this.button1.TabIndex = 0;
         this.button1.Text = "测试Object Ext";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Location = new System.Drawing.Point(114, 42);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(75, 25);
         this.button2.TabIndex = 1;
         this.button2.Text = "AOP拦截";
         this.button2.UseVisualStyleBackColor = true;
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // button3
         // 
         this.button3.Location = new System.Drawing.Point(12, 169);
         this.button3.Name = "button3";
         this.button3.Size = new System.Drawing.Size(75, 25);
         this.button3.TabIndex = 2;
         this.button3.Text = "button3";
         this.button3.UseVisualStyleBackColor = true;
         this.button3.Click += new System.EventHandler(this.button3_Click);
         // 
         // button4
         // 
         this.button4.Location = new System.Drawing.Point(533, 224);
         this.button4.Name = "button4";
         this.button4.Size = new System.Drawing.Size(8, 8);
         this.button4.TabIndex = 3;
         this.button4.Text = "button4";
         this.button4.UseVisualStyleBackColor = true;
         // 
         // button5
         // 
         this.button5.Location = new System.Drawing.Point(178, 171);
         this.button5.Name = "button5";
         this.button5.Size = new System.Drawing.Size(75, 23);
         this.button5.TabIndex = 4;
         this.button5.Text = "button5";
         this.button5.UseVisualStyleBackColor = true;
         this.button5.Click += new System.EventHandler(this.button5_Click);
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.FileName = "openFileDialog1";
         // 
         // button6
         // 
         this.button6.Location = new System.Drawing.Point(178, 288);
         this.button6.Name = "button6";
         this.button6.Size = new System.Drawing.Size(75, 23);
         this.button6.TabIndex = 5;
         this.button6.Text = "file";
         this.button6.UseVisualStyleBackColor = true;
         this.button6.Click += new System.EventHandler(this.button6_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(178, 317);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(515, 20);
         this.textBox1.TabIndex = 6;
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(178, 343);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(551, 20);
         this.textBox2.TabIndex = 7;
         // 
         // textBox3
         // 
         this.textBox3.Location = new System.Drawing.Point(178, 369);
         this.textBox3.Name = "textBox3";
         this.textBox3.Size = new System.Drawing.Size(551, 20);
         this.textBox3.TabIndex = 8;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(954, 638);
         this.Controls.Add(this.textBox3);
         this.Controls.Add(this.textBox2);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.button6);
         this.Controls.Add(this.button5);
         this.Controls.Add(this.button4);
         this.Controls.Add(this.button3);
         this.Controls.Add(this.button2);
         this.Controls.Add(this.button1);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
      private System.Windows.Forms.Button button5;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.Button button6;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.TextBox textBox3;
   }
}

