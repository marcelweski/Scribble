using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public class PageRooms : Page
	{
		private DarkTheme.Button btnCreate;
		private DarkTheme.Button btnJoin;
		private ListView lvwRooms;
		private ColumnHeader colName;
		private ColumnHeader colPlayerCount;
		private DarkTheme.Label lblPassword;
		private DarkTheme.TextBox txtPassword;
		private DarkTheme.Label lblTitle;

		public PageRooms()
		{
			this.InitializeComponent();

			this.txtPassword.textBox.UseSystemPasswordChar = true;

			this.Load += PageRooms_Load;
			this.Closed += PageRooms_Closed;
			this.VisibleChanged += PageRooms_VisibleChanged;
		}

		private void InitializeComponent()
		{
			this.lblTitle = new DarkTheme.Label();
			this.btnCreate = new DarkTheme.Button();
			this.btnJoin = new DarkTheme.Button();
			this.lvwRooms = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			this.lblTitle.Size = new System.Drawing.Size(115, 45);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Räume";
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
			this.btnCreate.TabIndex = 2;
			this.btnCreate.Text = "Erstellen";
			this.btnCreate.UseVisualStyleBackColor = false;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnJoin
			// 
			this.btnJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnJoin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnJoin.Enabled = false;
			this.btnJoin.FlatAppearance.BorderSize = 0;
			this.btnJoin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnJoin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnJoin.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnJoin.ForeColor = System.Drawing.Color.White;
			this.btnJoin.Location = new System.Drawing.Point(274, 464);
			this.btnJoin.Name = "btnJoin";
			this.btnJoin.Size = new System.Drawing.Size(200, 50);
			this.btnJoin.TabIndex = 3;
			this.btnJoin.Text = "Beitreten";
			this.btnJoin.UseVisualStyleBackColor = false;
			this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
			// 
			// lvwRooms
			// 
			this.lvwRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvwRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPlayerCount});
			this.lvwRooms.FullRowSelect = true;
			this.lvwRooms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwRooms.Location = new System.Drawing.Point(68, 108);
			this.lvwRooms.MultiSelect = false;
			this.lvwRooms.Name = "lvwRooms";
			this.lvwRooms.Size = new System.Drawing.Size(406, 350);
			this.lvwRooms.TabIndex = 4;
			this.lvwRooms.UseCompatibleStateImageBehavior = false;
			this.lvwRooms.View = System.Windows.Forms.View.Details;
			this.lvwRooms.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwRooms_ItemSelectionChanged);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 330;
			// 
			// colPlayerCount
			// 
			this.colPlayerCount.Text = "Spieler";
			this.colPlayerCount.Width = 69;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.BackColor = System.Drawing.Color.Transparent;
			this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPassword.ForeColor = System.Drawing.Color.White;
			this.lblPassword.Location = new System.Drawing.Point(502, 107);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(72, 21);
			this.lblPassword.TabIndex = 16;
			this.lblPassword.Text = "Passwort";
			// 
			// txtPassword
			// 
			this.txtPassword.BackColor = System.Drawing.Color.White;
			this.txtPassword.Location = new System.Drawing.Point(506, 141);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = false;
			this.txtPassword.Size = new System.Drawing.Size(200, 29);
			this.txtPassword.TabIndex = 15;
			// 
			// PageRooms
			// 
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lvwRooms);
			this.Controls.Add(this.btnJoin);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageRooms";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageRooms_Load(object sender, EventArgs e)
		{
			if (!Client.Connected)
			{
				this.Parent.closeCurrentPage();
			}
			else
			{
				Client.ConnectionChanged += Client_ConnectionChanged;
				Client.ErrorOccurred += Client_ErrorOccurred;
				Client.ObjectReceived += Client_ObjectReceived;

				Client.send(new ChangeState { State = State.RoomChoice });
			}
		}
		private void PageRooms_Closed(object sender, EventArgs e)
		{
			Client.disconnect();
		}
		private void PageRooms_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				this.PageRooms_Load(sender, e);
			}
		}

		// Client Events
		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if (!e.Connected && e.Lost)
			{
				this.Parent.closeCurrentPage();
				MessageBox.Show("Die Verbindung zum Server wurde getrennt!");
			}
		}
		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
		}
		private void Client_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			var obj = e.Object;
			if (obj is RoomList)
			{
				var roomList = obj as RoomList;

				this.lvwRooms.Items.Clear();
				foreach (var item in roomList.Items)
				{
					this.lvwRooms.Items.Add(new ListViewItem(new string[] { item.Name, item.TotalPlayers.ToString() }));
				}
			}
			else if (obj is Success)
			{
				Success success = obj as Success;
				if (success.Job == Job.RoomJoin)
				{
					Client.ConnectionChanged -= Client_ConnectionChanged;
					Client.ErrorOccurred -= Client_ErrorOccurred;
					Client.ObjectReceived -= Client_ObjectReceived;

					this.Parent.openPage(new PageLobby { Host = false });

					this.btnJoin.Text = "Beitreten";
					this.btnJoin.Enabled = true;
				}
			}
			else if (obj is Error)
			{
				Error error = obj as Error;
				if (error.Job == Job.RoomJoin)
				{
					this.btnJoin.Text = "Beitreten";
					this.btnJoin.Enabled = true;

					MessageBox.Show("Das Passwort ist ungültig!");
				}
			}
		}

		// Control Events
		private void lvwRooms_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.btnJoin.Enabled = e.IsSelected;
		}
		private void btnCreate_Click(object sender, EventArgs e)
		{
			Client.ConnectionChanged -= Client_ConnectionChanged;
			Client.ErrorOccurred -= Client_ErrorOccurred;
			Client.ObjectReceived -= Client_ObjectReceived;

			this.Parent.openPage(new PageCreateRoom());
		}
		private void btnJoin_Click(object sender, EventArgs e)
		{
			this.btnJoin.Enabled = false;
			this.btnJoin.Text = "Authentifiziere...";

			Client.send(new JoinRoom { Name = this.lvwRooms.SelectedItems[0].SubItems[0].Text, Password = this.txtPassword.Text.GetHashCode() });
		}
	}
}
