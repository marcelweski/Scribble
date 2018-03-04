using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public static class Server
	{
		private static AdvancedNetworkLib.Server server;
		private static Control _control;

		public static Random rand = new Random((int)DateTime.Now.Ticks);

		private static Dictionary<string, int> hashes;

		public static IEnumerable<AdvancedNetworkLib.Client> Clients { get => server.Clients; }

		public static int PasswordHash;

		//private static void onObjectReceived(AdvancedNetworkLib.Client client, object obj)
		//{
		//	MessageBox.Show($"Objekt vom Typ '{obj.GetType().Name}' von {client.RemoteEndPoint} erhalten", "Server");
		//}
		//private static void onStateChanged(object sender)
		//{
		//	MessageBox.Show(Listening ? "Server wurde gestartet" : "Server wurde gestoppt", "Server");
		//}
		//private static void onErrorOccurred(object sender, string message)
		//{
		//	MessageBox.Show(message, sender.GetType().Name);
		//}
		//private static void onClientsChanged(object sender)
		//{
		//	//MessageBox.Show("Die Verbindungen haben sich geändert", "Server");

		//	foreach (var c in server.Clients)
		//	{
		//		if (c.UserData == null)
		//		{
		//			c.UserData = new ClientUserDataOld
		//			{
		//				Host = false,
		//				PlayerName = null,
		//				RoomName = null,
		//				State = StateOld.Connecting
		//			};
		//		}
		//	}

		//}

		//private static void Server_ObjectReceived(AdvancedNetworkLib.Client client, object obj)
		//{
		//	var u = client.UserData as ClientUserDataOld;

		//	if (obj is CreateRoomRequest)
		//	{
		//		var request = obj as CreateRoomRequest;

		//		if (hashes.ContainsKey(request.Name))
		//		{
		//			client.send(new ErrorOld { Job = EJob.CreateRoom });
		//		}
		//		else
		//		{
		//			u.Host = true;
		//			u.RoomName = request.Name;
		//			u.PlayerName = "Host";

		//			hashes.Add(request.Name, request.Password);

		//			client.send(new SuccessOld { Job = EJob.CreateRoom });

		//			//var clients = server.Clients.Where(c => (c.UserData as ClientUserData).RoomName == userData.RoomName);
		//			//LobbyList lobbyList = new LobbyList();
		//			//foreach (var c in clients)
		//			//{
		//			//	lobbyList.Names.Add((c.UserData as ClientUserData).PlayerName, (c.UserData as ClientUserData).Ready);
		//			//}

		//			//client.send(lobbyList);
		//		}
		//	}
		//	else if (obj is JoinRoomRequest)
		//	{
		//		var request = obj as JoinRoomRequest;

		//		u.Host = false;
		//		u.RoomName = request.Name;
		//		u.PlayerName = $"Player{rand.Next(0, 999).ToString().PadLeft(3, '0')}";

		//		client.send(new SuccessOld());
		//	}
		//	else if (obj is ChangeStateLobby)
		//	{
		//		u.State = u.Host ? StateOld.LobbyReady : StateOld.Lobby;

		//		// get all clients that are in the same room
		//		var clients = server.Clients.Where(c => (c.UserData as ClientUserDataOld).RoomName == (client.UserData as ClientUserDataOld).RoomName);

		//		// send LobbyList to client
		//		LobbyList lobbyList = new LobbyList();
		//		foreach (var c in clients)
		//		{
		//			var userData = c.UserData as ClientUserDataOld;
		//			if (userData.State != StateOld.Connecting)
		//			{
		//				lobbyList.Items.Add(new LobbyListItem
		//				{
		//					PlayerName = userData.PlayerName,
		//					State = userData.State
		//				});
		//			}
		//		}

		//		foreach (var c in clients)
		//		{
		//			c.send(lobbyList);
		//		}
		//		client.send(new PlayerName { Data = u.PlayerName });
		//	}
		//	else if (obj is PlayerName)
		//	{
		//		string playerName = (obj as PlayerName).Data;

		//		// count all clients that are in the same room and have the same name, except sender client
		//		if (server.Clients.Count(c => (c.UserData as ClientUserDataOld).RoomName == u.RoomName && (c.UserData as ClientUserDataOld).PlayerName == playerName && c != client) > 0)
		//		{
		//			client.send(new ErrorOld { Job = EJob.PlayerNameChange });
		//		}
		//		else
		//		{
		//			client.send(new SuccessOld { Job = EJob.PlayerNameChange });
		//			u.State = StateOld.LobbyReady;
		//			u.PlayerName = playerName;

		//			// get all clients that are in the same room
		//			var clients = server.Clients.Where(c => (c.UserData as ClientUserDataOld).RoomName == (client.UserData as ClientUserDataOld).RoomName);

		//			// send LobbyList to client
		//			LobbyList lobbyList = new LobbyList();
		//			foreach (var c in clients)
		//			{
		//				var userData = c.UserData as ClientUserDataOld;
		//				if (userData.State != StateOld.Connecting)
		//				{
		//					lobbyList.Items.Add(new LobbyListItem
		//					{
		//						PlayerName = userData.PlayerName,
		//						State = userData.State
		//					});
		//				}
		//			}

		//			foreach (var c in clients)
		//			{
		//				c.send(lobbyList);
		//			}
		//		}
		//	}
		//	else if (obj is ChangeStateGame)
		//	{
		//		u.State = StateOld.Game;

		//		// get all clients that are in the same room
		//		var clients = server.Clients.Where(c => (c.UserData as ClientUserDataOld).RoomName == (client.UserData as ClientUserDataOld).RoomName);

		//		// send LobbyList to client
		//		LobbyList lobbyList = new LobbyList();
		//		foreach (var c in clients)
		//		{
		//			var userData = c.UserData as ClientUserDataOld;
		//			if (userData.State != StateOld.Connecting)
		//			{
		//				lobbyList.Items.Add(new LobbyListItem
		//				{
		//					PlayerName = userData.PlayerName,
		//					State = userData.State
		//				});
		//			}
		//		}

		//		foreach (var c in clients)
		//		{
		//			c.send(lobbyList);
		//		}
		//	}
		//	else if (obj is RequestRoomList)
		//	{
		//		RoomListOld roomList = new RoomListOld();
		//		roomList.Names.AddRange(hashes.Keys);

		//		client.send(roomList);
		//	}
		//}

		public static bool Listening { get => server != null ? server.Listening : false; }
		public static ushort Port { get => server != null ? server.Port : (ushort)0; }

		public static event EventHandler<AdvancedNetworkLib.ObjectReceivedEventArgs> ObjectReceived
		{
			add
			{
				server.ObjectReceived += value;
			}
			remove
			{
				server.ObjectReceived -= value;
			}
		}
		public static event EventHandler<AdvancedNetworkLib.StateChangedEventArgs> StateChanged
		{
			add
			{
				server.StateChanged += value;
			}
			remove
			{
				server.StateChanged -= value;
			}
		}
		public static event EventHandler<AdvancedNetworkLib.ClientsChangedEventArgs> ClientsChanged
		{
			add
			{
				server.ClientsChanged += value;
			}
			remove
			{
				server.ClientsChanged -= value;
			}
		}
		public static event EventHandler<AdvancedNetworkLib.ErrorOccurredEventArgs> ErrorOccurred
		{
			add
			{
				server.ErrorOccurred += value;
			}
			remove
			{
				server.ErrorOccurred -= value;
			}
		}

		public static void init(Control control)
		{
			_control = control;
			if (server == null)
			{
				server = new AdvancedNetworkLib.Server(_control);
				//server.ClientsChanged += onClientsChanged;
				//server.ErrorOccurred += onErrorOccurred;
				//server.ObjectReceived += Server_ObjectReceived;

				hashes = new Dictionary<string, int>();
				//server.StateChanged += onStateChanged;
				//ObjectReceived += onObjectReceived;
				//StateChanged += onStateChanged;
			}
		}

		public static void start(ushort port)
		{
			if (server == null)
				throw new ArgumentNullException("server");

			server.start(port);
		}
		public static void stop()
		{
			server?.stop();
			server = null;
		}
	}
}
