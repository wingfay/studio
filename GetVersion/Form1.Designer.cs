namespace GetVersion
{
   partial class Form1
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
         this.lblILSVersion = new System.Windows.Forms.Label();
         this.lblISISVersion = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // lblILSVersion
         // 
         this.lblILSVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblILSVersion.Location = new System.Drawing.Point(116, 28);
         this.lblILSVersion.Name = "lblILSVersion";
         this.lblILSVersion.Size = new System.Drawing.Size(466, 135);
         this.lblILSVersion.TabIndex = 0;
         this.lblILSVersion.Text = "lblILSVersion";
         this.lblILSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // lblISISVersion
         // 
         this.lblISISVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblISISVersion.Location = new System.Drawing.Point(116, 241);
         this.lblISISVersion.Name = "lblISISVersion";
         this.lblISISVersion.Size = new System.Drawing.Size(466, 135);
         this.lblISISVersion.TabIndex = 1;
         this.lblISISVersion.Text = "lblISISVersion";
         this.lblISISVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.lblISISVersion);
         this.Controls.Add(this.lblILSVersion);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Label lblILSVersion;
      private System.Windows.Forms.Label lblISISVersion;
   }
}

