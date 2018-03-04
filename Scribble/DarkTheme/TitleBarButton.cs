using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkTheme
{
	class TitleBarButton : System.Windows.Forms.Button
	{
		public enum EType
		{
			Minimize,
			Maximize,
			Restore,
			Close,
			Back
		}

		private EType type;
		public EType Type
		{
			get
			{
				return this.type;
			}

			set
			{
				this.type = value;
				this.Width = (this.type == EType.Back ? 48 : 24);
			}
		}

		public TitleBarButton()
		{
			this.Size = new Size(24, 24);
			this.BackColor = System.Drawing.Color.Transparent;
			this.FlatStyle = FlatStyle.Flat;
			this.FlatAppearance.BorderSize = 0;
			this.FlatAppearance.MouseOverBackColor = Color.TitleBarButtonOver;
			this.FlatAppearance.MouseDownBackColor = Color.TitleBarButtonDown;
			this.FlatAppearance.CheckedBackColor = Color.TitleBarButtonOver;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Pen pen = new Pen(System.Drawing.Color.White, 2);
			var g = e.Graphics;

			switch (this.Type)
			{
				case EType.Minimize:
					g.DrawLine(pen,  5, 18, 19, 18);
					break;

				case EType.Maximize:
					g.DrawLine(pen,  5,  6, 19,  6); // Top 1
					g.DrawLine(pen,  5,  8, 19,  8); // Top 2
					g.DrawLine(pen,  5, 18, 19, 18); // Bottom
					g.DrawLine(pen,  6,  9,  6, 17); // Left
					g.DrawLine(pen, 18,  9, 18, 17); // Right
					break;

				case EType.Restore:
					g.DrawLine(pen, 5, 12, 13, 12); // Top 1
					g.DrawLine(pen, 5, 14, 13, 14); // Top 2
					g.DrawLine(pen, 5, 18, 13, 18); // Bottom
					g.DrawLine(pen, 6, 15, 6, 17); // Left
					g.DrawLine(pen, 12, 15, 12, 17); // Right

					g.DrawLine(pen, 11,  6, 19,  6); // Top 1
					g.DrawLine(pen, 11,  8, 19,  8); // Top 2
					g.DrawLine(pen, 11, 12, 19, 12); // Bottom
					g.DrawLine(pen, 12,  9, 12, 11); // Left
					g.DrawLine(pen, 18,  9, 18, 11); // Right
					break;

				case EType.Close:
					g.DrawLine(pen, 4, 4, 18, 18);
					g.DrawLine(pen, 18, 4, 4, 18);
					break;

				case EType.Back:
					//g.DrawString("Zurück", new Font("Consolas", 12), Brushes.White, 0, 0);

					g.DrawLine(pen, 16, 12, 34, 12);
					g.DrawLine(pen, 16, 12, 22, 6);
					g.DrawLine(pen, 16, 12, 22, 18);
					break;
			}
		}
	}
}
