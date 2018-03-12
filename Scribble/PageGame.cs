using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO.Compression;
using System.Net.Sockets;
using System.Diagnostics;

namespace Scribble
{
	class PageGame : Page
	{
		// Fields
		// Controls
		private DarkTheme.NumericUpDown nupPenWidth;
		private DrawField drawField;
		private DarkTheme.Button btnReset;
		private DarkTheme.ColorPalette colorPalette;
		private DarkTheme.TextBox txtMessage;
		private DarkTheme.Button btnFillDraw;
		private WinAdvancedNetworkLibSample.ChatControl chatControl;
		private DarkTheme.Label lblPenWidth;
		private DarkTheme.Label lblWord;
		private DarkTheme.Label lblRoundInfo;
		private DarkTheme.Button btnKickPlayer;
		private ListView lvwPlayers;

		private Thread send_bitmap_thread;
		private bool bitmap_updated = false;
		private Bitmap bitmap_buffer;

		private ClientUserData userData;

		public PageGame(ClientUserData userData) : this()
		{
			this.userData = userData;
			this.userData.IsDrawing = false;
			this.send_bitmap_thread = null;

		}
		public PageGame()
		{
			InitializeComponent();

			this.nupPenWidth.ValueChanged += nupPenSize_ValueChanged;
			this.colorPalette.ColorChanged += ColorPalette_ColorChanged;

			this.Load += PageGame_Load;
			this.Closed += PageGame_Closed;

			var imagelist = new ImageList();
			imagelist.ImageSize = new Size(32, 32);
			imagelist.ColorDepth = ColorDepth.Depth32Bit;
			imagelist.Images.Add(Properties.Resources.user_small_white);
			this.lvwPlayers.LargeImageList = imagelist;
			this.lblRoundInfo.Text = "";
		}

		private void InitializeComponent()
		{
			this.nupPenWidth = new DarkTheme.NumericUpDown();
			this.lblPenWidth = new DarkTheme.Label();
			this.drawField = new Scribble.DrawField();
			this.btnReset = new DarkTheme.Button();
			this.colorPalette = new DarkTheme.ColorPalette();
			this.txtMessage = new DarkTheme.TextBox();
			this.btnFillDraw = new DarkTheme.Button();
			this.chatControl = new WinAdvancedNetworkLibSample.ChatControl();
			this.lvwPlayers = new System.Windows.Forms.ListView();
			this.lblWord = new DarkTheme.Label();
			this.lblRoundInfo = new DarkTheme.Label();
			this.btnKickPlayer = new DarkTheme.Button();
			this.SuspendLayout();
			// 
			// nupPenWidth
			// 
			this.nupPenWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nupPenWidth.BackColor = System.Drawing.Color.White;
			this.nupPenWidth.Location = new System.Drawing.Point(728, 509);
			this.nupPenWidth.Name = "nupPenWidth";
			this.nupPenWidth.Size = new System.Drawing.Size(100, 29);
			this.nupPenWidth.TabIndex = 1;
			this.nupPenWidth.Text = "numericUpDown1";
			this.nupPenWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// lblPenWidth
			// 
			this.lblPenWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPenWidth.AutoSize = true;
			this.lblPenWidth.BackColor = System.Drawing.Color.Transparent;
			this.lblPenWidth.Font = new System.Drawing.Font("Consolas", 12F);
			this.lblPenWidth.ForeColor = System.Drawing.Color.White;
			this.lblPenWidth.Location = new System.Drawing.Point(724, 487);
			this.lblPenWidth.Name = "lblPenWidth";
			this.lblPenWidth.Size = new System.Drawing.Size(108, 19);
			this.lblPenWidth.TabIndex = 2;
			this.lblPenWidth.Text = "Stiftstärke";
			// 
			// drawField
			// 
			this.drawField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawField.BackColor = System.Drawing.Color.White;
			this.drawField.BorderColor = System.Drawing.Color.Empty;
			this.drawField.Location = new System.Drawing.Point(234, 62);
			this.drawField.Name = "drawField";
			this.drawField.PenColor = System.Drawing.Color.Black;
			this.drawField.PenWidth = 10F;
			this.drawField.Size = new System.Drawing.Size(598, 400);
			this.drawField.TabIndex = 3;
			this.drawField.Text = "drawField";
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
			this.btnReset.FlatAppearance.BorderSize = 0;
			this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnReset.ForeColor = System.Drawing.Color.Black;
			this.btnReset.Location = new System.Drawing.Point(234, 509);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(128, 29);
			this.btnReset.TabIndex = 4;
			this.btnReset.Text = "Zurücksetzen";
			this.btnReset.UseVisualStyleBackColor = false;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// colorPalette
			// 
			this.colorPalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.colorPalette.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.colorPalette.Location = new System.Drawing.Point(377, 509);
			this.colorPalette.Name = "colorPalette";
			this.colorPalette.Size = new System.Drawing.Size(300, 30);
			this.colorPalette.TabIndex = 5;
			this.colorPalette.Text = "colorPalette1";
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.BackColor = System.Drawing.Color.White;
			this.txtMessage.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
			this.txtMessage.Location = new System.Drawing.Point(866, 509);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = false;
			this.txtMessage.Size = new System.Drawing.Size(200, 29);
			this.txtMessage.TabIndex = 7;
			this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
			// 
			// btnFillDraw
			// 
			this.btnFillDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFillDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
			this.btnFillDraw.FlatAppearance.BorderSize = 0;
			this.btnFillDraw.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFillDraw.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFillDraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFillDraw.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnFillDraw.ForeColor = System.Drawing.Color.Black;
			this.btnFillDraw.Location = new System.Drawing.Point(629, 509);
			this.btnFillDraw.Name = "btnFillDraw";
			this.btnFillDraw.Size = new System.Drawing.Size(89, 29);
			this.btnFillDraw.TabIndex = 8;
			this.btnFillDraw.Text = "Füllen";
			this.btnFillDraw.UseVisualStyleBackColor = false;
			this.btnFillDraw.Click += new System.EventHandler(this.btnFillDraw_Click);
			// 
			// chatControl
			// 
			this.chatControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chatControl.BackColor = System.Drawing.Color.White;
			this.chatControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.chatControl.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
			this.chatControl.Location = new System.Drawing.Point(866, 62);
			this.chatControl.Name = "chatControl";
			this.chatControl.ReadOnly = true;
			this.chatControl.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.chatControl.Size = new System.Drawing.Size(200, 441);
			this.chatControl.TabIndex = 9;
			this.chatControl.Text = "";
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lvwPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvwPlayers.Location = new System.Drawing.Point(21, 62);
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.Size = new System.Drawing.Size(207, 400);
			this.lvwPlayers.TabIndex = 10;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPlayers.View = System.Windows.Forms.View.Tile;
			this.lvwPlayers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwPlayers_ItemSelectionChanged);
			// 
			// lblWord
			// 
			this.lblWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblWord.AutoSize = true;
			this.lblWord.BackColor = System.Drawing.Color.Transparent;
			this.lblWord.Font = new System.Drawing.Font("Consolas", 12F);
			this.lblWord.ForeColor = System.Drawing.Color.White;
			this.lblWord.Location = new System.Drawing.Point(455, 17);
			this.lblWord.Name = "lblWord";
			this.lblWord.Size = new System.Drawing.Size(180, 19);
			this.lblWord.TabIndex = 11;
			this.lblWord.Text = "_ _ _ _ _ _ _ _ _ _";
			this.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblRoundInfo
			// 
			this.lblRoundInfo.AutoSize = true;
			this.lblRoundInfo.BackColor = System.Drawing.Color.Transparent;
			this.lblRoundInfo.Font = new System.Drawing.Font("Segoe UI", 14F);
			this.lblRoundInfo.ForeColor = System.Drawing.Color.White;
			this.lblRoundInfo.Location = new System.Drawing.Point(17, 17);
			this.lblRoundInfo.Name = "lblRoundInfo";
			this.lblRoundInfo.Size = new System.Drawing.Size(142, 25);
			this.lblRoundInfo.TabIndex = 12;
			this.lblRoundInfo.Text = "Runde 1 von 10";
			// 
			// btnKickPlayer
			// 
			this.btnKickPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnKickPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
			this.btnKickPlayer.FlatAppearance.BorderSize = 0;
			this.btnKickPlayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnKickPlayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnKickPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKickPlayer.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnKickPlayer.ForeColor = System.Drawing.Color.Black;
			this.btnKickPlayer.Location = new System.Drawing.Point(22, 510);
			this.btnKickPlayer.Name = "btnKickPlayer";
			this.btnKickPlayer.Size = new System.Drawing.Size(128, 29);
			this.btnKickPlayer.TabIndex = 13;
			this.btnKickPlayer.Text = "Kicken";
			this.btnKickPlayer.UseVisualStyleBackColor = false;
			this.btnKickPlayer.Visible = false;
			this.btnKickPlayer.Click += new System.EventHandler(this.btnKickPlayer_Click);
			// 
			// PageGame
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnKickPlayer);
			this.Controls.Add(this.lblRoundInfo);
			this.Controls.Add(this.lblWord);
			this.Controls.Add(this.lvwPlayers);
			this.Controls.Add(this.chatControl);
			this.Controls.Add(this.btnFillDraw);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.colorPalette);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.drawField);
			this.Controls.Add(this.lblPenWidth);
			this.Controls.Add(this.nupPenWidth);
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageGame";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		// Page Events
		private void PageGame_Load(object sender, EventArgs e)
		{
			Client.ConnectionChanged += Client_ConnectionChanged;
			Client.ErrorOccurred += Client_ErrorOccurred;
			Client.ObjectReceived += Client_ObjectReceived;

			Client.send(new ChangeState { State = State.Game });

			this.drawField.BitmapChanged += DrawField_BitmapChanged;
		}
		private void PageGame_Closed(object sender, EventArgs e)
		{
			Client.ConnectionChanged -= Client_ConnectionChanged;
			Client.ErrorOccurred -= Client_ErrorOccurred;
			Client.ObjectReceived -= Client_ObjectReceived;

			this.userData.IsDrawing = false;
		}

		// Client Events
		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if(e.Lost)
			{
				Client.ConnectionChanged -= Client_ConnectionChanged;
				Client.ErrorOccurred -= Client_ErrorOccurred;
				Client.ObjectReceived -= Client_ObjectReceived;

				this.Parent.closeCurrentPage();
				MessageBox.Show("Der Server ist nicht mehr erreichbar!");
			}
		}
		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
	{
			MessageBox.Show(e.Exception.Message);
		}
		private void Client_ObjectReceived(object sender, AdvancedNetworkLib.ObjectReceivedEventArgs e)
		{
			Console.WriteLine($"Received obj '{e.Object.GetType().Name}'");

			var obj = e.Object;

			if (obj is RankList)
			{
				this.updateRankListControl(obj as RankList);
			}
			else if (obj is ChatMessage)
			{
				var chatMessage = obj as ChatMessage;
				this.chatControl.AddMessage(chatMessage.PlayerName, chatMessage.Text);
			}
			else if (obj is ChoosingWordInfo)
			{
				var choosingWordInfo = obj as ChoosingWordInfo;
				this.chatControl.AddInfo($"{choosingWordInfo.PlayerName} wählt ein Wort!");
			}
			else if (obj is FoundWordInfo)
			{
				var foundWordInfo = obj as FoundWordInfo;
				this.chatControl.AddInfo($"{foundWordInfo.PlayerName} hat das Wort erraten!");
			}
			else if (obj is WhatDoYouWantInfo)
			{
				this.chatControl.AddInfo("Was willst du damit erreichen?");
			}
			else if (obj is WordChoice)
			{
				var wordChoice = obj as WordChoice;
				MessageWordChoice.Open(this.Parent, wordChoice, this.wordChoosen);
			}
			else if (obj is Drawing)
			{
				using (var ms = new MemoryStream((obj as Drawing).Data))
				{
					this.drawField.updateBitmap((Bitmap)Image.FromStream(ms));
				}
			}
			else if (obj is DrawingRequest)
			{
				// send latest drawing
				this.DrawField_BitmapChanged(null, EventArgs.Empty);
			}
			else if (obj is ChoosedWordInfo)
			{
				var choosedWord = obj as ChoosedWordInfo;
				this.lblWord.Text = choosedWord.Word;
			}
			else if (obj is FinalEvaluation)
			{
				this.chatControl.AddInfo("Ende im Gelände");
			}
			else if (obj is SimpleRoundInfo)
			{
				var sri = obj as SimpleRoundInfo;
				this.lblRoundInfo.Text = $"Runde {sri.Current} von {sri.Total}";
			}
			else if (obj is KickedNoMorePlayer)
			{
				this.Parent.closeCurrentPage();
				MessageBox.Show("Alle Spieler haben das Spiel verlassen");
			}
			else if (obj is KickedByHost)
			{
				this.Parent.closeCurrentPage();
				MessageBox.Show("Du wurdest aus dem Spiel geworfen!");
			}
		}

		// Control Events
		private void ColorPalette_ColorChanged(object sender, System.Drawing.Color color)
		{
			this.drawField.PenColor = color;
			this.drawField.BorderColor = color;
			this.drawField.Refresh();
		}
		private void nupPenSize_ValueChanged(object sender, EventArgs e)
		{
			this.drawField.PenWidth = Convert.ToSingle(this.nupPenWidth.Value);
		}
		private void DrawField_BitmapChanged(object sender, EventArgs e)
		{
			// TODO: make sure that latest bitmap will be send
			if (this.userData.IsDrawing && !this.bitmap_updated)
			{
				Console.WriteLine("Start cloning");
				this.bitmap_buffer = this.drawField.getBitmapClone();
				this.bitmap_updated = true;
			}
		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			this.drawField.reset();
		}
		private void btnFillDraw_Click(object sender, EventArgs e)
		{
			this.drawField.toogleMode();
			this.btnFillDraw.Text = (this.drawField.Mode == DrawField.EMode.Draw ? "Füllen" : "Zeichnen");
		}
		private void txtMessage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				string text = this.txtMessage.Text;
				if (text.Length > 0 && text.Length < 1000)
				{
					Client.send(new ChatMessage { Text = this.txtMessage.Text });
				}

				this.txtMessage.textBox.Clear();
			}
		}
		private void lvwPlayers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.btnKickPlayer.Visible = this.userData.Host && e.IsSelected;
		}
		private void btnKickPlayer_Click(object sender, EventArgs e)
		{
			if (this.lvwPlayers.SelectedItems.Count > 0)
			{
				// kick selected player
				Client.send(new KickedByHost { PlayerName = this.lvwPlayers.SelectedItems[0].Tag.ToString() });
			}
		}

		// Private Methods
		private void wordChoosen(object sender, WordChoosenEventArgs e)
		{
			this.drawField.reset();

			Client.send(new ChoosedWord { Word = e.Word });

			string wordWithSpaces = string.Join(" ", e.Word.ToArray());
			this.lblWord.Text = wordWithSpaces;
		}
		private void updateRankListControl(RankList rankList)
		{
			this.lvwPlayers.Items.Clear();
			for (int i = 0; i < rankList.Items.Count; i++)
			{
				var player = rankList.Items[i];
				//var tileSize = this.lvwPlayers.TileSize;

				var item = new ListViewItem($"{i+1}. {player.PlayerName}", 0);
				item.Tag = player.PlayerName;
				item.ForeColor = player.PlayerName == userData.PlayerName ? Color.LightBlue : Color.White;

				item.Text += $" [{player.Points}]";

				if (player.Host)
				{
					//item.Font = new Font("Segoe UI Emoji", 10.0f, FontStyle.Underline);
					item.Text += " [Host]";
				}

				if (player.IsDrawing)
				{
					//item.Font = new Font("Segoe UI Emoji", 10.0f, FontStyle.Underline|FontStyle.Italic);
					item.Text += " [zeichnet]";

					// TODO: find better solution
					this.userData.IsDrawing = this.userData.PlayerName == player.PlayerName;

					if (this.send_bitmap_thread == null)
					{
						this.send_bitmap_thread = new Thread(this.send_bitmap);
						this.send_bitmap_thread.Start();
					}

				}

				if (player.PlayerName == this.userData.PlayerName && player.Host)
				{
					this.userData.Host = true;
				}

				this.lvwPlayers.Items.Add(item);
			}

			var isDrawing = this.userData.IsDrawing;
			this.drawField.Enabled = isDrawing;
			this.btnReset.Visible = isDrawing;
			this.btnFillDraw.Visible = isDrawing;
			this.colorPalette.Visible = isDrawing;
			this.nupPenWidth.Visible = isDrawing;
			this.lblPenWidth.Visible = isDrawing;
		}
		private void send_bitmap()
		{
			while (this.userData.IsDrawing)
			{
				if (this.bitmap_updated)
				{
					try
					{
						using (var ms = new MemoryStream())
						{
							this.bitmap_buffer.Save(ms, ImageFormat.Png);
							this.bitmap_buffer.Dispose();

							Console.WriteLine($"Sending {ms.Length} Bytes");

							Client.sendSync(new Drawing { Data = ms.ToArray() });
						}
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.StackTrace + "\n" + exc.Message);
					}

					this.bitmap_updated = false;
				}
				Thread.Sleep(200);
			}

			this.send_bitmap_thread = null;
		}
	}
}
