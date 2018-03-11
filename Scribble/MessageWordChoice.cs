using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
	public class WordChoosenEventArgs: EventArgs
	{
		public string Word { get; set; }
	}

	public class MessageWordChoice : UserControl
	{
		public DarkTheme.Button btnSecondWord;
		public DarkTheme.Button btnThirdWord;
		public DarkTheme.Label lblTitle;

		//private PictureBox pictureBox;
		private DarkTheme.Button btnFirstWord;

		private event EventHandler<WordChoosenEventArgs> WordChoosen;

		public static void Open(Control parent, WordChoice wordChoice, EventHandler<WordChoosenEventArgs> wordChoosen)
		{
			// disable the page control
			foreach (Control control in parent.Controls)
			{
				if (control is Page)
					control.Enabled = false;
			}

			var msg = new MessageWordChoice();
			msg.btnFirstWord.Text = wordChoice.Words[0];
			msg.btnSecondWord.Text = wordChoice.Words[1];
			msg.btnThirdWord.Text = wordChoice.Words[2];
			msg.WordChoosen += wordChoosen;
			parent.SizeChanged += msg.OnSizeChanged;
			parent.Controls.Add(msg);
			msg.Disposed += (s, e) => Msg_Disposed(parent, EventArgs.Empty);
			msg.OnSizeChanged(parent, EventArgs.Empty);
			msg.Show();
			msg.BringToFront();
		}

		private static void Msg_Disposed(object sender, EventArgs e)
		{
			// enable the page control
			foreach (Control control in (sender as Control).Controls)
			{
				if (control is Page)
					control.Enabled = true;
			}
		}

		public MessageWordChoice()
		{
			this.InitializeComponent();
			//this.pictureBox.Image = 

			//var bytes = new byte[] { 255, 0, 0, 0 };
			//using (var ms = new System.IO.MemoryStream(bytes))
			{
				//this.BackgroundImage = 
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			//e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 0, 0, 0)), 0, 0, 100, 100);
		}

		public void OnSizeChanged(object sender, EventArgs e)
		{
			var form = sender as FrmMain;
			this.Top = (form.Height / 2) - (180 / 2);
			this.Width = form.Width;
			this.Height = 180;
			//this.pictureBox.Size = this.Size;
		}

		private void InitializeComponent()
		{
			this.btnThirdWord = new DarkTheme.Button();
			this.btnSecondWord = new DarkTheme.Button();
			this.btnFirstWord = new DarkTheme.Button();
			this.lblTitle = new DarkTheme.Label();
			this.SuspendLayout();
			// 
			// btnThirdWord
			// 
			this.btnThirdWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnThirdWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnThirdWord.FlatAppearance.BorderSize = 0;
			this.btnThirdWord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnThirdWord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnThirdWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThirdWord.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnThirdWord.ForeColor = System.Drawing.Color.White;
			this.btnThirdWord.Location = new System.Drawing.Point(535, 100);
			this.btnThirdWord.Name = "btnThirdWord";
			this.btnThirdWord.Size = new System.Drawing.Size(200, 50);
			this.btnThirdWord.TabIndex = 2;
			this.btnThirdWord.Text = "Drittes Wort";
			this.btnThirdWord.UseVisualStyleBackColor = false;
			this.btnThirdWord.Click += new System.EventHandler(this.btnThirdWord_Click);
			// 
			// btnSecondWord
			// 
			this.btnSecondWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSecondWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnSecondWord.FlatAppearance.BorderSize = 0;
			this.btnSecondWord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnSecondWord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnSecondWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSecondWord.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnSecondWord.ForeColor = System.Drawing.Color.White;
			this.btnSecondWord.Location = new System.Drawing.Point(329, 100);
			this.btnSecondWord.Name = "btnSecondWord";
			this.btnSecondWord.Size = new System.Drawing.Size(200, 50);
			this.btnSecondWord.TabIndex = 1;
			this.btnSecondWord.Text = "Zweites Wort";
			this.btnSecondWord.UseVisualStyleBackColor = false;
			this.btnSecondWord.Click += new System.EventHandler(this.btnSecondWord_Click);
			// 
			// btnFirstWord
			// 
			this.btnFirstWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnFirstWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnFirstWord.FlatAppearance.BorderSize = 0;
			this.btnFirstWord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnFirstWord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
			this.btnFirstWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirstWord.Font = new System.Drawing.Font("Consolas", 12F);
			this.btnFirstWord.ForeColor = System.Drawing.Color.White;
			this.btnFirstWord.Location = new System.Drawing.Point(123, 100);
			this.btnFirstWord.Name = "btnFirstWord";
			this.btnFirstWord.Size = new System.Drawing.Size(200, 50);
			this.btnFirstWord.TabIndex = 0;
			this.btnFirstWord.Text = "Erstes Wort";
			this.btnFirstWord.UseVisualStyleBackColor = false;
			this.btnFirstWord.Click += new System.EventHandler(this.btnFirstWord_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 20F);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(305, 32);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(244, 37);
			this.lblTitle.TabIndex = 3;
			this.lblTitle.Text = "Wähle ein Wort aus";
			// 
			// MessageWordChoice
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnThirdWord);
			this.Controls.Add(this.btnSecondWord);
			this.Controls.Add(this.btnFirstWord);
			this.Name = "MessageWordChoice";
			this.Size = new System.Drawing.Size(865, 454);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void btnFirstWord_Click(object sender, EventArgs e)
		{
			this.WordChoosen?.Invoke(this, new WordChoosenEventArgs { Word = this.btnFirstWord.Text });
			this.Dispose();
		}
		private void btnSecondWord_Click(object sender, EventArgs e)
		{
			this.WordChoosen?.Invoke(this, new WordChoosenEventArgs { Word = this.btnSecondWord.Text });
			this.Dispose();
		}
		private void btnThirdWord_Click(object sender, EventArgs e)
		{
			this.WordChoosen?.Invoke(this, new WordChoosenEventArgs { Word = this.btnThirdWord.Text });
			this.Dispose();
		}
	}
}
