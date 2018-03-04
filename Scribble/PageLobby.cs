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

		//private AdvancedNetworkLib.Server Server { get => (this.FindForm() as FrmMain).Server; }
		//private AdvancedNetworkLib.Client Client { get => (this.FindForm() as FrmMain).Client; }

		//private System.Drawing.Image image;


		//
		//private enum ERole
		//{
		//	Server,
		//	Client
		//}
		//private ERole role;

		//
		//private LobbyList lobbyList;

		//private class ClientUserData
		//{
		//	public string Name { get; set; }
		//	public bool Ready;
		//}

		public bool Host = false;
		//public string RoomName;
		//public string PlayerName;

		//private Random rand;
		//

		// General (used by server and client, any player)
		//

		public PageLobby()
		{
			this.InitializeComponent();

			this.Load += PageLobby_Load;
			this.Closed += PageLobby_Closed;



			//this.btnReady.Click += BtnReady_Click;

			//
			//this.lobbyList = new LobbyList();

			//this.rand = new Random((int)DateTime.Now.Ticks);
			////

			var imagelist = new System.Windows.Forms.ImageList();
			imagelist.ImageSize = new System.Drawing.Size(64, 64);
			imagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			imagelist.Images.Add(Properties.Resources.user_small_white);
			this.lvwPlayers.LargeImageList = imagelist;

			this.lvwPlayers.ItemSelectionChanged += LvwPlayers_ItemSelectionChanged;
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

		private void PageLobby_Load(object sender, EventArgs e)
		{
			Client.ErrorOccurred += Client_ErrorOccurred;
			Client.ConnectionChanged += Client_ConnectionChanged;
			Client.ObjectReceived += Client_ObjectReceived;

			Client.send(new ChangeState { State = this.Host ? State.LobbyReady : State.Lobby });

			this.btnKickPlayer.Visible = this.Host;
			this.btnChangeName.Visible = this.Host;
			this.btnReady.Text = this.Host ? "Starten" : "Bereit";

			//if (this.Server.Listening)
			//{
			//	this.role = ERole.Server;
			//	this.playerName = "Server";

			//	this.txtName.Text = this.playerName;

			//	this.updateLobbyList();
			//	this.Server.ClientsChanged += Server_ClientsChanged;
			//	this.Server.ObjectReceived += Server_ObjectReceived;
			//}
			//else
			//{
			//	this.role = ERole.Client;

			//	this.Client.ObjectReceived += Client_ObjectReceived;
			//}
		}

		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
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
			//if (obj is LobbyList)
			//{
			//	this.updateLobbyListControl(obj as LobbyList);
			//}
			//else if (obj is ErrorOld)
			//{
			//	var error = obj as ErrorOld;
			//	if (error.Job == EJob.PlayerNameChange)
			//	{
			//		this.btnReady.Enabled = true;
			//		this.txtName.Enabled = true;
			//		MessageBox.Show("Der Name ist leider schon vergeben!");
			//	}
			//}
			//else if (obj is SuccessOld)
			//{
			//	var job = (obj as SuccessOld).Job;
			//	if (job == EJob.PlayerNameChange)
			//	{
			//		if (this.Host)
			//		{
			//			//this.Parent.closeCurrentPageAndOpenNewPage(new PageGame());
			//			Client.send(new ChangeStateGame());
			//		}
			//	}
			//}
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
						MessageBox.Show("Du kannst nicht aleine spielen!");
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

		

		//private void Server_ObjectReceived(AdvancedNetworkLib.Client client, object obj)
		//{
		//	if (obj is PlayerName)
		//	{
		//		// check if name is free
		//		string newPlayerName = (obj as PlayerName).Data;
		//		string oldPlayerName = (client.UserData as ClientUserData).Name;

		//		List<string> playerNames = new List<string>(this.lobbyList.Names.Keys);
		//		playerNames.Remove(oldPlayerName);

		//		if (playerNames.Contains(newPlayerName))
		//		{
		//			client.send(new Error { Job = EJob.PlayerNameChange });
		//		}
		//		else
		//		{
		//			client.send(new Success { Job = EJob.PlayerNameChange });

		//			(client.UserData as ClientUserData).Name = newPlayerName;
		//			(client.UserData as ClientUserData).Ready = true;
		//			this.updateLobbyList();
		//			this.Server.sendToAll(this.lobbyList);
		//		}
		//	}
		//}

		//private void Client_ObjectReceived(AdvancedNetworkLib.Client client, object obj)
		//{
		//	if (obj is PlayerName)
		//	{
		//		this.playerName = this.txtName.Text = (obj as PlayerName).Data;
		//	}
		//	else if (obj is LobbyList)
		//	{
		//		this.updateLobbyListControl((obj as LobbyList));
		//	}
		//	else if (obj is Success)
		//	{
		//		var job = (obj as Success).Job;
		//		if (job == EJob.PlayerNameChange)
		//		{
		//			this.btnReady.Enabled = false;
		//		}
		//	}
		//	else if (obj is Error)
		//	{
		//		var job = (obj as Error).Job;
		//		if (job == EJob.PlayerNameChange)
		//		{
		//			this.btnReady.Enabled = true;
		//			this.txtName.Enabled = true;
		//			MessageBox.Show("Dieser Name ist bereits vergeben!");
		//		}
		//	}
		//	else if (obj is StartGame)
		//	{
		//		StartGame game = (obj as StartGame);
		//		this.Client.ObjectReceived -= this.Client_ObjectReceived;
		//		(this.Parent as FrmMain).openPage(new PageGame { PlayerName = this.playerName });
		//	}
		//}

		//private void updateLobbyList()
		//{
		//	this.lobbyList.Names.Clear();

		//	if (this.role == ERole.Server)
		//		this.lobbyList.Names.Add(this.playerName, true);

		//	foreach (var client in this.Server.Clients)
		//	{
		//		this.lobbyList.Names.Add((client.UserData as ClientUserData).Name, (client.UserData as ClientUserData).Ready);
		//	}

		//	// update control
		//	this.updateLobbyListControl(this.lobbyList);
		//}

		private void updateLobbyListControl(LobbyList lobbyList)
		{
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

		//private void Server_ClientsChanged(object sender)
		//{
		//	this.lvwPlayers.Items.Clear();

		//	// update new client userdata
		//	foreach (var client in this.Server.Clients)
		//	{
		//		if (client.UserData == null)
		//		{
		//			// give new client a random name
		//			ClientUserData userData = new ClientUserData();
		//			userData.Ready = false;
		//			while (this.lobbyList.Names.ContainsKey(userData.Name = $"Player{this.rand.Next(0, 999).ToString().PadLeft(3, '0')}")) { }

		//			client.UserData = userData;
		//			client.send(new PlayerName { Data = userData.Name });
		//		}
		//	}

		//	// update lobbylist
		//	this.updateLobbyList();

		//	this.Server.sendToAll(this.lobbyList);

		//}

		private void PageLobby_Closed(object sender, EventArgs e)
		{
			//Console.WriteLine("Closing lobby...");
			//this.Server?.stop();
			//this.Client?.disconnect();
			//Client.disconnect();
		}

		private void InitializeComponent()
		{
			this.lvwPlayers = new System.Windows.Forms.ListView();
			this.lblName = new DarkTheme.Label();
			this.txtName = new DarkTheme.TextBox();
			this.btnReady = new DarkTheme.Button();
			this.btnKickPlayer = new DarkTheme.Button();
			this.btnChangeName = new DarkTheme.Button();
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
			this.lvwPlayers.Location = new System.Drawing.Point(140, 58);
			this.lvwPlayers.MultiSelect = false;
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.Size = new System.Drawing.Size(820, 274);
			this.lvwPlayers.TabIndex = 1;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			// 
			// lblName
			// 
			this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Consolas", 12F);
			this.lblName.ForeColor = System.Drawing.Color.White;
			this.lblName.Location = new System.Drawing.Point(443, 384);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(45, 19);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Location = new System.Drawing.Point(447, 406);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = false;
			this.txtName.Size = new System.Drawing.Size(200, 29);
			this.txtName.TabIndex = 3;
			// 
			// btnReady
			// 
			this.btnReady.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnReady.FlatAppearance.BorderSize = 0;
			this.btnReady.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnReady.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReady.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnReady.ForeColor = System.Drawing.Color.White;
			this.btnReady.Location = new System.Drawing.Point(447, 441);
			this.btnReady.Name = "btnReady";
			this.btnReady.Size = new System.Drawing.Size(200, 50);
			this.btnReady.TabIndex = 4;
			this.btnReady.Text = "Bereit";
			this.btnReady.UseVisualStyleBackColor = false;
			this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
			// 
			// btnKickPlayer
			// 
			this.btnKickPlayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnKickPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.Enabled = false;
			this.btnKickPlayer.FlatAppearance.BorderSize = 0;
			this.btnKickPlayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnKickPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKickPlayer.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnKickPlayer.ForeColor = System.Drawing.Color.White;
			this.btnKickPlayer.Location = new System.Drawing.Point(653, 441);
			this.btnKickPlayer.Name = "btnKickPlayer";
			this.btnKickPlayer.Size = new System.Drawing.Size(200, 50);
			this.btnKickPlayer.TabIndex = 5;
			this.btnKickPlayer.Text = "Spieler rauswerfen";
			this.btnKickPlayer.UseVisualStyleBackColor = false;
			// 
			// btnChangeName
			// 
			this.btnChangeName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnChangeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatAppearance.BorderSize = 0;
			this.btnChangeName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnChangeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnChangeName.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnChangeName.ForeColor = System.Drawing.Color.White;
			this.btnChangeName.Location = new System.Drawing.Point(241, 441);
			this.btnChangeName.Name = "btnChangeName";
			this.btnChangeName.Size = new System.Drawing.Size(200, 50);
			this.btnChangeName.TabIndex = 6;
			this.btnChangeName.Text = "Namen ändern";
			this.btnChangeName.UseVisualStyleBackColor = false;
			this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
			// 
			// PageLobby
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnChangeName);
			this.Controls.Add(this.btnKickPlayer);
			this.Controls.Add(this.btnReady);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lvwPlayers);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageLobby";
			this.VisibleChanged += new System.EventHandler(this.PageLobby_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		//private bool checkIfEverybodyIsReady()
		//{
		//	foreach (var c in this.Server.Clients)
		//	{
		//		if (c.UserData != null && !(c.UserData as ClientUserData).Ready)
		//			return false;
		//	}
		//	return true;
		//}

		private void BtnReady_Clickasdsasda(object sender, EventArgs e)
		{
			//Client.send(new ChangeStateLobbyReady());
			//if (this.role == ERole.Server)
			//{
			//	// sending name request to server
			//	string name = this.txtName.Text;

			//	// check if name is valid
			//	if (name.Length == 0)
			//	{
			//		MessageBox.Show("Bitte einen Namen angeben!");
			//	}
			//	else
			//	{
			//		string oldPlayerName = this.playerName;

			//		List<string> playerNames = new List<string>(this.lobbyList.Names.Keys);
			//		playerNames.Remove(oldPlayerName);

			//		if (playerNames.Contains(name))
			//		{
			//			MessageBox.Show("Dieser Name ist leider schon vergeben!");
			//		}
			//		else
			//		{
			//			this.playerName = name;
			//			this.updateLobbyList();
			//			this.Server.sendToAll(this.lobbyList);

			//			// check if every player is ready
			//			if (!this.checkIfEverybodyIsReady())
			//			{
			//				MessageBox.Show("Es sind noch nicht alle Spieler bereit!");
			//			}
			//			else
			//			{
			//				// start game
			//				this.Server.sendToAll(new StartGame());
			//				(this.Parent as FrmMain).openPage(new PageGame { PlayerName = this.playerName });

			//				this.Server.ObjectReceived -= this.Server_ObjectReceived;
			//				this.Server.ClientsChanged -= this.Server_ClientsChanged;
			//			}
			//		}
			//	}
			//}
			//else
			//{
			//	// sending name request to server
			//	string name = this.txtName.Text;

			//	// check if name is valid
			//	if (name.Length == 0)
			//	{
			//		MessageBox.Show("Bitte einen Namen angeben!");
			//	}
			//	else
			//	{
			//		this.btnReady.Enabled = false;
			//		this.txtName.Enabled = false;
			//		Client.send(new PlayerName { Data = name });
			//	}
			//}

		}

		private void Server_ObjectReceived1(AdvancedNetworkLib.Client client, object obj)
		{
			//Console.WriteLine("BUMM BUMM");
		}

		private void btnExit_Click(object sender, EventArgs e)
		{

		}

		private void PageLobby_VisibleChanged(object sender, EventArgs e)
		{
			//if (this.Visible)
			//{
			//	this.Parent.closeCurrentPage();
			//	//	this.Server?.stop();
			//	//	this.Client?.disconnect();
			//}
			//else
			//{
			//	if (this.Server != null)
			//		this.Server.ConnectionsChanged += Server_ConnectionsChanged;
			//}
		}

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



		//private void Server_ConnectionsChanged(List<Client> clients)
		//{
		//	this.listView1.Items.Clear();
		//	foreach (var c in clients)
		//	{
		//		this.listView1.Items.Add(c.EndPoint.ToString());
		//	}
		//}
	}
}
