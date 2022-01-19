
namespace WinForm_Sudio
{
	partial class FormJSTOR
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
			this.txtKeyword = new System.Windows.Forms.TextBox();
			this.txtSearchURL = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(161, 140);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtKeyword
			// 
			this.txtKeyword.Location = new System.Drawing.Point(161, 98);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(308, 20);
			this.txtKeyword.TabIndex = 1;
			// 
			// txtSearchURL
			// 
			this.txtSearchURL.Location = new System.Drawing.Point(161, 190);
			this.txtSearchURL.Name = "txtSearchURL";
			this.txtSearchURL.Size = new System.Drawing.Size(308, 20);
			this.txtSearchURL.TabIndex = 2;
			// 
			// FormJSTOR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtSearchURL);
			this.Controls.Add(this.txtKeyword);
			this.Controls.Add(this.button1);
			this.Name = "FormJSTOR";
			this.Text = "FormJSTOR";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtKeyword;
		private System.Windows.Forms.TextBox txtSearchURL;
	}
}