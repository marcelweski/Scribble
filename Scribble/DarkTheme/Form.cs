using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkTheme;
using System.Runtime.InteropServices;


namespace DarkTheme
{
	public class Form: System.Windows.Forms.Form
	{
		private Label lblTitle;
		private Point lastMousePos;

		private Panel pnlResizeRight;
		private Panel pnlResizeCorner;
		private Panel pnlResizeBottom;

		private TitleBarButton btnMinimize;
		private TitleBarButton btnMaximize;
		private TitleBarButton btnClose;
		private TitleBarButton btnBack;

		private bool backClickedSet = false;
		public event EventHandler BackClicked
		{
			add
			{
				this.btnBack.Click += value;
				this.backClickedSet = true;
				this.UpdateBackButton();
			}

			remove
			{
				this.btnBack.Click -= null;
				this.backClickedSet = false;
				this.UpdateBackButton();
			}
		}

		public int BorderOffset { get => (this.WindowState == FormWindowState.Maximized ? 0 : 1); }

		public Form()
		{
			this.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
			this.FormBorderStyle = FormBorderStyle.None;

			this.DoubleBuffered = true;

			this.lblTitle = new Label();
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.lblTitle.Location = new Point(5, 6);
			this.lblTitle.Height = 16;
			this.lblTitle.Font = new Font("Consolas", 9.0f);
			this.lblTitle.BackColor = this.BackColor;
			this.lblTitle.MouseDown += (s, e) => this.OnMouseDown(e);
			this.lblTitle.MouseMove += (s, e) => this.OnMouseMove(e);
			this.lblTitle.MouseDoubleClick += (s, e) => this.ToogleMaximized();
			this.Controls.Add(this.lblTitle);

			this.btnMinimize = new TitleBarButton();
			this.btnMinimize.Top = 1;
			this.btnMinimize.Type = TitleBarButton.EType.Minimize;
			this.btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
			this.btnMinimize.TabStop = false;
			this.Controls.Add(this.btnMinimize);

			this.btnMaximize = new TitleBarButton();
			this.btnMaximize.Top = 1;
			this.btnMaximize.Type = TitleBarButton.EType.Maximize;
			this.btnMaximize.Click += (s, e) =>
			{
				this.ToogleMaximized();
				this.btnMaximize.Type = (this.WindowState == FormWindowState.Maximized ? TitleBarButton.EType.Restore : TitleBarButton.EType.Maximize);
			};
			this.btnMaximize.TabStop = false;
			this.Controls.Add(this.btnMaximize);

			this.btnClose = new TitleBarButton();
			this.btnClose.Top = 1;
			this.btnClose.Type = TitleBarButton.EType.Close;
			this.btnClose.Click += (s, e) => this.Close();
			this.btnClose.TabStop = false;
			this.Controls.Add(this.btnClose);

			this.btnBack = new TitleBarButton();
			this.btnBack.Top = 1;
			this.btnBack.Left = 1;
			this.btnBack.Type = TitleBarButton.EType.Back;
			this.btnBack.TabStop = false;
			this.Controls.Add(this.btnBack);

			this.pnlResizeRight = new Panel();
			this.pnlResizeRight.Top = 24 + 1;
			this.pnlResizeRight.Width = 6;
			this.pnlResizeRight.BackColor = System.Drawing.Color.Transparent;
			this.pnlResizeRight.Cursor = Cursors.SizeWE;
			this.pnlResizeRight.MouseDown += (s, e) =>
			{
				this.lastMousePos = e.Location;
			};
			this.pnlResizeRight.MouseMove += (s, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Width += e.X - this.lastMousePos.X;
				}
			};
			this.Controls.Add(this.pnlResizeRight);

			this.pnlResizeCorner = new Panel();
			this.pnlResizeCorner.Width = 6;
			this.pnlResizeCorner.Height = 6;
			this.pnlResizeCorner.BackColor = System.Drawing.Color.Transparent;
			this.pnlResizeCorner.Cursor = Cursors.SizeNWSE;
			this.pnlResizeCorner.MouseDown += (s, e) =>
			{
				this.lastMousePos = e.Location;
			};
			this.pnlResizeCorner.MouseMove += (s, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Width += e.X - this.lastMousePos.X;
					this.Height += e.Y - this.lastMousePos.Y;
				}
			};
			this.Controls.Add(this.pnlResizeCorner);

			this.pnlResizeBottom = new Panel();
			this.pnlResizeBottom.Left = 0;
			this.pnlResizeBottom.Height = 6;
			this.pnlResizeBottom.BackColor = System.Drawing.Color.Transparent;
			this.pnlResizeBottom.Cursor = Cursors.SizeNS;
			this.pnlResizeBottom.MouseDown += (s, e) =>
			{
				this.lastMousePos = e.Location;
			};
			this.pnlResizeBottom.MouseMove += (s, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Height += e.Y - this.lastMousePos.Y;
				}
			};
			this.Controls.Add(this.pnlResizeBottom);

			this.MinimumSize = new Size(1100, 600);

			// Events
			this.MouseDown += Form_MouseDown;
			this.MouseMove += Form_MouseMove;
			this.MouseDoubleClick += (s, e) => this.ToogleMaximized();
		}

		public void Form_MouseMove(object sender, MouseEventArgs e)
		{
			//if (e.Button == MouseButtons.Left)
			//{
			//	this.Left += e.X - this.lastMousePos.X;
			//	this.Top += e.Y - this.lastMousePos.Y;
			//}
		}

		private const int WM_NCHITTEST = 0x84;
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		public void Form_MouseDown(object sender, MouseEventArgs e)
		{
			//this.lastMousePos = e.Location;
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		//protected override void WndProc(ref Message m)
		//{
		//	switch (m.Msg)
		//	{
		//		case 0x84:
		//			base.WndProc(ref m);
		//			if ((int)m.Result == 0x1)
		//				m.Result = (IntPtr)0x2;
		//			return;
		//	}

		//	base.WndProc(ref m);
		//}

		public void ToogleMaximized()
		{
			this.WindowState = (this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized);
		}

		public void UpdateBackButton()
		{
			this.lblTitle.Top = this.BorderOffset + 6;
			this.btnBack.Top = this.BorderOffset;
			this.btnBack.Left = this.BorderOffset;
			if (this.backClickedSet)
			{
				this.lblTitle.Left = this.BorderOffset + this.btnBack.Width + 5;
				this.lblTitle.Width = this.Width - (this.BorderOffset + this.btnBack.Width + this.lblTitle.Left + 24 * 3 + 5);
				this.btnBack.Visible = true;
			}
			else
			{
				this.lblTitle.Left = this.BorderOffset + 5;
				this.lblTitle.Width = this.Width - (this.BorderOffset + this.lblTitle.Left + 24 * 3 + 5);
				this.btnBack.Visible = false;
			}
		}

		private void UpdateSize()
		{
			int offset = (this.WindowState == FormWindowState.Maximized ? 0 : 1);

			this.btnMaximize.Type = (this.WindowState == FormWindowState.Maximized ? TitleBarButton.EType.Restore : TitleBarButton.EType.Maximize);

			this.btnClose.Top = offset;
			this.btnClose.Left = (this.Width - this.btnClose.Width) - offset;
			this.btnMaximize.Top = offset;
			this.btnMaximize.Left = this.btnClose.Left - this.btnMaximize.Width;
			this.btnMinimize.Top = offset;
			this.btnMinimize.Left = this.btnMaximize.Left - this.btnMinimize.Width;

			this.UpdateBackButton();

			if (this.WindowState == FormWindowState.Maximized)
			{
				this.pnlResizeRight.Visible = false;
				this.pnlResizeCorner.Visible = false;
				this.pnlResizeBottom.Visible = false;
			}
			else
			{
				this.pnlResizeRight.Visible = true;
				this.pnlResizeCorner.Visible = true;
				this.pnlResizeBottom.Visible = true;

				this.pnlResizeRight.Left = this.Width - this.pnlResizeRight.Width;
				this.pnlResizeRight.Height = this.Height - (offset + 24 + this.pnlResizeCorner.Height);

				this.pnlResizeCorner.Top = this.Height - this.pnlResizeCorner.Height;
				this.pnlResizeCorner.Left = this.Width - this.pnlResizeCorner.Width;

				this.pnlResizeBottom.Top = this.Height - this.pnlResizeBottom.Height;
				this.pnlResizeBottom.Width = this.Width - this.pnlResizeCorner.Width;
			}

			this.Refresh();
		}

		///////////////////////////////////////////////////////////////////////////////
		/// Overridden Events /////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			this.UpdateSize();
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.WindowState = (this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized);
		}
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.lblTitle.Text = this.Text;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (this.WindowState != FormWindowState.Maximized)
				e.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, this.Width-1, this.Height-1);
		}
	}
}
