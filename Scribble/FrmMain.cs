using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public partial class FrmMain : DarkTheme.Form
	{
		//private PageStart pageStart;
		//private PageHost pageHost;
		//private PageJoin pageJoin;
		//private PageLobby pageLobby;
		private Stack<DarkTheme.Page> stackPages;

		public AdvancedNetworkLib.Server Server { get; private set; }
		public AdvancedNetworkLib.Client Client { get; private set; }

		public FrmMain()
		{
			InitializeComponent();

			Message.init(this);

			this.stackPages = new Stack<DarkTheme.Page>();

			//this.SizeChanged += this.FrmMain_SizeChanged;

			this.FormClosed += FrmMain_FormClosed;

			//this.pageStart = new PageStart();
			//this.pageStart.MouseDown += (s, e) => this.OnMouseDown(e);
			//this.pageStart.MouseMove += (s, e) => this.OnMouseMove(e);
			//this.pageStart.btnHost.Click += BtnHost_Click;
			//this.pageStart.btnJoin.Click += BtnJoin_Click;
			//this.pageStart.btnLobby.Click += BtnLobby_Click;
			//this.Controls.Add(this.pageStart);

			//this.pageHost = new PageHost();
			//this.pageHost.MouseDown += (s, e) => this.OnMouseDown(e);
			//this.pageHost.MouseMove += (s, e) => this.OnMouseMove(e);
			//this.pageHost.Visible = false;
			//this.pageHost.btnBack.Click += BtnBack_Click;
			//this.Controls.Add(this.pageHost);

			//this.pageJoin = new PageJoin();
			//this.pageJoin.MouseDown += (s, e) => this.OnMouseDown(e);
			//this.pageJoin.MouseMove += (s, e) => this.OnMouseMove(e);
			//this.pageJoin.Visible = false;
			//this.Controls.Add(this.pageJoin);

			//this.pageLobby = new PageLobby();
			//this.pageLobby.MouseDown += (s, e) => this.OnMouseDown(e);
			//this.pageLobby.MouseMove += (s, e) => this.OnMouseMove(e);
			//this.pageLobby.Visible = false;
			//this.pageLobby.btnExit.Click += BtnExit_Click;
			//this.Controls.Add(this.pageLobby);

			//this.Width = 1366 / 2;
			//this.Height = 768 / 2;

			this.Server = new AdvancedNetworkLib.Server(this);
			this.Server.ErrorOccurred += Server_ErrorOccurred;
			this.Server.StateChanged += Server_StateChanged;

			this.Client = new AdvancedNetworkLib.Client(this);
			this.Client.ErrorOccurred += Client_ErrorOccurred;
			this.Client.ConnectionChanged += Client_ConnectionChanged;

			this.openPage(new PageStart());
		}

		private void Client_ConnectionChanged(object sender, AdvancedNetworkLib.ConnectionChangedEventArgs e)
		{
			if (e.Connected)
			{
				this.closeCurrentPageAndOpenNewPage(new PageLobby());
			}
			else
			{
				if (e.Lost)
				{
					this.closeCurrentPage();
					MessageBox.Show("Der Server hatte kein Bock mehr!");
				}
			}
		}

		private void Client_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
		}

		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Server?.stop();
		}

		private void Server_StateChanged(object sender, AdvancedNetworkLib.StateChangedEventArgs e)
		{
			if (e.Listening)
			{
				this.closeCurrentPageAndOpenNewPage(new PageLobby());
			}
		}

		private void Server_ErrorOccurred(object sender, AdvancedNetworkLib.ErrorOccurredEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
		}

		public void openPage(DarkTheme.Page page)
		{
			if (this.stackPages.Count > 0)
			{
				this.stackPages.Peek().Enabled = false;
				this.stackPages.Peek().Hide();
			}

			page.MouseDown += (s, e) => this.OnMouseDown(e);
			page.MouseMove += (s, e) => this.OnMouseMove(e);
			page.MouseDoubleClick += (s, e) => this.ToogleMaximized();
			this.SizeChanged += page.UpdateSize;

			if (this.stackPages.Count == 1)
			{
				this.BackClicked += this.closeCurrentPage;
			}

			this.Controls.Add(page);
			this.stackPages.Push(page);
			page.Show();
		}

		public void closeCurrentPage(object sender, EventArgs e)
		{
			this.closeCurrentPage();
		}
		public void closeCurrentPage()
		{
			// TODO: returning to previous page is buggy (skips previous page and goes to first page sometimes)
			if (this.stackPages.Count > 1)
			{
				DarkTheme.Page currentPage = this.stackPages.Peek();
				currentPage.Close();

				this.SizeChanged -= currentPage.UpdateSize;

				this.Controls.Remove(currentPage);
				this.stackPages.Pop();
				currentPage.Dispose();

				if (this.stackPages.Count == 1)
				{
					this.BackClicked -= this.closeCurrentPage;
				}

				this.stackPages.Peek().Enabled = true;
				this.stackPages.Peek().Show();

			}
		}
		public void closeCurrentPageAndOpenNewPage(DarkTheme.Page page)
		{
			if (this.stackPages.Count > 1)
			{
				DarkTheme.Page currentPage = this.stackPages.Peek();
				currentPage.Close();

				//this.BackClicked -= this.closeCurrentPage;
				this.SizeChanged -= currentPage.UpdateSize;

				this.Controls.Remove(currentPage);
				this.stackPages.Pop();
				currentPage.Dispose();

				if (this.stackPages.Count == 0)
				{
					this.BackClicked -= this.closeCurrentPage;
				}
			}

			page.MouseDown += (s, e) => this.OnMouseDown(e);
			page.MouseMove += (s, e) => this.OnMouseMove(e);
			page.MouseDoubleClick += (s, e) => this.ToogleMaximized();
			this.SizeChanged += page.UpdateSize;

			this.Controls.Add(page);
			this.stackPages.Push(page);
			page.Show();

			if (this.stackPages.Count == 1)
			{
				this.BackClicked += this.closeCurrentPage;
			}
		}

		private void FrmMain_KeyDown(object sender, KeyEventArgs e)
		{
			Console.WriteLine(e.KeyCode);
		}

		//public void showPageLobby(Server server)
		//{
		//	this.pageHost.Visible = false;
		//	this.pageLobby.Server = server;
		//	this.pageLobby.Visible = true;
		//}

		//public void showPageLobby(Client client)
		//{
		//	this.pageJoin.Visible = false;
		//	this.pageLobby.Client = client;
		//	this.pageLobby.Visible = true;
		//}

		//private void BtnLobby_Click(object sender, EventArgs e)
		//{
		//	this.pageStart.Visible = false;
		//	this.pageLobby.Visible = true;
		//}

		//private void BtnExit_Click(object sender, EventArgs e)
		//{
		//	this.pageStart.Visible = true;
		//	this.pageLobby.Visible = false;
		//}

		//private void BtnJoin_Click(object sender, EventArgs e)
		//{
		//	this.BackClicked += FrmMain_BackClicked;
		//	this.UpdateBackButton();

		//	this.pageStart.Visible = false;
		//	this.pageJoin.Visible = true;
		//}

		//private void FrmMain_BackClicked(object sender, EventArgs e)
		//{
		//	this.BackClicked -= null;
		//	this.UpdateBackButton();

		//	this.pageStart.Visible = true;
		//	this.pageJoin.Visible = false;
		//}

		//private void BtnBack_Click(object sender, EventArgs e)
		//{
		//	this.pageStart.Visible = true;
		//	this.pageHost.Visible = false;
		//}

		//private void BtnHost_Click(object sender, EventArgs e)
		//{
		//	this.pageStart.Visible = false;
		//	this.pageHost.Visible = true;
		//}

		//private void FrmMain_SizeChanged(object sender, EventArgs e)
		//{
		//	this.pageStart.Width = this.Width - 2;
		//	this.pageStart.Height = this.Height - (24 + 2);

		//	this.pageHost.Width = this.Width - 2;
		//	this.pageHost.Height = this.Height - (24 + 2);

		//}
	}
}
