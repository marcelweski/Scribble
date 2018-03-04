using System;
using System.Collections.Generic;

namespace Scribble
{
	[Serializable]
	public class ServerPassword
	{
		public int Hash;
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
		public Job Job;
	}

	[Serializable]
	public class Error
	{
		public Job Job;
	}

	[Serializable]
	public struct RoomListItem
	{
		public string Name;
		public int PlayerCount;
	}

	[Serializable]
	public class RoomList
	{
		public List<RoomListItem> Items;

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
		public State State;
		public object Data;
	}

	[Serializable]
	public class ClientUserData
	{
		public bool Host;
		public string RoomName;
		public string PlayerName;
		public int Password;
		public State State;
		public bool IsDrawing;
		public long Points;
	}

	[Serializable]
	public class RandomRoomName
	{
		public string Name;
	}

	[Serializable]
	public class RandomPlayerName
	{
		public string Name;
	}

	[Serializable]
	public class CreateRoom
	{
		public string Name;
		public int Password;
	}

	[Serializable]
	public class JoinRoom
	{
		public string Name;
		public int Password;
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
		public List<LobbyListItem> Items { get; set; }

		public LobbyList()
		{
			this.Items = new List<LobbyListItem>();
		}
	}

	[Serializable]
	public class StartGame
	{

	}

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
		public string PlayerName;
		public string Text;
	}

	[Serializable]
	public class ChoosingWordInfo
	{
		public string PlayerName;
	}

	[Serializable]
	public class FoundWordInfo
	{
		public string PlayerName;
	}

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
	public class RoomInfoSimple
	{
		//public int RoundCount;
		//public int CurrentRound;

		public string Name;
		public string Drawer;
		public string CurrentWord;

	}

	[Serializable]
	public class RoundInfo
	{
		public int Number { get; set; }
		public long StartTime;
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
	public class RoomInfo : RoomInfoSimple
	{
		public int TotalRoundCount;
		//public int CurrentRound;

		public RoundInfo RoundInfo { get; set; }

		public bool Started;
		public List<int> ChoosenWordIndices;
		public List<char> CurrentWordRevealed;

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
		//public byte[] Data;
		public System.Drawing.Bitmap Bitmap;
	}

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
	{

	}
}
