using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	class PageStart : Page
	{
		public DarkTheme.Button btnHostPublic;
		public DarkTheme.Button btnConnect;
		private Label lblCopyright;
		public System.Windows.Forms.Label lblTitle;

		public PageStart()
		{
			InitializeComponent();

			// TODO: copyright
		}

		private void InitializeComponent()
		{
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnHostPublic = new DarkTheme.Button();
			this.btnConnect = new DarkTheme.Button();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTitle.Font = new System.Drawing.Font("Consolas", 50F);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(217, 128);
			this.lblTitle.MaximumSize = new System.Drawing.Size(680, 170);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(680, 170);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Willkommen bei Scribble";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnHostPublic
			// 
			this.btnHostPublic.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnHostPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnHostPublic.FlatAppearance.BorderSize = 0;
			this.btnHostPublic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnHostPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnHostPublic.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnHostPublic.ForeColor = System.Drawing.Color.White;
			this.btnHostPublic.Location = new System.Drawing.Point(344, 351);
			this.btnHostPublic.Name = "btnHostPublic";
			this.btnHostPublic.Size = new System.Drawing.Size(200, 50);
			this.btnHostPublic.TabIndex = 3;
			this.btnHostPublic.Text = "Server hosten";
			this.btnHostPublic.UseVisualStyleBackColor = false;
			this.btnHostPublic.Click += new System.EventHandler(this.btnHostPublic_Click);
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnConnect.FlatAppearance.BorderSize = 0;
			this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConnect.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnConnect.ForeColor = System.Drawing.Color.White;
			this.btnConnect.Location = new System.Drawing.Point(550, 351);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(200, 50);
			this.btnConnect.TabIndex = 6;
			this.btnConnect.Text = "Verbinden";
			this.btnConnect.UseVisualStyleBackColor = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// lblCopyright
			// 
			this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCopyright.ForeColor = System.Drawing.Color.White;
			this.lblCopyright.Location = new System.Drawing.Point(933, 551);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(160, 23);
			this.lblCopyright.TabIndex = 7;
			this.lblCopyright.Text = "by Marcel Weski © 2018";
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// PageStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnHostPublic);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageStart";
			this.ResumeLayout(false);

		}

		// Control Events
		private void btnHost_Click(object sender, EventArgs e)
		{
			this.Parent.openPage(new PageCreateRoom());
		}
		private void btnJoin_Click(object sender, EventArgs e)
		{
			//FrmMain frmMain = (this.Parent as FrmMain);
			//frmMain.openPage(new PageJoin());
		}
		private void btnHostPublic_Click(object sender, EventArgs e)
		{
			this.Parent.openPage(new PageHost());
		}
		private void btnConnect_Click(object sender, EventArgs e)
		{
			this.Parent.openPage(new PageConnect());
		}
	}
}
