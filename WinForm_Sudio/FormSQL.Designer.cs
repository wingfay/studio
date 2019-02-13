namespace WinForm_Sudio
{
   partial class FormSQL
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.button1 = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.btnTable2Word = new System.Windows.Forms.Button();
         this.txtTableName = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(126, 96);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 0;
         this.button1.Text = "测试表生成";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(115, 191);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(435, 185);
         this.textBox1.TabIndex = 1;
         // 
         // btnTable2Word
         // 
         this.btnTable2Word.Location = new System.Drawing.Point(396, 96);
         this.btnTable2Word.Name = "btnTable2Word";
         this.btnTable2Word.Size = new System.Drawing.Size(75, 23);
         this.btnTable2Word.TabIndex = 2;
         this.btnTable2Word.Text = "生成";
         this.btnTable2Word.UseVisualStyleBackColor = true;
         this.btnTable2Word.Click += new System.EventHandler(this.btnTable2Word_Click);
         // 
         // txtTableName
         // 
         this.txtTableName.Location = new System.Drawing.Point(396, 70);
         this.txtTableName.Name = "txtTableName";
         this.txtTableName.Size = new System.Drawing.Size(100, 20);
         this.txtTableName.TabIndex = 3;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(332, 70);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(31, 13);
         this.label1.TabIndex = 4;
         this.label1.Text = "表名";
         // 
         // FormSQL
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.txtTableName);
         this.Controls.Add(this.btnTable2Word);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.button1);
         this.Name = "FormSQL";
         this.Text = "FormSQL";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button btnTable2Word;
      private System.Windows.Forms.TextBox txtTableName;
      private System.Windows.Forms.Label label1;
   }
}