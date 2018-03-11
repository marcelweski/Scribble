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
		private Stack<DarkTheme.Page> stackPages;

		public FrmMain()
		{
			InitializeComponent();

			Message.init(this);

			this.stackPages = new Stack<DarkTheme.Page>();

			this.FormClosed += FrmMain_FormClosed;

			this.openPage(new PageStart());
		}

		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Server.stop();
			Client.disconnect();
		}

		// Page Control Methods
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
