using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media;

namespace Scribble
{
	class DrawField : Control
	{
		private Bitmap bitmap;
		private Point lastMousePos;
		private System.Drawing.Pen pen;

		public float PenWidth { get => this.pen.Width; set => this.pen.Width = value; }
		public System.Drawing.Color PenColor { get => this.pen.Color; set => this.pen.Color = value; }
		public System.Drawing.Color BorderColor { get; set; }

		private bool drawing;

		public enum EMode
		{
			Draw,
			Fill
		}
		public EMode Mode;

		public event EventHandler BitmapChanged;

		public DrawField()
		{
			this.Size = new Size(600, 400);
			this.BackColor = System.Drawing.Color.White;

			this.DoubleBuffered = true;

			this.bitmap = new Bitmap(640, 360, this.CreateGraphics());

			this.MouseDown += DrawField_MouseDown;
			this.MouseUp += DrawField_MouseUp;
			this.MouseMove += DrawField_MouseMove;

			this.Paint += DrawField_Paint;

			this.pen = new System.Drawing.Pen(System.Drawing.Color.Black, 10);
			this.pen.StartCap = LineCap.Round;
			this.pen.EndCap = LineCap.Round;

			this.BorderColor = System.Drawing.Color.Black;

			this.drawing = false;
			this.Mode = EMode.Draw;

			this.reset();
		}

		public Bitmap getBitmapClone()
		{
			return (Bitmap)this.bitmap.Clone();
		}

		public void updateBitmap(Bitmap bitmap)
		{
			this.bitmap = bitmap;
			this.Refresh();
		}

		public void reset()
		{
			Graphics g = Graphics.FromImage(this.bitmap);
			g.Clear(System.Drawing.Color.White);
			this.Refresh();
		}

		public void toogleMode()
		{
			this.Mode = (this.Mode == EMode.Draw ? EMode.Fill : EMode.Draw);
		}

		private bool RGBequal(System.Drawing.Color c1, System.Drawing.Color c2)
		{
			return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
		}

		private void floodFill(Bitmap bitmap, int x, int y, System.Drawing.Color newColor)
		{
			var oldColor = bitmap.GetPixel(x, y);
			if (RGBequal(oldColor, newColor))
				return;

			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Restart();

			Stack<Point> stack = new Stack<Point>();
			stack.Push(new Point(x, y));

			while (stack.Count > 0)
			{
				Point p = stack.Pop();

				if (RGBequal(bitmap.GetPixel(p.X, p.Y), oldColor))
				{
					bitmap.SetPixel(p.X, p.Y, newColor);

					if (p.Y < bitmap.Height - 1) stack.Push(new Point(p.X, p.Y + 1));
					if (p.Y > 0)                 stack.Push(new Point(p.X, p.Y - 1));
					if (p.X < bitmap.Width - 1)  stack.Push(new Point(p.X + 1, p.Y));
					if (p.X > 0)                 stack.Push(new Point(p.X - 1, p.Y));
				}

				//if (stack.Count > bitmap.Width * bitmap.Height * 10)
				//	break;
			}

			stopwatch.Stop();
			Console.WriteLine($"FloodFill took {stopwatch.ElapsedMilliseconds} ms");

			this.Refresh();
		}

		private void DrawField_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

			e.Graphics.DrawImage(this.bitmap, 0, 0, this.Width+1, this.Height+1);
			//Console.WriteLine(this.Height);
			//e.Graphics.DrawRectangle(new System.Drawing.Pen(this.BorderColor, 4.0f), 0, 0, this.Width-1, this.Height-1);

			this.BitmapChanged?.Invoke(this, EventArgs.Empty);
		}

		private void DrawField_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.Mode == EMode.Draw && this.drawing)
			{
				Graphics g = Graphics.FromImage(this.bitmap);
				//g.SmoothingMode = SmoothingMode.HighQuality;
				g.DrawLine(this.pen, this.lastMousePos.X * ((float)this.bitmap.Width / this.Width), this.lastMousePos.Y * ((float)this.bitmap.Height / this.Height), e.Location.X * ((float)this.bitmap.Width / this.Width), e.Location.Y * ((float)this.bitmap.Height / this.Height));

				this.Refresh();

				this.lastMousePos = e.Location;
			}
		}

		private void DrawField_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.drawing = false;
			}
		}

		private void DrawField_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.Mode == EMode.Draw)
				{
					this.lastMousePos = e.Location;
					this.drawing = true;

					try
					{
						Graphics g = Graphics.FromImage(this.bitmap);
						//g.SmoothingMode = SmoothingMode.HighQuality;
						g.DrawLine(this.pen, this.lastMousePos.X * ((float)this.bitmap.Width / this.Width), this.lastMousePos.Y * ((float)this.bitmap.Height / this.Height), this.lastMousePos.X * ((float)this.bitmap.Width / this.Width) + 1, this.lastMousePos.Y * ((float)this.bitmap.Height / this.Height) + 1);
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.StackTrace + "\n" + exc.Message);
					}

					this.Refresh();
				}
				else // Fill
				{
					int x = (int)(e.Location.X * ((float)this.bitmap.Width / this.Width));
					int y = (int)(e.Location.Y * ((float)this.bitmap.Height / this.Height));

					System.Drawing.Color newColor = this.PenColor;

					{
						this.floodFill(this.bitmap, x, y, newColor);
					}
				}
			}
		}
	}
}
