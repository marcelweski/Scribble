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
		public DarkTheme.Button btnCreateRoom;
		public DarkTheme.Button btnJoin;
		public DarkTheme.Button btnHostPublic;
		public DarkTheme.Button btnHostLocal;
		private System.Windows.Forms.Button btnTest;
		public DarkTheme.Button btnConnect;
		public System.Windows.Forms.Label lblTitle;

		public PageStart()
		{
			InitializeComponent();

			// TODO: copyright
		}

		private void InitializeComponent()
		{
			this.btnCreateRoom = new DarkTheme.Button();
			this.btnJoin = new DarkTheme.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnHostPublic = new DarkTheme.Button();
			this.btnHostLocal = new DarkTheme.Button();
			this.btnTest = new System.Windows.Forms.Button();
			this.btnConnect = new DarkTheme.Button();
			this.SuspendLayout();
			// 
			// btnCreateRoom
			// 
			this.btnCreateRoom.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnCreateRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnCreateRoom.Enabled = false;
			this.btnCreateRoom.FlatAppearance.BorderSize = 0;
			this.btnCreateRoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCreateRoom.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnCreateRoom.ForeColor = System.Drawing.Color.White;
			this.btnCreateRoom.Location = new System.Drawing.Point(551, 351);
			this.btnCreateRoom.Name = "btnCreateRoom";
			this.btnCreateRoom.Size = new System.Drawing.Size(200, 50);
			this.btnCreateRoom.TabIndex = 0;
			this.btnCreateRoom.Text = "Raum erstellen";
			this.btnCreateRoom.UseVisualStyleBackColor = false;
			this.btnCreateRoom.Click += new System.EventHandler(this.btnHost_Click);
			// 
			// btnJoin
			// 
			this.btnJoin.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnJoin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnJoin.Enabled = false;
			this.btnJoin.FlatAppearance.BorderSize = 0;
			this.btnJoin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnJoin.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnJoin.ForeColor = System.Drawing.Color.White;
			this.btnJoin.Location = new System.Drawing.Point(551, 407);
			this.btnJoin.Name = "btnJoin";
			this.btnJoin.Size = new System.Drawing.Size(200, 50);
			this.btnJoin.TabIndex = 1;
			this.btnJoin.Text = "Raum beitreten";
			this.btnJoin.UseVisualStyleBackColor = false;
			this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTitle.Font = new System.Drawing.Font("Consolas", 50F);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(218, 128);
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
			this.btnHostPublic.Location = new System.Drawing.Point(345, 351);
			this.btnHostPublic.Name = "btnHostPublic";
			this.btnHostPublic.Size = new System.Drawing.Size(200, 50);
			this.btnHostPublic.TabIndex = 3;
			this.btnHostPublic.Text = "Server hosten";
			this.btnHostPublic.UseVisualStyleBackColor = false;
			this.btnHostPublic.Click += new System.EventHandler(this.btnHostPublic_Click);
			// 
			// btnHostLocal
			// 
			this.btnHostLocal.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnHostLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnHostLocal.Enabled = false;
			this.btnHostLocal.FlatAppearance.BorderSize = 0;
			this.btnHostLocal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnHostLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnHostLocal.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnHostLocal.ForeColor = System.Drawing.Color.White;
			this.btnHostLocal.Location = new System.Drawing.Point(345, 407);
			this.btnHostLocal.Name = "btnHostLocal";
			this.btnHostLocal.Size = new System.Drawing.Size(200, 50);
			this.btnHostLocal.TabIndex = 4;
			this.btnHostLocal.Text = "Lokal hosten und Raum erstellen";
			this.btnHostLocal.UseVisualStyleBackColor = false;
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(73, 512);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 5;
			this.btnTest.Text = "test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
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
			this.btnConnect.Location = new System.Drawing.Point(446, 463);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(200, 50);
			this.btnConnect.TabIndex = 6;
			this.btnConnect.Text = "Verbinden";
			this.btnConnect.UseVisualStyleBackColor = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// PageStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.btnHostLocal);
			this.Controls.Add(this.btnHostPublic);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnJoin);
			this.Controls.Add(this.btnCreateRoom);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageStart";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PageStart_KeyDown);
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
		private void PageStart_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Console.WriteLine(e.KeyCode);
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			//MessageWordChoice.Open(this.Parent);

			//this.DrawOverlay();
		}

		
		//private void Control_Paint(object sender, PaintEventArgs e)
		//{
		//	var c = sender as Control;
		//	e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 0, 0, 0)), 0, 0, c.Width, c.Height);
		//	//e.Graphics.FillRectangle(System.Drawing.Brushes.AliceBlue, 0, 0, c.Width, c.Height);
		//}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			this.Parent.openPage(new PageConnect());
		}
	}
}
