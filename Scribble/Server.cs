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

		public static IEnumerable<AdvancedNetworkLib.Client> Clients { get => server?.Clients; }

		public static int PasswordHash;

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
