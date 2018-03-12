using System;
using System.Collections.Generic;

namespace Scribble
{
	[Serializable]
	public class ServerPassword
	{
		public int Hash { get; set; }
	}

	public enum Job
	{
		AcceptPassword,
		RoomCreation,
		RoomJoin,
		NameChange,
		GameStart
	}

	[Serializable]
	public class Success
	{
		public Job Job { get; set; }
	}

	[Serializable]
	public class Error
	{
		public Job Job { get; set; }
	}

	[Serializable]
	public struct RoomListItem
	{
		public string Name { get; set; }
		public int PlayersInLobby { get; set; }
		public int PlayersInGame { get; set; }
		public int TotalPlayers { get => this.PlayersInLobby + this.PlayersInGame; }
	}

	[Serializable]
	public class RoomList
	{
		public List<RoomListItem> Items { get; set; }

		public RoomList()
		{
			this.Items = new List<RoomListItem>();
		}
	}

	public enum State
	{
		None,
		RoomChoice,
		RoomCreation,
		Lobby,
		LobbyReady,
		Game
	};

	[Serializable]
	public class ChangeState
	{
		public State State { get; set; }
		public object Data { get; set; }
	}

	[Serializable]
	public class ClientUserData
	{
		public bool Host { get; set; }
		public string RoomName { get; set; }
		public string PlayerName { get; set; }
		public int Password { get; set; }
		public State State { get; set; }
		public bool IsDrawing { get; set; }
		public long Points { get; set; }
	}

	[Serializable]
	public class RandomRoomName
	{
		public string Name { get; set; }
	}

	[Serializable]
	public class RandomPlayerName
	{
		public string Name { get; set; }
	}

	[Serializable]
	public class CreateRoom
	{
		public string Name { get; set; }
		public int Password { get; set; }
	}

	[Serializable]
	public class JoinRoom
	{
		public string Name { get; set; }
		public int Password { get; set; }
	}

	[Serializable]
	public struct LobbyListItem
	{
		public string PlayerName { get; set; }
		public State State { get; set; }
	}

	[Serializable]
	public class LobbyList
	{
		public string RoomName { get; set; }
		public List<LobbyListItem> Items { get; set; }

		public LobbyList()
		{
			this.Items = new List<LobbyListItem>();
		}
	}

	[Serializable]
	public class StartGame
	{ }

	[Serializable]
	public class RankListItem
	{
		public bool Host { get; set; }
		public string PlayerName { get; set; }
		public bool IsDrawing { get; set; }
		public long Points { get; set; }

		public RankListItem()
		{
			this.Host = false;
			this.PlayerName = "";
			this.IsDrawing = false;
			this.Points = 0;
		}
	}

	[Serializable]
	public class RankList
	{
		public List<RankListItem> Items { get; set; }

		public RankList()
		{
			this.Items = new List<RankListItem>();
		}
	}

	[Serializable]
	public class ChatMessage
	{
		public string PlayerName { get; set; }
		public string Text { get; set; }
	}

	[Serializable]
	public class ChoosingWordInfo
	{
		public string PlayerName { get; set; }
	}

	[Serializable]
	public class FoundWordInfo
	{
		public string PlayerName { get; set; }
	}

	[Serializable]
	public class WhatDoYouWantInfo
	{ }

	[Serializable]
	public class WordChoice
	{
		public List<string> Words { get; set; }

		public WordChoice()
		{
			this.Words = new List<string>();
		}
	}

	[Serializable]
	public class RoundInfo
	{
		public int Number { get; set; }
		public long StartTime { get; set; }
		public System.Windows.Forms.Timer WordUpdateTimer { get; set; }

		public Dictionary<string, long> PlayerTimes { get; set; }
		public List<string> PlayersThatDrawed { get; set; }

		public RoundInfo()
		{
			this.Number = 0;
			this.StartTime = 0;
			this.PlayerTimes = new Dictionary<string, long>();
			this.PlayersThatDrawed = new List<string>();
		}
	}

	[Serializable]
	public class RoomInfo
	{
		public string Name { get; set; }
		public string Drawer { get; set; }
		public string CurrentWord { get; set; }

		public int TotalRoundCount { get; set; }

		public RoundInfo RoundInfo { get; set; }

		public bool Started { get; set; }
		public List<int> ChoosenWordIndices { get; set; }
		public List<char> CurrentWordRevealed { get; set; }

		public RoomInfo()
		{
			this.ChoosenWordIndices = new List<int>();
			this.CurrentWordRevealed = new List<char>();
			this.RoundInfo = new RoundInfo();
		}
	}
	
	[Serializable]
	public class SimpleRoundInfo
	{
		public int Current { get; set; }
		public int Total { get; set; }
	}

	[Serializable]
	public class Drawing
	{
		public byte[] Data { get; set; }
	}

	[Serializable]
	public class DrawingRequest
	{ }

	[Serializable]
	public class ChoosedWord
	{
		public string Word { get; set; }
	}

	[Serializable]
	public class ChoosedWordInfo
	{
		public string Word { get; set; }
	}

	[Serializable]
	public class FinalEvaluation
	{ }

	[Serializable]
	public class KickedNoMorePlayer
	{ }

	[Serializable]
	public class KickedByHost
	{
		public string PlayerName { get; set; }
	}
}
