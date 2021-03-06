﻿using System;
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
		// Controls
		private DarkTheme.Label lblTitle;
		private DarkTheme.Label lblInfo;
		private ListView lvwWords;
		private ColumnHeader colWord;
		private ColumnHeader colDownvotes;
		private ListView lvwRooms;
		private ColumnHeader colRoomName;
		private ColumnHeader colPlayersInLobby;
		private ColumnHeader colPlayersInGame;
		private ColumnHeader colPlayers;
		private DarkTheme.Label lblClientCount;
		private DarkTheme.Label lblPlayerInLobbyCount;
		private DarkTheme.Label lblPlayerInGameCount;
		private DarkTheme.Label lblRoomCount;
		private DarkTheme.Button btnStop;

		////////////////////////////////////////////////////////////////////////////////////
		private int roundDuration = 120; // in seconds

		private List<RoomInfo> rooms;

		private class WordListItem
		{
			public string Word { get; set; }
			public int DownVotes { get; set; }
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
		}

		private void InitializeComponent()
		{
			this.lblTitle = new DarkTheme.Label();
			this.btnStop = new DarkTheme.Button();
			this.lvwRooms = new System.Windows.Forms.ListView();
			this.colRoomName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayersInLobby = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayersInGame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblInfo = new DarkTheme.Label();
			this.lblClientCount = new DarkTheme.Label();
			this.lblPlayerInLobbyCount = new DarkTheme.Label();
			this.lblPlayerInGameCount = new DarkTheme.Label();
			this.lblRoomCount = new DarkTheme.Label();
			this.lvwWords = new System.Windows.Forms.ListView();
			this.colWord = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDownvotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.colPlayers});
			this.lvwRooms.Location = new System.Drawing.Point(274, 134);
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
			// colSpieler
			// 
			this.colPlayers.Text = "Spieler";
			this.colPlayers.Width = 50;
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
			this.lblClientCount.Location = new System.Drawing.Point(696, 134);
			this.lblClientCount.Name = "lblClientCount";
			this.lblClientCount.Size = new System.Drawing.Size(161, 21);
			this.lblClientCount.TabIndex = 5;
			this.lblClientCount.Text = "Verbundene Clients: 0";
			// 
			// lblPlayerInLobbyCount
			// 
			this.lblPlayerInLobbyCount.AutoSize = true;
			this.lblPlayerInLobbyCount.BackColor = System.Drawing.Color.Transparent;
			this.lblPlayerInLobbyCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPlayerInLobbyCount.ForeColor = System.Drawing.Color.White;
			this.lblPlayerInLobbyCount.Location = new System.Drawing.Point(696, 169);
			this.lblPlayerInLobbyCount.Name = "lblPlayerInLobbyCount";
			this.lblPlayerInLobbyCount.Size = new System.Drawing.Size(149, 21);
			this.lblPlayerInLobbyCount.TabIndex = 6;
			this.lblPlayerInLobbyCount.Text = "Spieler in Lobbies: 0";
			// 
			// lblPlayerInGameCount
			// 
			this.lblPlayerInGameCount.AutoSize = true;
			this.lblPlayerInGameCount.BackColor = System.Drawing.Color.Transparent;
			this.lblPlayerInGameCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblPlayerInGameCount.ForeColor = System.Drawing.Color.White;
			this.lblPlayerInGameCount.Location = new System.Drawing.Point(696, 204);
			this.lblPlayerInGameCount.Name = "lblPlayerInGameCount";
			this.lblPlayerInGameCount.Size = new System.Drawing.Size(146, 21);
			this.lblPlayerInGameCount.TabIndex = 7;
			this.lblPlayerInGameCount.Text = "Spieler in Spielen: 0";
			// 
			// lblRoomCount
			// 
			this.lblRoomCount.AutoSize = true;
			this.lblRoomCount.BackColor = System.Drawing.Color.Transparent;
			this.lblRoomCount.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblRoomCount.ForeColor = System.Drawing.Color.White;
			this.lblRoomCount.Location = new System.Drawing.Point(696, 239);
			this.lblRoomCount.Name = "lblRoomCount";
			this.lblRoomCount.Size = new System.Drawing.Size(75, 21);
			this.lblRoomCount.TabIndex = 8;
			this.lblRoomCount.Text = "Räume: 0";
			// 
			// lvwWords
			// 
			this.lvwWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvwWords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colWord,
            this.colDownvotes});
			this.lvwWords.Location = new System.Drawing.Point(68, 134);
			this.lvwWords.Name = "lvwWords";
			this.lvwWords.Size = new System.Drawing.Size(200, 320);
			this.lvwWords.TabIndex = 9;
			this.lvwWords.UseCompatibleStateImageBehavior = false;
			this.lvwWords.View = System.Windows.Forms.View.Details;
			// 
			// colWord
			// 
			this.colWord.Text = "Wort";
			this.colWord.Width = 130;
			// 
			// colDownvotes
			// 
			this.colDownvotes.Text = "Downvotes";
			this.colDownvotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colDownvotes.Width = 49;
			// 
			// PageHostOverview
			// 
			this.Controls.Add(this.lvwWords);
			this.Controls.Add(this.lblRoomCount);
			this.Controls.Add(this.lblPlayerInGameCount);
			this.Controls.Add(this.lblPlayerInLobbyCount);
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
			// TODO: make words downvoteable
			this.wordList = this.loadWordListFromFile(@"Resources\wordlists\de.txt");
			this.updateWordListControl(this.wordList);

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
			// TODO: kick last player in game

			lock (this.rooms)
			{
				// check if rooms have enough player
				foreach (var roomInfo in this.rooms)
				{
					// check if only one player is left in game
					var playersInGame = this.getPlayersInGame(roomInfo);
					if (playersInGame.Count() == 1)
					{
						// kick last player in game
						playersInGame.First().send(new KickedNoMorePlayer());
					}
				}

				// update roominfos
				this.sendRoomListToAll();

				// check if rooms still have a host
				foreach (var roomInfo in this.rooms)
				{
					var playersInGame = this.getPlayersInGame(roomInfo);
					var hostFound = playersInGame.Any(p => (p.UserData as ClientUserData).Host);
					if (!hostFound && playersInGame.Count() > 0)
					{
						// nominate new host
						(playersInGame.First().UserData as ClientUserData).Host = true;
						roomInfo.Drawer = (playersInGame.First().UserData as ClientUserData).PlayerName;
						this.sendRankListToAll(roomInfo);
					}
				}

				// check if rooms still have a drawer
				foreach (var roomInfo in this.rooms)
				{
					var playersInGame = this.getPlayersInGame(roomInfo);
					var drawerFound = playersInGame.Any(p => (p.UserData as ClientUserData).IsDrawing);
					if (!drawerFound && playersInGame.Count() > 0)
					{
						// choose new drawer
						this.nextDrawer(roomInfo);
					}
				}

				// update and send lobbylists
				foreach (var roomInfo in this.rooms)
					this.sendLobbyListToAll(roomInfo);

				// update all ranklists
				foreach (var roomInfo in this.rooms)
					this.sendRankListToAll(roomInfo);

				// Update count labels
				this.updateCountLabels();
			}
		}
		private void Server_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			AdvancedNetworkLib.Client client = (sender as AdvancedNetworkLib.Client);
			ClientUserData userData = client.UserData as ClientUserData;
			RoomInfo roomInfo = this.rooms.Find(c => c.Name == userData?.RoomName);

			var obj = e.Object;
			if (obj is ServerPassword)
			{
				ServerPassword serverPassword = obj as ServerPassword;
				this.checkServerPassword(client, serverPassword);
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
					userData.Host = false;

					// if player was in lobby
					if (oldState == State.Lobby || oldState == State.LobbyReady)
					{
						this.sendLobbyListToAll(roomInfo);
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
					}

					// update and send roomlist
					this.sendRoomListToAll();
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
					if (userData.PlayerName == string.Empty)
					{
						string randomPlayerName = string.Empty;

						// generate random room name

						var players = this.getPlayers(roomInfo);
						while (players.Any(p =>
						{
							return (p.UserData as ClientUserData).PlayerName == (randomPlayerName = $"Player{Server.rand.Next(0, 999).ToString().PadLeft(3, '0')}");
						}))
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
						var players = this.getPlayersInLobbyAndReady(roomInfo);
						this.sendObjectToPlayers(players, new StartGame());
					}
					else
					{
						// check if player joins later
						if (this.rooms.Exists(c => c.Name == userData.RoomName && c.Started))
						{
							// this player joins later
							// send ranklist
							this.sendRankListToAll(roomInfo);

							// get current drawer and send drawing request
							var currentDrawer = this.getCurrentDrawer(roomInfo);
							if (currentDrawer != null)
							{
								currentDrawer.send(new DrawingRequest());
							}
						}
						else
						{
							// count players in lobby
							var playersInLobbyCount = this.getPlayersInLobbyAndReady(roomInfo).Count();

							// start game when all players are in the game
							if (playersInLobbyCount == 0)
							{
								// start game for the first time
								roomInfo.Started = true;

								// start first round
								this.startRound(roomInfo);
							}
						}
					}

					this.sendRoomListToAll();
				}

				this.updateCountLabels();
			}
			else if (obj is CreateRoom)
			{
				CreateRoom createRoom = obj as CreateRoom;
				this.createRoom(client, createRoom);
			}
			else if (obj is JoinRoom)
			{
				JoinRoom joinRoom = obj as JoinRoom;
				this.joinRoom(client, joinRoom);
			}
			else if (obj is StartGame)
			{
				int playerCount = this.getPlayers(roomInfo).Count();
				int playerCountReady = this.getPlayersInLobbyAndReady(roomInfo).Count();
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
				bool foundWord = (roomInfo.CurrentWord.ToLower() == chatMessage.Text.ToLower());

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
			else if (obj is KickedByHost)
			{
				var playerName = (obj as KickedByHost).PlayerName;
				var player = this.getPlayerByName(playerName);
				player?.send(obj);
			}
		}

		// Control Events
		private void btnStop_Click(object sender, EventArgs e)
		{
			this.Parent.closeCurrentPage();
		}

		// Player Requests
		private IEnumerable<AdvancedNetworkLib.Client> getPlayers(RoomInfo roomInfo)
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.RoomName == roomInfo.Name;
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInGame(RoomInfo roomInfo)
		{
			return this.getPlayers(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u.State == State.Game;
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInLobby(RoomInfo roomInfo)
		{
			return this.getPlayers(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return (u.State == State.Lobby || u.State == State.LobbyReady);
			});
		}
		private IEnumerable<AdvancedNetworkLib.Client> getPlayersInLobbyAndReady(RoomInfo roomInfo)
		{
			return this.getPlayers(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u.State == State.LobbyReady;
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
			return this.getPlayersInLobby(roomInfo).Concat(this.getPlayersInGame(roomInfo));
		}
		private IEnumerable<AdvancedNetworkLib.Client> getHosts()
		{
			return Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.Host;
			});
		}
		private AdvancedNetworkLib.Client getCurrentDrawer(RoomInfo roomInfo)
		{
			var drawer = this.getPlayersInGame(roomInfo).Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u.IsDrawing;
			});
			if (drawer.Count() > 0)
				return drawer.First();
			return null;
		}
		private AdvancedNetworkLib.Client getPlayerByName(string playerName)
		{
			var player = Server.Clients.Where(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.PlayerName == playerName;
			});

			return (player.Count() > 0) ? player.First() : null;
		}

		// Private Methods
		private void checkServerPassword(AdvancedNetworkLib.Client client, ServerPassword serverPassword)
		{
			if (serverPassword.Hash == Server.PasswordHash)
			{
				client.send(new Success { Job = Job.AcceptPassword });

				client.UserData = new ClientUserData();
			}
			else
			{
				client.send(new Error { Job = Job.AcceptPassword });
			}
		}
		private void createRoom(AdvancedNetworkLib.Client client, CreateRoom createRoom)
		{
			ClientUserData userData = client.UserData as ClientUserData;

			// check if there is any room with the same name
			if (Server.Clients.Any(c => (c.UserData as ClientUserData)?.RoomName == createRoom.Name))
			{
				client.send(new Error { Job = Job.RoomCreation });
			}
			else
			{
				userData.RoomName = createRoom.Name;
				userData.Password = createRoom.Password;
				userData.Host = true;

				// create room list entry
				lock (this.rooms)
				{
					// TODO: make round count variable
					this.rooms.Add(new RoomInfo
					{
						Name = userData.RoomName,
						Started = false,
						TotalRoundCount = 5,
					});
				}

				client.send(new Success { Job = Job.RoomCreation });
			}
		}
		private void joinRoom(AdvancedNetworkLib.Client client, JoinRoom joinRoom)
		{
			ClientUserData userData = client.UserData as ClientUserData;

			// check password
			bool passwordIsValid = Server.Clients.Any(c =>
			{
				var u = c.UserData as ClientUserData;
				return u != null && u.RoomName == joinRoom.Name && u.Password == joinRoom.Password;
			});

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
		private void setChoosenWord()
		{

		}

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
			var nextDrawer = drawerCandidates.Pick();

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
			//try
			{
				//var drawingPlayer = Server.Clients.First(c =>
				//{
				//	var u = c.UserData as ClientUserData;
				//	return u != null && u.RoomName == roomInfo.Name && u.PlayerName == roomInfo.Drawer;
				//});
				//(drawingPlayer.UserData as ClientUserData).IsDrawing = false;
				var drawer = this.getCurrentDrawer(roomInfo);
				if (drawer != null)
				{
					(drawer.UserData as ClientUserData).IsDrawing = false;
				}
			}
			//catch { }

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
				var nextDrawer = drawerCandidates.Pick();

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
					// TODO: create and send final evalution
					var allPlayers = this.getPlayersInGame(roomInfo);
					foreach (var p in allPlayers)
					{
						p.send(new FinalEvaluation { });
					}
				}
			}
		}
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
			lobbyList.RoomName = roomInfo.Name;
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
			// remove empty rooms
			this.rooms.RemoveAll(r =>
			{
				// check if there are players in the room
				var totalPlayerCount = this.getPlayers(r).Count();
				if (totalPlayerCount == 0)
				{
					r.RoundInfo.WordUpdateTimer?.Stop();
				}
				return totalPlayerCount == 0;
			});

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

		private List<WordListItem> loadWordListFromFile(string path, bool tryAgain = true)
		{
			var wordList = new List<WordListItem>();

			string[] lines = null;
			try
			{
				lines = File.ReadAllLines(path, Encoding.UTF8);
			}
			catch (IOException exc)
			{
				if (tryAgain)
				{
					return this.loadWordListFromFile(Path.Combine(@"..\..\", path), false);
				}
				else
				{
					MessageBox.Show(exc.Message);
					return wordList;
				}
			}
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

		private void updateWordListControl(List<WordListItem> wordList)
		{
			var orderedWordList = wordList.OrderBy(w => w.DownVotes);

			this.lvwWords.Items.Clear();
			foreach (var word in orderedWordList)
			{
				this.lvwWords.Items.Add(new ListViewItem(new string[] { word.Word, word.DownVotes.ToString() }));
			}
		}
		private void updateCountLabels()
		{
			this.lblClientCount.Text = $"Verbundene Clients: {Server.Clients.Count()}";
			this.lblPlayerInLobbyCount.Text = $"Spieler in Lobbies: {Server.Clients.Count(c => (c.UserData as ClientUserData)?.State == State.Lobby || (c.UserData as ClientUserData)?.State == State.LobbyReady)}";
			this.lblPlayerInGameCount.Text = $"Spieler in Spielen: {Server.Clients.Count(c => (c.UserData as ClientUserData)?.State == State.Game)}";
			this.lblRoomCount.Text = $"Räume: {this.rooms.Count}";
		}
	}
}
