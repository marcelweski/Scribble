using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public class PageConnect : Page
	{
		private DarkTheme.TextBox txtPort;
		private DarkTheme.Label lblPort;
		private DarkTheme.Label lblHost;
		private DarkTheme.TextBox txtHost;
		private DarkTheme.Button btnConnect;
		private DarkTheme.Label lblPassword;
		private DarkTheme.TextBox txtPassword;
		private DarkTheme.Label lblTitle;

		public PageConnect()
		{
			this.InitializeComponent();

			this.txtHost.Text = "localhost";
			this.txtPort.Text = "1234";
			this.txtPassword.Text = "secret";
			this.txtPassword.textBox.UseSystemPasswordChar = true;

			this.Closed += PageConnect_Closed;
		}

		private void InitializeComponent()
		{
			this.lblTitle = new DarkTheme.Label();
			this.txtPort = new DarkTheme.TextBox();
			this.lblPort = new DarkTheme.Label();
			this.lblHost = new DarkTheme.Label();
			this.txtHost = new DarkTheme.TextBox();
			this.btnConnect = new DarkTheme.Button();
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
			this.lblTitle.Size = new System.Drawing.Size(381, 45);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Zu einen Server verbinden";
			// 
			// txtPort
			// 
			this.txtPort.BackColor = System.Drawing.Color.White;
			this.txtPort.Location = new System.Drawing.Point(274, 158);
			this.txtPort.Name = "txtPort";
			this.txtPort.ReadOnly = false;
			this.txtPort.Size = new System.Drawing.Size(100, 29);
			this.txtPort.TabIndex = 11;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.BackColor = System.Drawing.Color.Transparent;
			this.lblPort.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPort.ForeColor = System.Drawing.Color.White;
			this.lblPort.Location = new System.Drawing.Point(270, 124);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(38, 21);
			this.lblPort.TabIndex = 10;
			this.lblPort.Text = "Port";
			// 
			// lblHost
			// 
			this.lblHost.AutoSize = true;
			this.lblHost.BackColor = System.Drawing.Color.Transparent;
			this.lblHost.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblHost.ForeColor = System.Drawing.Color.White;
			this.lblHost.Location = new System.Drawing.Point(64, 124);
			this.lblHost.Name = "lblHost";
			this.lblHost.Size = new System.Drawing.Size(42, 21);
			this.lblHost.TabIndex = 9;
			this.lblHost.Text = "Host";
			// 
			// txtHost
			// 
			this.txtHost.BackColor = System.Drawing.Color.White;
			this.txtHost.Location = new System.Drawing.Point(68, 158);
			this.txtHost.Name = "txtHost";
			this.txtHost.ReadOnly = false;
			this.txtHost.Size = new System.Drawing.Size(200, 29);
			this.txtHost.TabIndex = 8;
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnConnect.FlatAppearance.BorderSize = 0;
			this.btnConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConnect.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnConnect.ForeColor = System.Drawing.Color.White;
			this.btnConnect.Location = new System.Drawing.Point(68, 464);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(200, 50);
			this.btnConnect.TabIndex = 12;
			this.btnConnect.Text = "Verbinden";
			this.btnConnect.UseVisualStyleBackColor = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.BackColor = System.Drawing.Color.Transparent;
			this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPassword.ForeColor = System.Drawing.Color.White;
			this.lblPassword.Location = new System.Drawing.Point(64, 200);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(72, 21);
			this.lblPassword.TabIndex = 14;
			this.lblPassword.Text = "Passwort";
			// 
			// txtPassword
			// 
			this.txtPassword.BackColor = System.Drawing.Color.White;
			this.txtPassword.Location = new System.Drawing.Point(68, 234);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = false;
			this.txtPassword.Size = new System.Drawing.Size(200, 29);
			this.txtPassword.TabIndex = 13;
			// 
			// PageConnect
			// 
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.lblHost);
			this.Controls.Add(this.txtHost);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageConnect";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageConnect_Closed(object sender, EventArgs e)
		{
		}

		// Client Events
		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if (e.Connected)
			{
				this.btnConnect.Text = "Authentifiziere...";

				(sender as AdvancedNetworkLib.Client).send(new ServerPassword { Hash = this.txtPassword.Text.GetHashCode() });
			}
		}
		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);

			this.btnConnect.Text = "Verbinden";
			this.btnConnect.Enabled = true;

			Client.ConnectionChanged -= Client_ConnectionChanged;
			Client.ErrorOccurred -= Client_ErrorOccurred;
			Client.ObjectReceived -= Client_ObjectReceived;
			Client.disconnect();
		}
		private void Client_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			var obj = e.Object;

			if (obj is Success)
			{
				var success = obj as Success;

				if (success.Job == Job.AcceptPassword)
				{
					Client.ConnectionChanged -= Client_ConnectionChanged;
					Client.ErrorOccurred -= Client_ErrorOccurred;
					Client.ObjectReceived -= Client_ObjectReceived;

					this.Parent.closeCurrentPageAndOpenNewPage(new PageRooms());
				}
			}
			else if (obj is Error)
			{
				var error = obj as Error;

				if (error.Job == Job.AcceptPassword)
				{
					MessageBox.Show("Das Passwort ist ungültig!");

					this.btnConnect.Text = "Verbinden";
					this.btnConnect.Enabled = true;

					Client.ConnectionChanged -= Client_ConnectionChanged;
					Client.ErrorOccurred -= Client_ErrorOccurred;
					Client.ObjectReceived -= Client_ObjectReceived;
					Client.disconnect();
				}
			}
		}

		// Control Events
		private void btnConnect_Click(object sender, EventArgs e)
		{
			string host = this.txtHost.Text;
			if (host.Length == 0)
			{
				MessageBox.Show("Bitte einen gültigen Host angeben!");
				return;
			}

			ushort port;
			if (!ushort.TryParse(this.txtPort.Text, out port))
			{
				MessageBox.Show("Bitte einen gültigen Port angeben!");
				return;
			}

			this.btnConnect.Enabled = false;
			this.btnConnect.Text = "Verbindung wird aufgebaut...";

			Client.init(this.Parent);
			Client.ConnectionChanged += Client_ConnectionChanged;
			Client.ErrorOccurred += Client_ErrorOccurred;
			Client.ObjectReceived += Client_ObjectReceived;
			Client.connect(host, port);
		}
	}
}
