using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public static class Client
	{
		private static AdvancedNetworkLib.Client client;

		public static bool Connected { get => client.Connected; }

		public static event EventHandler<AdvancedNetworkLib.ConnectionChangedEventArgs> ConnectionChanged
		{
			add
			{
				client.ConnectionChanged += value;
			}
			remove
			{
				client.ConnectionChanged -= value;
			}
		}
		public static event EventHandler<AdvancedNetworkLib.ErrorOccurredEventArgs> ErrorOccurred
		{
			add
			{
				client.ErrorOccurred += value;
			}
			remove
			{
				client.ErrorOccurred -= value;
			}
		}
		public static event EventHandler<AdvancedNetworkLib.ObjectReceivedEventArgs> ObjectReceived
		{
			add
			{
				client.ObjectReceived += value;
			}
			remove
			{
				client.ObjectReceived -= value;
			}
		}

		public static void init(Control control)
		{
			if (client == null)
			{
				client = new AdvancedNetworkLib.Client(control);
			}
		}

		public static void connect(string host, ushort port)
		{
			if (client == null)
				throw new ArgumentNullException("client");

			client.connect(host, port);
		}

		public static void disconnect()
		{
			client?.disconnect();
			client = null;
		}

		public static void send(object obj)
		{
			if (client == null)
				throw new ArgumentNullException("client");

			client.send(obj);
		}

		public static void sendSync(object obj)
		{
			if (client == null)
				throw new ArgumentNullException("client");

			client.sendSync(obj);
		}
	}
}
