using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clickmania
{
	public partial class GameWindow : Form
	{
		private Board _board;
		private int _score;
		private readonly List<int[]> _indexes = new List<int[]>();
		private bool[,] _visited;

		public GameWindow()
		{
			InitializeComponent();
		}

		private void InitializeGame(object sender, EventArgs e)
		{
			StartGame(5, 5, 5);
		}

		private void StartGame(int width, int height, int colorNumber)
		{
			_board = new Board(width, height, colorNumber);

			if (_score != 0)
			{
				HighScoreRegistry.AddHighScore(_score, GameBoard.RowCount * GameBoard.ColumnCount);
				_score = 0;
			}

			GameBoard.Controls.Clear();
			GameBoard.ColumnStyles.Clear();
			GameBoard.RowStyles.Clear();
			GameBoard.AutoSize = true;
			GameBoard.ColumnCount = _board.Width;
			GameBoard.RowCount = _board.Height;
			_visited = new bool[_board.Width, _board.Height];

			for (int i = 0; i < _board.Width; i++)
				for (int j = 0; j < _board.Height; j++)
					_visited[i, j] = false;

			for (int i = 0; i < GameBoard.RowCount; ++i)
			{
				GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / _board.Height));
				GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / _board.Width));

				for (int j = 0; j < GameBoard.ColumnCount; ++j)
				{
					var pctrCard = new PictureBox
					{
						BackColor = _board.GetColor(),
						Margin = new Padding(0),
						SizeMode = PictureBoxSizeMode.Zoom,
						Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
						ClientSize = new Size(GameBoard.Size.Width / _board.Width, GameBoard.Size.Height / _board.Height)
					};
					pctrCard.Click += pctrCard_Click;

					GameBoard.Controls.Add(pctrCard);
				}
			}
		}

		private void pctrCard_Click(object sender, EventArgs e)
		{
			var control = sender as Control;
			var row = control.TabIndex / _board.Width;
			var column = control.TabIndex % _board.Width;

			if (radioButtonC.Checked && control.BackColor != SystemColors.Control)
			{
				for (int i = row; i > 0; i--)
				{
					Control con1 = GameBoard.GetControlFromPosition(column, i);
					Control con2 = GameBoard.GetControlFromPosition(column, i - 1);
					con1.BackColor = con2.BackColor;
				}

				Control con = GameBoard.GetControlFromPosition(column, 0);
				con.BackColor = SystemColors.Control;
				_score++;
				HighScoreList.Items.Clear();

				ListViewItem lvi1 = new ListViewItem("Current: " + _score);
				HighScoreList.Items.Add(lvi1);

				var highScores = HighScoreRegistry.GetAllRecords().Select(score => new ListViewItem(score));
				HighScoreList.Items.AddRange(highScores.ToArray());

				Game_Over();
			}
			else
			{
				Control con = GameBoard.GetControlFromPosition(column, row);

				if (con.BackColor != SystemColors.Control)
				{
					Check(column, row, control.BackColor);
					if (_indexes.Count > 1)
					{
						for (int i = 0; i < _indexes.Count; i++)
						{
							int[] tab = _indexes[i];
							GameBoard.GetControlFromPosition(tab[0], tab[1]).BackColor = SystemColors.Control;
						}
						for (int i = GameBoard.RowCount - 1; i > 0; i--)
						{
							for (int j = 0; j < GameBoard.ColumnCount; j++)
								if (GameBoard.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
								{
									bool exists = false;
									for (int k = i - 1; k >= 0; k--)
										if (GameBoard.GetControlFromPosition(j, k).BackColor != SystemColors.Control)
											exists = true;

									if (exists)
										while (GameBoard.GetControlFromPosition(j, i).BackColor == SystemColors.Control)
										{
											for (int k = i - 1; k >= 0; k--)
												GameBoard.GetControlFromPosition(j, k + 1).BackColor = GameBoard.GetControlFromPosition(j, k).BackColor;
											GameBoard.GetControlFromPosition(j, 0).BackColor = SystemColors.Control;
										}
								}
						}
						if (_indexes.Count > 1)
						{
							_score = _score + _indexes.Count;

							HighScoreList.Items.Clear();

							ListViewItem lvi1 = new ListViewItem("Current: " + _score);
							HighScoreList.Items.Add(lvi1);

							var highScores = HighScoreRegistry.GetAllRecords().Select(score => new ListViewItem(score));
							HighScoreList.Items.AddRange(highScores.ToArray());

						}
					}
					_indexes.Clear();
					for (int i = 0; i < _board.Width; i++)
						for (int j = 0; j < _board.Height; j++)
							_visited[i, j] = false;
				}

				Game_Over();
			}
		}

		private void Check(int x, int y, Color c)
		{
			Control con2 = GameBoard.GetControlFromPosition(x, y);
			if (c == con2.BackColor)
			{
				_visited[x, y] = true;
				int[] tab = new int[2];
				tab[0] = x;
				tab[1] = y;
				_indexes.Add(tab);
				if (x > 0 && _visited[x - 1, y] == false)
					Check(x - 1, y, c);
				if (y > 0 && _visited[x, y - 1] == false)
					Check(x, y - 1, c);
				if (x < GameBoard.ColumnCount - 1 && _visited[x + 1, y] == false)
					Check(x + 1, y, c);
				if (y < GameBoard.RowCount - 1 && _visited[x, y + 1] == false)
					Check(x, y + 1, c);
			}
		}

		private void Game_Over()
		{
			foreach (Control c in GameBoard.Controls)
			{
				if (c.BackColor != SystemColors.Control)
					return;
			}
			MessageBox.Show("Congratulations! You won!");
		}

		private void ChooseClassVersion(object sender, EventArgs e)
		{
			HighScoreList.Visible = false;
			GameBoard.Width = Width;
		}

		private void ChooseHomeVersion(object sender, EventArgs e)
		{
			HighScoreList.Visible = true;
			GameBoard.Width = Width - HighScoreList.Width;
		}

		private void ChangeColorNumber(object sender, EventArgs e)
		{
			StartGame(_board.Width, _board.Height, trackBar.Value);
		}

		private void Start5X5Game(object sender, EventArgs e)
		{
			StartGame(5, 5, _board.ColorNumber);
		}

		private void Start10X10Game(object sender, EventArgs e)
		{
			StartGame(10, 10, _board.ColorNumber);
		}

		private void Start5X10Game(object sender, EventArgs e)
		{
			StartGame(5, 10, _board.ColorNumber);
		}

		private void Start10X5Game(object sender, EventArgs e)
		{
			StartGame(10, 5, _board.ColorNumber);
		}
	}
}
