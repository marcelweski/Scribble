using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public class PageCreateRoom : Page
	{
		private DarkTheme.Button btnCreate;
		private DarkTheme.TextBox txtName;
		private DarkTheme.Label lblName;
		private DarkTheme.Label lblPassword;
		private DarkTheme.TextBox txtPassword;
		private DarkTheme.Label lblTitle;

		public PageCreateRoom()
		{
			this.InitializeComponent();

			this.txtPassword.textBox.UseSystemPasswordChar = true;

			this.Load += PageCreateRoom_Load;
			this.Closed += PageCreateRoom_Closed;
		}

		private void InitializeComponent()
		{
			this.lblPassword = new DarkTheme.Label();
			this.txtPassword = new DarkTheme.TextBox();
			this.lblName = new DarkTheme.Label();
			this.txtName = new DarkTheme.TextBox();
			this.btnCreate = new DarkTheme.Button();
			this.lblTitle = new DarkTheme.Label();
			this.SuspendLayout();
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.BackColor = System.Drawing.Color.Transparent;
			this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPassword.ForeColor = System.Drawing.Color.White;
			this.lblPassword.Location = new System.Drawing.Point(64, 191);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(72, 21);
			this.lblPassword.TabIndex = 11;
			this.lblPassword.Text = "Passwort";
			// 
			// txtPassword
			// 
			this.txtPassword.BackColor = System.Drawing.Color.White;
			this.txtPassword.Location = new System.Drawing.Point(68, 219);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = false;
			this.txtPassword.Size = new System.Drawing.Size(200, 29);
			this.txtPassword.TabIndex = 10;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblName.ForeColor = System.Drawing.Color.White;
			this.lblName.Location = new System.Drawing.Point(64, 127);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(52, 21);
			this.lblName.TabIndex = 9;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Location = new System.Drawing.Point(68, 155);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = false;
			this.txtName.Size = new System.Drawing.Size(200, 29);
			this.txtName.TabIndex = 8;
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnCreate.FlatAppearance.BorderSize = 0;
			this.btnCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCreate.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnCreate.ForeColor = System.Drawing.Color.White;
			this.btnCreate.Location = new System.Drawing.Point(68, 464);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(200, 50);
			this.btnCreate.TabIndex = 3;
			this.btnCreate.Text = "Erstellen";
			this.btnCreate.UseVisualStyleBackColor = false;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 24F);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(60, 60);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(222, 45);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Raum erstellen";
			// 
			// PageCreateRoom
			// 
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageCreateRoom";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageCreateRoom_Load(object sender, EventArgs e)
		{
			Client.ConnectionChanged += Client_ConnectionChanged;
			Client.ErrorOccurred += Client_ErrorOccurred;
			Client.ObjectReceived += Client_ObjectReceived;

			Client.send(new ChangeState { State = State.RoomCreation });
		}
		private void PageCreateRoom_Closed(object sender, EventArgs e)
		{
			//Client.send(new ChangeState { State = State.RoomChoice });
		}

		// Client Events
		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if (!e.Connected && e.Lost)
			{
				this.Parent.closeCurrentPage();
				MessageBox.Show("Die Verbindung zum Server wurde getrennt!");
			}

			//if (client.Connected)
			//{
			//	string name = this.txtName.Text;
			//	int password = this.txtPassword.Text.GetHashCode();

			//	var request = new CreateRoomRequest { Name = name, Password = password };
			//	Client.send(request);
			//}
		}
		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			//Client.ErrorOccurred -= Client_ErrorOccurred;
			//Client.ConnectionChanged -= Client_ConnectionChanged;
			//Client.ObjectReceived -= Client_ObjectReceived;

			//MessageBox.Show(message);
			//this.btnCreate.Enabled = true;
		}
		private void Client_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			var obj = e.Object;
			if (obj is RandomRoomName)
			{
				RandomRoomName roomName = obj as RandomRoomName;
				this.txtName.Text = roomName.Name;
			}
			else if (obj is Success)
			{
				Success success = obj as Success;
				if (success.Job == Job.RoomCreation)
				{
					Client.ErrorOccurred -= Client_ErrorOccurred;
					Client.ConnectionChanged -= Client_ConnectionChanged;
					Client.ObjectReceived -= Client_ObjectReceived;

					this.Parent.closeCurrentPageAndOpenNewPage(new PageLobby { Host = true });
				}
			}
			else if (obj is Error)
			{
				Error error = obj as Error;
				if (error.Job == Job.RoomCreation)
				{
					MessageBox.Show("Dieser Name ist leider bereits vergeben!");

					this.btnCreate.Text = "Erstellen";
					this.btnCreate.Enabled = true;
				}
			}
		}

		// Control Events
		private void btnCreate_Click(object sender, EventArgs e)
		{
			string name = this.txtName.Text;
			if (name.Length == 0)
			{
				MessageBox.Show("Bitte einen gültigen Namen für den Raum angeben!");
				return;
			}

			int password = this.txtPassword.Text.GetHashCode();

			this.btnCreate.Enabled = false;
			this.btnCreate.Text = "Wird erstellt...";

			Client.send(new CreateRoom { Name = name, Password = password });
		}
	}
}
