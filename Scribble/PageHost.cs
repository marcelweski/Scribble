using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	class PageHost : Page
	{
		private DarkTheme.Button btnStart;
		private DarkTheme.TextBox txtPort;
		private DarkTheme.Label lblPort;
		private DarkTheme.Label lblPassword;
		private DarkTheme.TextBox txtPassword;
		private DarkTheme.Label lblTitle;

		public PageHost()
		{
			this.InitializeComponent();

			this.txtPort.Text = "1234";
			this.txtPassword.Text = "secret";
			this.txtPassword.textBox.UseSystemPasswordChar = true;

			this.Closed += PageHost_Closed;
		}

		private void PageHost_Closed(object sender, EventArgs e)
		{
			//Server.stop();
		}

		private void InitializeComponent()
		{
			this.lblTitle = new DarkTheme.Label();
			this.btnStart = new DarkTheme.Button();
			this.txtPort = new DarkTheme.TextBox();
			this.lblPort = new DarkTheme.Label();
			this.lblPassword = new DarkTheme.Label();
			this.txtPassword = new DarkTheme.TextBox();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 24F);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(60, 60);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(208, 45);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Server hosten";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnStart.FlatAppearance.BorderSize = 0;
			this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStart.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnStart.ForeColor = System.Drawing.Color.White;
			this.btnStart.Location = new System.Drawing.Point(68, 464);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(200, 50);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Starten";
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// txtPort
			// 
			this.txtPort.BackColor = System.Drawing.Color.White;
			this.txtPort.Location = new System.Drawing.Point(68, 160);
			this.txtPort.Name = "txtPort";
			this.txtPort.ReadOnly = false;
			this.txtPort.Size = new System.Drawing.Size(100, 29);
			this.txtPort.TabIndex = 2;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.BackColor = System.Drawing.Color.Transparent;
			this.lblPort.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPort.ForeColor = System.Drawing.Color.White;
			this.lblPort.Location = new System.Drawing.Point(64, 127);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(38, 21);
			this.lblPort.TabIndex = 3;
			this.lblPort.Text = "Port";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.BackColor = System.Drawing.Color.Transparent;
			this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPassword.ForeColor = System.Drawing.Color.White;
			this.lblPassword.Location = new System.Drawing.Point(64, 201);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(72, 21);
			this.lblPassword.TabIndex = 16;
			this.lblPassword.Text = "Passwort";
			// 
			// txtPassword
			// 
			this.txtPassword.BackColor = System.Drawing.Color.White;
			this.txtPassword.Location = new System.Drawing.Point(68, 234);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = false;
			this.txtPassword.Size = new System.Drawing.Size(200, 29);
			this.txtPassword.TabIndex = 15;
			// 
			// PageHost
			// 
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageHost";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			ushort port;
			if (!ushort.TryParse(this.txtPort.Text, out port))
			{
				MessageBox.Show("Bitte einen gültigen Port angeben!");
			}
			else
			{
				this.btnStart.Enabled = false;
				this.btnStart.Text = "Wird gestartet...";

				Server.init(this.Parent);
				Server.StateChanged += Server_StateChanged;
				Server.ErrorOccurred += Server_ErrorOccurred;
				Server.PasswordHash = this.txtPassword.Text.GetHashCode();
				Server.start(port);
			}
		}

		private void Server_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);

			Server.StateChanged -= Server_StateChanged;
			Server.ErrorOccurred -= Server_ErrorOccurred;
			Server.stop();

			this.btnStart.Enabled = true;
			this.btnStart.Text = "Starten";
		}

		private void Server_StateChanged(object sender, AdvancedNetworkLib.StateChangedEventArgs e)
		{
			if (e.Listening)
			{
				Server.StateChanged -= Server_StateChanged;
				Server.ErrorOccurred -= Server_ErrorOccurred;

				this.btnStart.Enabled = true;
				this.btnStart.Text = "Starten";

				this.Parent.closeCurrentPageAndOpenNewPage(new PageHostOverview());
			}
		}
	}
}
