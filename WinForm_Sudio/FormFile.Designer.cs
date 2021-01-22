namespace WinForm_Sudio
{
   partial class FormFile
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
         this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // directorySearcher1
         // 
         this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
         this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
         this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(144, 94);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 0;
         this.button1.Text = "button1";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // FormFile
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.button1);
         this.Name = "FormFile";
         this.Text = "FormFile";
         this.ResumeLayout(false);

      }

      #endregion

      private System.DirectoryServices.DirectorySearcher directorySearcher1;
      private System.Windows.Forms.Button button1;
   }
}