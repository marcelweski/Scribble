using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scribble
{
	public class PageHostOverview : Page
	{
		private DarkTheme.Button btnStop;
		private ListView lvwRooms;
		private ColumnHeader colRoomName;
		private ColumnHeader colPlayersInLobby;
		private ColumnHeader colPlayersInGame;
		private DarkTheme.Label lblInfo;
		private DarkTheme.Label lblClientCount;
		private DarkTheme.Label lblPlayerLobbyCount;
		private DarkTheme.Label lblPlayerGameCount;
		private DarkTheme.Label lblHostCount;
		private DarkTheme.Label lblTitle;

		private int roundDuration = 60; // in seconds

		////////////////////////////////////////////////////////////////////////////////////
		private List<RoomInfo> rooms;
		private ColumnHeader colSpieler;

		private class WordListItem
		{
			public string Word;
			public int DownVotes;
		}
		private List<WordListItem> wordList;
		////////////////////////////////////////////////////////////////////////////////////

		public PageHostOverview()
		{
			this.InitializeComponent();

			this.Load += PageHostOverview_Load;
			this.Closed += PageHostOverview_Closed;

			this.lblInfo.Text = $"Denke daran den Port {Server.Port} mit dem TCP-Protokoll auf deinem Router freizugeben!";

			this.rooms = new List<RoomInfo>();
			this.wordList = this.loadWordListFromFile("../../Resources/wordlists/de.txt");
		}

		private void InitializeComponent()
		{
			this.lblTitle = new DarkTheme.Label();
			this.btnStop = new DarkTheme.Button();
			this.lvwRooms = new System.Windows.Forms.ListView();
			this.colRoomName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayersInLobby = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayersInGame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblInfo = new DarkTheme.Label();
			this.lblClientCount = new DarkTheme.Label();
			this.lblPlayerLobbyCount = new DarkTheme.Label();
			this.lblPlayerGameCount = new DarkTheme.Label();
			this.lblHostCount = new DarkTheme.Label();
			this.colSpieler = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			this.lblTitle.Size = new System.Drawing.Size(243, 45);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Server Übersicht";
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnStop.FlatAppearance.BorderSize = 0;
			this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStop.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnStop.ForeColor = System.Drawing.Color.White;
			this.btnStop.Location = new System.Drawing.Point(68, 464);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(200, 50);
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "Stoppen";
			this.btnStop.UseVisualStyleBackColor = false;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// lvwRooms
			// 
			this.lvwRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvwRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRoomName,
            this.colPlayersInLobby,
            this.colPlayersInGame,
            this.colSpieler});
			this.lvwRooms.Location = new System.Drawing.Point(68, 138);
			this.lvwRooms.Name = "lvwRooms";
			this.lvwRooms.Size = new System.Drawing.Size(403, 320);
			this.lvwRooms.TabIndex = 3;
			this.lvwRooms.UseCompatibleStateImageBehavior = false;
			this.lvwRooms.View = System.Windows.Forms.View.Details;
			// 
			// colRoomName
			// 
			this.colRoomName.Text = "Raumname";
			this.colRoomName.Width = 120;
			// 
			// colPlayersInLobby
			// 
			this.colPlayersInLobby.Text = "Spieler in Lobby";
			this.colPlayersInLobby.Width = 90;
			// 
			// colPlayersInGame
			// 
			this.colPlayersInGame.Text = "Spieler im Spiel";
			this.colPlayersInGame.Width = 85;
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.BackColor = System.Drawing.Color.Transparent;
			this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblInfo.ForeColor = System.Drawing.Color.White;
			this.lblInfo.Location = new System.Drawing.Point(64, 105);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(71, 21);
			this.lblInfo.TabIndex = 4;
			this.lblInfo.Text = "Port-Info";
			// 
			// lblClientCount
			// 
			this.lblClientCount.AutoSize = true;
			this.lblClientCount.BackColor = System.Drawing.Color.Transparent;
			this.lblClientCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblClientCount.ForeColor = System.Drawing.Color.White;
			this.lblClientCount.Location = new System.Drawing.Point(490, 138);
			this.lblClientCount.Name = "lblClientCount";
			this.lblClientCount.Size = new System.Drawing.Size(161, 21);
			this.lblClientCount.TabIndex = 5;
			this.lblClientCount.Text = "Verbundene Clients: 0";
			// 
			// lblPlayerLobbyCount
			// 
			this.lblPlayerLobbyCount.AutoSize = true;
			this.lblPlayerLobbyCount.BackColor = System.Drawing.Color.Transparent;
			this.lblPlayerLobbyCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPlayerLobbyCount.ForeColor = System.Drawing.Color.White;
			this.lblPlayerLobbyCount.Location = new System.Drawing.Point(490, 173);
			this.lblPlayerLobbyCount.Name = "lblPlayerLobbyCount";
			this.lblPlayerLobbyCount.Size = new System.Drawing.Size(138, 21);
			this.lblPlayerLobbyCount.TabIndex = 6;
			this.lblPlayerLobbyCount.Text = "Spieler in Lobby: 0";
			// 
			// lblPlayerGameCount
			// 
			this.lblPlayerGameCount.AutoSize = true;
			this.lblPlayerGameCount.BackColor = System.Drawing.Color.Transparent;
			this.lblPlayerGameCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPlayerGameCount.ForeColor = System.Drawing.Color.White;
			this.lblPlayerGameCount.Location = new System.Drawing.Point(490, 208);
			this.lblPlayerGameCount.Name = "lblPlayerGameCount";
			this.lblPlayerGameCount.Size = new System.Drawing.Size(134, 21);
			this.lblPlayerGameCount.TabIndex = 7;
			this.lblPlayerGameCount.Text = "Spieler im Spiel: 0";
			// 
			// lblHostCount
			// 
			this.lblHostCount.AutoSize = true;
			this.lblHostCount.BackColor = System.Drawing.Color.Transparent;
			this.lblHostCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblHostCount.ForeColor = System.Drawing.Color.White;
			this.lblHostCount.Location = new System.Drawing.Point(490, 243);
			this.lblHostCount.Name = "lblHostCount";
			this.lblHostCount.Size = new System.Drawing.Size(65, 21);
			this.lblHostCount.TabIndex = 8;
			this.lblHostCount.Text = "Hosts: 0";
			// 
			// colSpieler
			// 
			this.colSpieler.Text = "Spieler";
			this.colSpieler.Width = 50;
			// 
			// PageHostOverview
			// 
			this.Controls.Add(this.lblHostCount);
			this.Controls.Add(this.lblPlayerGameCount);
			this.Controls.Add(this.lblPlayerLobbyCount);
			this.Controls.Add(this.lblClientCount);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.lvwRooms);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.lblTitle);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageHostOverview";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageHostOverview_Load(object sender, EventArgs e)
		{
			Server.StateChanged += Server_StateChanged;
			Server.ErrorOccurred += Server_ErrorOccurred;
			Server.ClientsChanged += Server_ClientsChanged;
			Server.ObjectReceived += Server_ObjectReceived;
		}
		private void PageHostOverview_Closed(object sender, EventArgs e)
		{
			Server.StateChanged -= Server_StateChanged;
			Server.ErrorOccurred -= Server_ErrorOccurred;
			Server.ClientsChanged -= Server_ClientsChanged;
			Server.ObjectReceived -= Server_ObjectReceived;

			Server.stop();
		}

		// Server Events
		private void Server_StateChanged(object sender, AdvancedNetworkLib.StateChangedEventArgs e)
		{
			
		}
		private void Server_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.StackTrace+"\n"+e.Exception.Message);
		}
		private void Server_ClientsChanged(object sender, AdvancedNetworkLib.ClientsChangedEventArgs e)
		{
			this.lblClientCount.Text = $"Verbundene Clients: {e.Clients.Count()}";

			// TODO: if host leaves room, set new host

			// update roominfos
			lock (this.rooms)
			{
				this.sendRoomListToAll();
			}

			// update and send lobbylists
			lock (this.rooms)
			{
				foreach (var roomInfo in this.rooms)
					this.sendLobbyListToAll(roomInfo);
			}

			// update all ranklists
			lock (this.rooms)
			{
				foreach (var roomInfo in this.rooms)
					this.sendRankListToAll(roomInfo);
			}
		}
		private void Server_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			AdvancedNetworkLib.Client client = (sender as AdvancedNetworkLib.Client);
			ClientUserData userData = client.UserData as ClientUserData;
			RoomInfo roomInfo = this.rooms.Find(c => c.Name == userData.RoomName);

			var obj = e.Object;
			if (obj is ServerPassword)
			{
				ServerPassword serverPassword = obj as ServerPassword;

				if (serverPassword.Hash == Server.PasswordHash)
				{
					client.send(new Success { Job = Job.AcceptPassword });

					client.UserData = new ClientUserData
					{
						State = State.None
					};

				}
				else
				{
					client.send(new Error { Job = Job.AcceptPassword });
				}
			}
			else if (obj is ChangeState)
			{
				var oldState = userData.State;
				ChangeState changeState = obj as ChangeState;
				userData.State = changeState.State;

				if (userData.State == State.RoomChoice)
				{
					userData.PlayerName = string.Empty;
					userData.RoomName = string.Empty;
					userData.Password = 0;
					userData.IsDrawing = false;
					userData.Points = 0;

					// if player was in lobby
					if (oldState == State.Lobby || oldState == State.LobbyReady)
					{
						this.sendLobbyListToAll(roomInfo);
						this.sendRoomListToAll();
					}

					// if player was in game
					if (oldState == State.Game)
					{
						// check if only one player is left in game
						var playersInGame = this.getPlayersInGame(roomInfo);
						if (playersInGame.Count() == 1)
						{
							// kick last player in game
							playersInGame.First().send(new KickedNoMorePlayer());
						}
						else if (playersInGame.Count() > 1)
						{
							// update ranklist for players in game
							this.sendRankListToAll(roomInfo);
						}

						// update overview
						this.updateOverview();
					}

					if (userData.Host)
					{
						userData.Host = false;

						// update and send roomlist
						this.sendRoomListToAll();
					
					}
					else
					{
						// send roomlist
						this.sendRoomList(client);
					}
				}
				else if (userData.State == State.RoomCreation)
				{
					string randomRoomName = string.Empty;

					// generate random room name
					// TODO: better performance when checking host boolean first
					int i = 1000;
					while (Server.Clients.Count(c => (c.UserData as ClientUserData).RoomName == (randomRoomName = $"Room{Server.rand.Next(0, 999).ToString().PadLeft(3, '0')}") && (c.UserData as ClientUserData).Host) > 0 && i --> 0)
					{ }

					client.send(new RandomRoomName { Name = randomRoomName });
				}
				else if (userData.State == State.LobbyReady || userData.State == State.Lobby)
				{
					// TODO: update roomlist
					if (userData.PlayerName == string.Empty)
					{
						string randomPlayerName = string.Empty;

						// generate random room name
						// TODO: better performance
						int i = 1000;
						while (Server.Clients.Count(c => (c.UserData as ClientUserData).PlayerName == (randomPlayerName = $"Player{Server.rand.Next(0, 999).ToString().PadLeft(3, '0')}") && (c.UserData as ClientUserData).RoomName == userData.RoomName) > 0 && i-- > 0)
						{ }

						userData.PlayerName = randomPlayerName;

						client.send(new RandomPlayerName { Name = randomPlayerName });

						// update and send lobbylist
						this.sendLobbyListToAll(roomInfo);
					}
					else
					{
						string newPlayerName = changeState.Data as string;

						// check if any player has already the same name
						int playerWithSameName = Server.Clients.Count(c => c.UserData != null && c != client && (c.UserData as ClientUserData).RoomName == userData.RoomName && (c.UserData as ClientUserData).PlayerName == newPlayerName);

						if (playerWithSameName > 0)
						{
							client.send(new Error { Job = Job.NameChange });
						}
						else
						{
							// name is free and will be used
							userData.PlayerName = newPlayerName;

							if (userData.Host)
							{
								client.send(new Success { Job = Job.NameChange });
							}
							else
							{
								bool hostIsPlaying = Server.Clients.Count(c => c.UserData != null && (c.UserData as ClientUserData).RoomName == userData.RoomName && (c.UserData as ClientUserData).Host && (c.UserData as ClientUserData).State == State.Game) > 0;

								client.send(new Success { Job = hostIsPlaying ? Job.GameStart : Job.NameChange });
							}

							// update and send lobbylist
							this.sendLobbyListToAll(roomInfo);
						}
					}

					// update and send roomlist
					this.sendRoomListToAll();
				}
				else if (userData.State == State.Game)
				{
					if (userData.Host)
					{
						// send ranklist
						this.sendRankListToAll(roomInfo);

						// tell other players that the game starts
						var clients = Server.Clients.Where(c => c.UserData != null && (c.UserData as ClientUserData).RoomName == userData.RoomName && (c.UserData as ClientUserData).State == State.LobbyReady);
						this.sendObjectToPlayers(clients, new StartGame());
					}
					else
					{
						// check if player joins later
						if (this.rooms.Exists(c => c.Name == userData.RoomName && c.Started))
						{
							// this player joins later
							// send ranklist
							this.sendRankListToAll(roomInfo);

							// TODO: send last drawing
						}
						else
						{
							var playersInLobby = Server.Clients.Count(c =>
							{
								var u = c.UserData as ClientUserData;
								return u != null && u.RoomName == userData.RoomName && u.State == State.LobbyReady;
							});
							if (playersInLobby == 0)
							{
								// start game for the first game
								roomInfo.Started = true;

								// start first round
								this.startRound(roomInfo);
							}
						}
					}

					this.sendRoomListToAll();
				}
			}
			else if (obj is CreateRoom)
			{
				CreateRoom createRoom = obj as CreateRoom;

				int roomsWithSameName = Server.Clients.Count(c => c.UserData != null && (c.UserData as ClientUserData).RoomName == createRoom.Name);
				if (roomsWithSameName > 0)
				{
					client.send(new Error { Job = Job.RoomCreation });
				}
				else
				{
					userData.RoomName = createRoom.Name;
					userData.Password = createRoom.Password;
					userData.Host = true;

					// create room list entry
					var item = new RoomInfo
					{
						Name = userData.RoomName,
						Started = false,
						TotalRoundCount = 5,
					};
					lock (this.rooms)
					{
						this.rooms.Add(item);
					}

					client.send(new Success { Job = Job.RoomCreation });
				}
			}
			else if (obj is JoinRoom)
			{
				JoinRoom joinRoom = obj as JoinRoom;

				// check password
				bool passwordIsValid = Server.Clients.Count(c =>
				{
					var u = c.UserData as ClientUserData;
					return (u != null && u.RoomName == joinRoom.Name && u.Password == joinRoom.Password);

				}) > 0;

				if (!passwordIsValid)
				{
					client.send(new Error { Job = Job.RoomJoin });
				}
				else
				{
					userData.RoomName = joinRoom.Name;
					userData.Password = joinRoom.Password;
					userData.Host = false;

					client.send(new Success { Job = Job.RoomJoin });
				}

				this.updateOverview();
			}
			else if (obj is StartGame)
			{
				int playerCount = Server.Clients.Count(c => c.UserData != null && (c.UserData as ClientUserData).RoomName == userData.RoomName);
				int playerCountReady = Server.Clients.Count(c => c.UserData != null && (c.UserData as ClientUserData).RoomName == userData.RoomName && (c.UserData as ClientUserData).State == State.LobbyReady);
				if (playerCount != playerCountReady || playerCount < 2)
				{
					client.send(new Error { Job = Job.GameStart });
				}
				else
				{
					client.send(new Success { Job = Job.GameStart });
				}
			}
			else if (obj is ChatMessage)
			{
				var chatMessage = obj as ChatMessage;
				chatMessage.PlayerName = userData.PlayerName;

				// get all clients in the same room and that are playing
				var players = this.getPlayersInGame(roomInfo);

				// check if entered word is equal to searched word
				bool foundWord = (roomInfo.CurrentWord == chatMessage.Text);

				if (foundWord && (roomInfo.RoundInfo.PlayerTimes.ContainsKey(userData.PlayerName) || userData.IsDrawing))
				{
					client.send(new WhatDoYouWantInfo());
				}
				else
				{
					var foundWordInfo = foundWord ? new FoundWordInfo { PlayerName = userData.PlayerName } : null;

					if (foundWord)
					{
						// give points
						roomInfo.RoundInfo.PlayerTimes.Add(userData.PlayerName, 500 * ((players.Count() - 1) - roomInfo.RoundInfo.PlayerTimes.Count));
					}

					foreach (var player in players)
					{
						if (foundWord)
						{
							player.send(foundWordInfo);
						}
						else
						{
							player.send(chatMessage);
						}
					}

					// check if this is the last player that found the word
					if (foundWord && roomInfo.RoundInfo.PlayerTimes.Count == players.Count() - 1)
					{
						// end round
						roomInfo.RoundInfo.WordUpdateTimer.Stop();

						// send revealed word
						string revealedWord = string.Join(" ", roomInfo.CurrentWord);
						var choosedWordInfo = new ChoosedWordInfo { Word = revealedWord };

						this.sendObjectToPlayers(players, choosedWordInfo);

						this.nextDrawer(roomInfo);
					}
				}
			}
			else if (obj is Drawing)
			{
				// get every player in the same room, except drawer
				var players = this.getPlayersInGameExceptDrawer(roomInfo);

				this.sendObjectToPlayers(players, obj);
			}
			else if (obj is ChoosedWord)
			{
				var choosenWord = obj as ChoosedWord;

				roomInfo.CurrentWord = choosenWord.Word;
				roomInfo.CurrentWordRevealed.Clear();

				// store choosen word to list with choosen words => solution is a bit ugly
				roomInfo.ChoosenWordIndices.Add(this.wordList.IndexOf(this.wordList.First(w => w.Word == choosenWord.Word)));

				var timer = roomInfo.RoundInfo.WordUpdateTimer = new Timer();
				timer.Interval = (int)((this.roundDuration * 1000.0) / choosenWord.Word.Length);
				timer.Tick += (s2, e2) => this.revealCharOfWord(s2, roomInfo);
				this.revealCharOfWord(timer, roomInfo);
				timer.Start();
			}
		}

		// Control Events
		private void btnStop_Click(object sender, EventArgs e)
		{
			this.Parent.closeCurrentPage();
		}

		// Player Requests
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInGame(RoomInfo roomInfo)
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.RoomName == roomInfo.Name && u.State == State.Game;
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInLobby(RoomInfo roomInfo)
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.RoomName == roomInfo.Name && (u.State == State.Lobby || u.State == State.LobbyReady);
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getRemainingDrawerCandidates(RoomInfo roomInfo)
		{
			return this.getPlayersInGame(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return !roomInfo.RoundInfo.PlayersThatDrawed.Contains(u.PlayerName);
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInRoomChoice()
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.State == State.RoomChoice;
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInGameExceptDrawer(RoomInfo roomInfo)
		{
			return this.getPlayersInGame(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return !u.IsDrawing;
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInLobbyAndGame(RoomInfo roomInfo)
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.RoomName == roomInfo.Name && (u.State == State.Lobby || u.State == State.LobbyReady || u.State == State.Game);
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getHosts()
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.Host;
			});
		}

		// Private Methods
		private void sendObjectToPlayers(IEnumerable<AdvancedNetworkLib.Client> players, object obj)
		{
			foreach (var player in players)
			{
				player.send(obj);
			}
		}
		private void startRound(RoomInfo roomInfo)
		{
			// get all players
			var playersInGame = this.getPlayersInGame(roomInfo);

			// get random player, that begins the first round
			var drawerCandidates = this.getRemainingDrawerCandidates(roomInfo);

			///////////////////////////////////////////////////////////////////////////////////////////////////////
			// TODO: if only one player left in room => kick him
			var nextDrawer = drawerCandidates.First(); // TODO: make this random

			roomInfo.Drawer = (nextDrawer.UserData as ClientUserData).PlayerName;
			(nextDrawer.UserData as ClientUserData).IsDrawing = true;
			this.sendRankListToAll(roomInfo);
			roomInfo.RoundInfo.PlayersThatDrawed.Add(roomInfo.Drawer);

			// set round info
			roomInfo.RoundInfo.Number++;
			roomInfo.RoundInfo.StartTime = DateTime.Now.Ticks;

			var choosingWordInfo = new ChoosingWordInfo { PlayerName = (nextDrawer.UserData as ClientUserData).PlayerName };

			var simpleRoundInfo = new SimpleRoundInfo { Current = roomInfo.RoundInfo.Number, Total = roomInfo.TotalRoundCount };
			foreach (var p in playersInGame)
			{
				if (p != nextDrawer)
				{
					p.send(choosingWordInfo);
				}
				p.send(simpleRoundInfo);
			}

			// send word choice
			this.sendWordChoice(nextDrawer, roomInfo);
			///////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		private void nextDrawer(RoomInfo roomInfo)
		{
			// reset current drawer to normal player
			try
			{
				var drawingPlayer = Server.Clients.First(c =>
				{
					var u = c.UserData as ClientUserData;
					return u != null && u.RoomName == roomInfo.Name && u.PlayerName == roomInfo.Drawer;
				});
				(drawingPlayer.UserData as ClientUserData).IsDrawing = false;
			}
			catch { }

			// evaluate current drawing-round (update player points)
			foreach (var item in roomInfo.RoundInfo.PlayerTimes)
			{
				var player = Server.Clients.First(c =>
				{
					var u = c.UserData as ClientUserData;
					return u != null && u.PlayerName == item.Key;
				});

				if (player != null)
				{
					(player.UserData as ClientUserData).Points += item.Value;
				}
			}

			// reset drawing-round-info
			roomInfo.RoundInfo.StartTime = 0;
			roomInfo.RoundInfo.PlayerTimes.Clear();

			// TODO: create and send evalution (show evalution-overview on client)

			// send updated ranklist
			this.sendRankListToAll(roomInfo);

			var drawerCandidates = this.getRemainingDrawerCandidates(roomInfo);

			// check if there are remaing drawers in this round
			if (drawerCandidates.Count() > 0)
			{
				// get all players
				var playersInGame = this.getPlayersInGame(roomInfo);

				///////////////////////////////////////////////////////////////////////////////////////////////////////
				var nextDrawer = drawerCandidates.First(); // TODO: make this random

				roomInfo.Drawer = (nextDrawer.UserData as ClientUserData).PlayerName;
				(nextDrawer.UserData as ClientUserData).IsDrawing = true;
				this.sendRankListToAll(roomInfo);
				roomInfo.RoundInfo.PlayersThatDrawed.Add(roomInfo.Drawer);

				var choosingWordInfo = new ChoosingWordInfo { PlayerName = (nextDrawer.UserData as ClientUserData).PlayerName };

				var simpleRoundInfo = new SimpleRoundInfo { Current = roomInfo.RoundInfo.Number, Total = roomInfo.TotalRoundCount };
				foreach (var p in playersInGame)
				{
					if (p != nextDrawer)
					{
						p.send(choosingWordInfo);
					}
					p.send(simpleRoundInfo);
				}

				// set round info
				//roomInfo.RoundInfo.Number++;
				roomInfo.RoundInfo.StartTime = DateTime.Now.Ticks;

				// send word choice
				this.sendWordChoice(nextDrawer, roomInfo);
				///////////////////////////////////////////////////////////////////////////////////////////////////////
			}
			else
			{
				// check if rounds remaining
				if (roomInfo.RoundInfo.Number < roomInfo.TotalRoundCount)
				{
					roomInfo.RoundInfo.PlayersThatDrawed.Clear();

					// start next round
					this.startRound(roomInfo);
				}
				else
				{
					// TODO: show final evalution
					var allPlayers = this.getPlayersInGame(roomInfo);
					foreach (var p in allPlayers)
					{
						p.send(new FinalEvaluation { });
					}
				}
			}
		}
		//private void evaluateRound(RoomInfo roomInfo)
		//{
		//	var drawingPlayer = Server.Clients.First(c =>
		//	{
		//		var u = c.UserData as ClientUserData;
		//		return u != null && u.RoomName == roomInfo.Name && u.PlayerName == roomInfo.Drawer;
		//	});
		//	(drawingPlayer.UserData as ClientUserData).IsDrawing = false;

		//	// evaluate current round (update player points)
		//	foreach (var item in roomInfo.RoundInfo.PlayerTimes)
		//	{
		//		var player = Server.Clients.First(c =>
		//		{
		//			var u = c.UserData as ClientUserData;
		//			return u != null && u.PlayerName == item.Key;
		//		});

		//		if (player != null)
		//		{
		//			(player.UserData as ClientUserData).Points += item.Value;
		//		}
		//	}

		//	// reset round-info
		//	roomInfo.RoundInfo.StartTime = 0;
		//	roomInfo.RoundInfo.PlayerTimes.Clear();

		//	// send updated ranklist
		//	this.sendRankListToAll(roomInfo.Name);

		//	// TODO: create and send evalution

			

		//	//if (roomInfo.RoundInfo.Number < roomInfo.TotalRoundCount)
		//	//{
		//	//	// start next round
		//	//	//roomInfo.RoundInfo.Number++;
		//	//}
		//	//else
		//	//{
		//	//	// TODO: show final evalution
		//	//}
		//}
		private void revealCharOfWord(object sender, RoomInfo roomInfo)
		{
			bool fullyRevealed = false;
			if (roomInfo.CurrentWordRevealed.Count == 0)
			{
				// make placeholder for every char in word
				foreach (char c in roomInfo.CurrentWord)
				{
					roomInfo.CurrentWordRevealed.Add('_');
				}
			}
			else
			{
				// get unrevelead chars
				List<int> unrevealedChars = new List<int>();
				for (int i = 0; i < roomInfo.CurrentWordRevealed.Count; i++)
				{
					if (roomInfo.CurrentWordRevealed[i] == '_')
					{
						unrevealedChars.Add(i);
					}
				}

				// get random index
				int randomIdx = unrevealedChars[Server.rand.Next(unrevealedChars.Count)];

				// reveal char
				roomInfo.CurrentWordRevealed[randomIdx] = roomInfo.CurrentWord[randomIdx];

				fullyRevealed = unrevealedChars.Count == 1;
			}

			// send partly revealed word to all players, except drawer
			var players = this.getPlayersInGameExceptDrawer(roomInfo);


			// check if word is fully revealed
			string revealedWord;
			if (fullyRevealed)
			{
				(sender as Timer).Stop();

				revealedWord = string.Join(" ", roomInfo.CurrentWord);
			}
			else
			{
				revealedWord = string.Join(" ", roomInfo.CurrentWordRevealed);
			}

			var choosedWordInfo = new ChoosedWordInfo { Word = revealedWord };
			foreach (var player in players)
				player.send(choosedWordInfo);

			if (fullyRevealed)
			{
				this.nextDrawer(roomInfo);
			}
		}

		private RankList getRankList(RoomInfo roomInfo)
		{
			// TODO: prevent calling this function unnecessary often
			var players = this.getPlayersInGame(roomInfo);
			players = players.OrderByDescending(c => (c.UserData as ClientUserData).Points);

			RankList rankList = new RankList();
			foreach (var player in players)
			{
				var u = player.UserData as ClientUserData;
				rankList.Items.Add(new RankListItem
				{
					Host = u.Host,
					PlayerName = u.PlayerName,
					IsDrawing = u.IsDrawing,
					Points = u.Points,
				});
			}

			return rankList;
		}
		private void sendRankListToAll(RoomInfo roomInfo)
		{
			var rankList = this.getRankList(roomInfo);

			var players = this.getPlayersInGame(roomInfo);
			this.sendObjectToPlayers(players, rankList);
		}

		private LobbyList getLobbyList(RoomInfo roomInfo)
		{
			var players = this.getPlayersInLobbyAndGame(roomInfo);

			LobbyList lobbyList = new LobbyList();
			foreach (var player in players)
			{
				var u = player.UserData as ClientUserData;
				lobbyList.Items.Add(new LobbyListItem
				{
					PlayerName = u.PlayerName,
					State = u.State
				});
			}

			return lobbyList;
		}
		private void sendLobbyListToAll(RoomInfo roomInfo)
		{
			var lobbyList = this.getLobbyList(roomInfo);

			var players = this.getPlayersInLobby(roomInfo);
			this.sendObjectToPlayers(players, lobbyList);
		}

		private RoomList getRoomList()
		{
			RoomList roomList = new RoomList();
			foreach (var roomInfo in this.rooms)
			{
				roomList.Items.Add(new RoomListItem
				{
					Name = roomInfo.Name,
					PlayersInLobby = this.getPlayersInLobby(roomInfo).Count(),
					PlayersInGame = this.getPlayersInGame(roomInfo).Count()
				});
			}
			return roomList;
		}
		private void sendRoomListToAll()
		{
			// TODO: make this more efficent
			var roomList = this.getRoomList();

			var players = this.getPlayersInRoomChoice();
			this.sendObjectToPlayers(players, roomList);

			this.updateOverview();
		}

		//private void updateRoomList()
		//{
		//	// get all clients that are hosts
		//	var hosts = this.getHosts();

		//	this.roomList.Items.Clear();
		//	foreach (var host in hosts)
		//	{
		//		var u = host.UserData as ClientUserData;
		//		this.roomList.Items.Add(new RoomListItem
		//		{
		//			Name = u.RoomName,
		//			PlayerCount = Server.Clients.Count(c2 => c2.UserData != null && (c2.UserData as ClientUserData).RoomName == u.RoomName),
		//		});
		//	}
		//}
		private void sendRoomList(AdvancedNetworkLib.Client client)
		{
			// TODO: make this more efficent
			var roomList = this.getRoomList();

			client.send(roomList);
		}
		private void updateOverview()
		{
			// TODO: make this more efficent
			var roomList = this.getRoomList();

			this.lvwRooms.Items.Clear();
			foreach (var room in roomList.Items)
			{
				this.lvwRooms.Items.Add(new ListViewItem(new string[] { room.Name, room.PlayersInLobby.ToString(), room.PlayersInGame.ToString(), room.TotalPlayers.ToString() }));
			}
		}
		private List<WordListItem> loadWordListFromFile(string path)
		{
			var wordList = new List<WordListItem>();

			var lines = File.ReadAllLines(path, Encoding.UTF8);
			foreach (var line in lines)
			{
				var parts = line.Split(';');
				wordList.Add(new WordListItem { Word = parts[0], DownVotes = Convert.ToInt32(parts[1]) });
			}

			if (wordList.Count < 10)
				throw new Exception("Not enough words in wordlist! The wordlist has to contain at least 10 words!");

			return wordList;
		}
		private void sendWordChoice(AdvancedNetworkLib.Client player, RoomInfo roomInfo)
		{
			var availableWords = this.wordList.Where(w => !roomInfo.ChoosenWordIndices.Contains(this.wordList.IndexOf(w))).ToList();

			var wordChoice = new WordChoice();
			for (int i = 0; i < 3; i++)
			{
				var randomWord = availableWords.ElementAt(Server.rand.Next(availableWords.Count));
				wordChoice.Words.Add(randomWord.Word);
				availableWords.Remove(randomWord);
			}
			player.send(wordChoice);
		}
	}
}
