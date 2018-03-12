using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	class PageLobby : Page
	{
		private DarkTheme.Label lblName;
		private DarkTheme.TextBox txtName;
		private DarkTheme.Button btnReady;
		private System.Windows.Forms.ListView lvwPlayers;
		private DarkTheme.Button btnKickPlayer;
		private DarkTheme.Button btnChangeName;
		private DarkTheme.Label lblRoomName;
		public bool Host = false;

		public PageLobby()
		{
			this.InitializeComponent();

			this.Load += PageLobby_Load;
			this.Closed += PageLobby_Closed;

			var imagelist = new ImageList();
			imagelist.ImageSize = new System.Drawing.Size(64, 64);
			imagelist.ColorDepth = ColorDepth.Depth32Bit;
			imagelist.Images.Add(Properties.Resources.user_small_white);
			this.lvwPlayers.LargeImageList = imagelist;
			this.lvwPlayers.ItemSelectionChanged += LvwPlayers_ItemSelectionChanged;
		}

		private void InitializeComponent()
		{
			this.lvwPlayers = new System.Windows.Forms.ListView();
			this.lblName = new DarkTheme.Label();
			this.txtName = new DarkTheme.TextBox();
			this.btnReady = new DarkTheme.Button();
			this.btnKickPlayer = new DarkTheme.Button();
			this.btnChangeName = new DarkTheme.Button();
			this.lblRoomName = new DarkTheme.Label();
			this.SuspendLayout();
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lvwPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvwPlayers.Font = new System.Drawing.Font("Consolas", 9F);
			this.lvwPlayers.ForeColor = System.Drawing.Color.White;
			this.lvwPlayers.Location = new System.Drawing.Point(68, 107);
			this.lvwPlayers.MultiSelect = false;
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.Size = new System.Drawing.Size(960, 274);
			this.lvwPlayers.TabIndex = 1;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Consolas", 12F);
			this.lblName.ForeColor = System.Drawing.Color.White;
			this.lblName.Location = new System.Drawing.Point(64, 384);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(45, 19);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Location = new System.Drawing.Point(68, 406);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = false;
			this.txtName.Size = new System.Drawing.Size(200, 29);
			this.txtName.TabIndex = 3;
			// 
			// btnReady
			// 
			this.btnReady.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnReady.FlatAppearance.BorderSize = 0;
			this.btnReady.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnReady.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReady.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnReady.ForeColor = System.Drawing.Color.White;
			this.btnReady.Location = new System.Drawing.Point(68, 441);
			this.btnReady.Name = "btnReady";
			this.btnReady.Size = new System.Drawing.Size(200, 50);
			this.btnReady.TabIndex = 4;
			this.btnReady.Text = "Bereit";
			this.btnReady.UseVisualStyleBackColor = false;
			this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
			// 
			// btnKickPlayer
			// 
			this.btnKickPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnKickPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.Enabled = false;
			this.btnKickPlayer.FlatAppearance.BorderSize = 0;
			this.btnKickPlayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKickPlayer.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnKickPlayer.ForeColor = System.Drawing.Color.White;
			this.btnKickPlayer.Location = new System.Drawing.Point(828, 396);
			this.btnKickPlayer.Name = "btnKickPlayer";
			this.btnKickPlayer.Size = new System.Drawing.Size(200, 50);
			this.btnKickPlayer.TabIndex = 5;
			this.btnKickPlayer.Text = "Spieler rauswerfen";
			this.btnKickPlayer.UseVisualStyleBackColor = false;
			// 
			// btnChangeName
			// 
			this.btnChangeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnChangeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatAppearance.BorderSize = 0;
			this.btnChangeName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnChangeName.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnChangeName.ForeColor = System.Drawing.Color.White;
			this.btnChangeName.Location = new System.Drawing.Point(274, 441);
			this.btnChangeName.Name = "btnChangeName";
			this.btnChangeName.Size = new System.Drawing.Size(200, 50);
			this.btnChangeName.TabIndex = 6;
			this.btnChangeName.Text = "Namen ändern";
			this.btnChangeName.UseVisualStyleBackColor = false;
			this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
			// 
			// lblRoomName
			// 
			this.lblRoomName.AutoSize = true;
			this.lblRoomName.BackColor = System.Drawing.Color.Transparent;
			this.lblRoomName.Font = new System.Drawing.Font("Segoe UI Semilight", 24F);
			this.lblRoomName.ForeColor = System.Drawing.Color.White;
			this.lblRoomName.Location = new System.Drawing.Point(60, 60);
			this.lblRoomName.Name = "lblRoomName";
			this.lblRoomName.Size = new System.Drawing.Size(176, 45);
			this.lblRoomName.TabIndex = 7;
			this.lblRoomName.Text = "Raumname";
			// 
			// PageLobby
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblRoomName);
			this.Controls.Add(this.btnChangeName);
			this.Controls.Add(this.btnKickPlayer);
			this.Controls.Add(this.btnReady);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lvwPlayers);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageLobby";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageLobby_Load(object sender, EventArgs e)
		{
			Client.ErrorOccurred += Client_ErrorOccurred;
			Client.ConnectionChanged += Client_ConnectionChanged;
			Client.ObjectReceived += Client_ObjectReceived;

			Client.send(new ChangeState { State = this.Host ? State.LobbyReady : State.Lobby });

			this.btnKickPlayer.Visible = this.Host;
			this.btnChangeName.Visible = this.Host;
			this.btnReady.Text = this.Host ? "Starten" : "Bereit";
		}
		private void PageLobby_Closed(object sender, EventArgs e)
		{
			Client.ConnectionChanged -= Client_ConnectionChanged;
			Client.ErrorOccurred -= Client_ErrorOccurred;
			Client.ObjectReceived -= Client_ObjectReceived;
		}

		// Client Events
		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.StackTrace+"\n"+e.Exception.Message);
		}
		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if (!e.Connected)
			{
				this.Parent.closeCurrentPage();
				MessageBox.Show("Der Server ist nicht mehr erreichbar!");
			}
		}
		private void Client_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			var obj = e.Object;

			if (obj is RandomPlayerName)
			{
				this.txtName.Text = (obj as RandomPlayerName).Name;
			}
			else if (obj is LobbyList)
			{
				LobbyList lobbyList = obj as LobbyList;
				this.updateLobbyListControl(lobbyList);
			}
			else if (obj is Success)
			{
				var success = obj as Success;
				if (success.Job == Job.NameChange)
				{
					if (this.Host)
					{
						this.btnChangeName.Text = "Namen ändern";
						this.btnChangeName.Enabled = true;
					}
					else
					{
						this.btnReady.Text = "Bereit";
					}
				}
				else if (success.Job == Job.GameStart)
				{
					Client.ConnectionChanged -= Client_ConnectionChanged;
					Client.ErrorOccurred -= Client_ErrorOccurred;
					Client.ObjectReceived -= Client_ObjectReceived;

					this.Parent.closeCurrentPageAndOpenNewPage(new PageGame(new ClientUserData { Host = true, PlayerName = this.txtName.Text }));
				}
			}
			else if (obj is Error)
			{
				var error = obj as Error;
				if (error.Job == Job.NameChange)
				{
					MessageBox.Show("Dieser Name ist bereits vergeben!");

					if (this.Host)
					{
						this.btnChangeName.Text = "Namen ändern";
						this.btnChangeName.Enabled = true;
					}
					else
					{
						this.btnReady.Text = "Bereit";
						this.btnReady.Enabled = true;
					}
				}
				else if (error.Job == Job.GameStart)
				{
					if (this.lvwPlayers.Items.Count == 1)
					{
						MessageBox.Show("Du kannst nicht alleine spielen!");
					}
					else
					{
						MessageBox.Show("Es sind noch nicht alle Spieler bereit!");
					}

					this.btnReady.Text = "Starten";
					this.btnReady.Enabled = true;
				}
			}
			else if (obj is StartGame)
			{
				Client.ConnectionChanged -= Client_ConnectionChanged;
				Client.ErrorOccurred -= Client_ErrorOccurred;
				Client.ObjectReceived -= Client_ObjectReceived;

				this.Parent.closeCurrentPageAndOpenNewPage(new PageGame(new ClientUserData { Host = false, PlayerName = this.txtName.Text }));
			}
		}

		// Control Events
		private void btnReady_Click(object sender, EventArgs e)
		{
			if (this.txtName.Text.Length == 0)
			{
				MessageBox.Show("Bitte gebe einen gültigen Namen an!");
			}
			else
			{
				if (this.Host)
				{
					// check if every player is ready
					this.btnReady.Enabled = false;
					this.btnReady.Text = "Überprüfe...";
					Client.send(new StartGame());
				}
				else
				{
					this.btnReady.Enabled = false;
					this.btnReady.Text = "Überprüfe...";
					Client.send(new ChangeState { State = State.LobbyReady, Data = this.txtName.Text });
				}
			}
		}
		private void btnChangeName_Click(object sender, EventArgs e)
		{
			if (this.txtName.Text.Length == 0)
			{
				MessageBox.Show("Bitte gebe einen gültigen Namen an!");
			}
			else
			{
				this.btnChangeName.Enabled = false;
				this.btnChangeName.Text = "Überprüfe...";
				Client.send(new ChangeState { State = State.LobbyReady, Data = this.txtName.Text });
			}
		}
		private void LvwPlayers_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
		{
			if (this.Host)
			{
				this.btnKickPlayer.Enabled = e.IsSelected && e.ItemIndex != 0;
			}
			else
			{
				e.Item.Selected = false;
			}
		}

		// Private Methods
		private void updateLobbyListControl(LobbyList lobbyList)
		{
			// update room name
			this.lblRoomName.Text = lobbyList.RoomName;

			this.lvwPlayers.Items.Clear();
			foreach (var i in lobbyList.Items)
			{
				var tileSize = this.lvwPlayers.TileSize;

				var item = new ListViewItem(i.PlayerName, 0);
				if (i.State == State.LobbyReady)
				{
					item.Font = new System.Drawing.Font("Consolas", 10.0f, System.Drawing.FontStyle.Underline);
				}
				else if (i.State == State.Game)
				{
					item.Font = new System.Drawing.Font("Consolas", 10.0f, System.Drawing.FontStyle.Underline);
					item.ForeColor = System.Drawing.Color.LightBlue;
				}
				//item.Position = new System.Drawing.Point((this.lvwPlayers.Width/2) - (tileSize.Width/2), (this.lvwPlayers.Height / 2) - (tileSize.Height / 2));
				//item.Position = new System.Drawing.Point(100, 100);
				this.lvwPlayers.Items.Add(item);
			}
		}
	}
}
