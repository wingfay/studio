namespace WinForm_Sudio
{
   partial class FormWCAGCheck
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
         this.btnCheck = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.txtFolder = new System.Windows.Forms.TextBox();
         this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
         this.panel2 = new System.Windows.Forms.Panel();
         this.dataGridView1 = new System.Windows.Forms.DataGridView();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
         this.SuspendLayout();
         // 
         // btnCheck
         // 
         this.btnCheck.Location = new System.Drawing.Point(476, 10);
         this.btnCheck.Name = "btnCheck";
         this.btnCheck.Size = new System.Drawing.Size(75, 23);
         this.btnCheck.TabIndex = 0;
         this.btnCheck.Text = "Check";
         this.btnCheck.UseVisualStyleBackColor = true;
         this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.txtFolder);
         this.panel1.Controls.Add(this.btnCheck);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(800, 44);
         this.panel1.TabIndex = 2;
         // 
         // txtFolder
         // 
         this.txtFolder.Location = new System.Drawing.Point(12, 12);
         this.txtFolder.Name = "txtFolder";
         this.txtFolder.Size = new System.Drawing.Size(424, 20);
         this.txtFolder.TabIndex = 3;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.dataGridView1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 44);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(800, 406);
         this.panel2.TabIndex = 3;
         // 
         // dataGridView1
         // 
         this.dataGridView1.AllowUserToAddRows = false;
         this.dataGridView1.AllowUserToDeleteRows = false;
         this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataGridView1.Location = new System.Drawing.Point(0, 0);
         this.dataGridView1.Name = "dataGridView1";
         this.dataGridView1.ReadOnly = true;
         this.dataGridView1.Size = new System.Drawing.Size(800, 406);
         this.dataGridView1.TabIndex = 0;
         // 
         // FormWCAGCheck
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Name = "FormWCAGCheck";
         this.Text = "FormWCAGCheck";
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnCheck;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.TextBox txtFolder;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.DataGridView dataGridView1;
   }
}