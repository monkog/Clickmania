using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clickmania
{
	public partial class GameWindow : Form
	{
		private Game _game;
		private readonly List<int[]> _indexes = new List<int[]>();
		private bool[,] _visited;

		public GameWindow()
		{
			InitializeComponent();
		}

		private void InitializeGame(object sender, EventArgs e)
		{
			StartGame(5, 5, 5, true);
		}

		private void StartGame(int width, int height, int colorNumber, bool isEasyVersion)
		{
			if (_game != null && _game.Score != 0)
				HighScoreRegistry.AddHighScore(_game.Score, GameBoard.RowCount * GameBoard.ColumnCount);

			_game = new Game(width, height, colorNumber, isEasyVersion);

			GameBoard.Controls.Clear();
			GameBoard.ColumnStyles.Clear();
			GameBoard.RowStyles.Clear();
			GameBoard.AutoSize = true;
			GameBoard.ColumnCount = _game.Board.Columns;
			GameBoard.RowCount = _game.Board.Rows;
			_visited = new bool[_game.Board.Columns, _game.Board.Rows];

			for (int i = 0; i < GameBoard.RowCount; ++i)
			{
				GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / _game.Board.Rows));
				GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / _game.Board.Columns));

				for (int j = 0; j < GameBoard.ColumnCount; ++j)
				{
					var field = new PictureBox
					{
						BackColor = _game.Board.GetColor(),
						Margin = new Padding(0),
						SizeMode = PictureBoxSizeMode.Zoom,
						Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
						ClientSize = new Size(GameBoard.Size.Width / _game.Board.Columns, GameBoard.Size.Height / _game.Board.Rows)
					};
					field.Click += RemoveField;

					GameBoard.Controls.Add(field);
				}
			}
		}

		private void RemoveField(object sender, EventArgs e)
		{
			var control = sender as Control;
			var row = control.TabIndex / _game.Board.Columns;
			var column = control.TabIndex % _game.Board.Columns;

			if (_game.IsEasyVersion && control.BackColor != SystemColors.Control)
			{
				RemoveFieldInEasyVersion(row, column);
			}
			else
			{
				RemoveFieldInHardVersion(column, row, control);
			}
		}

		private void RemoveFieldInHardVersion(int column, int row, Control control)
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
						_game.Board.RemoveField();
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
											GameBoard.GetControlFromPosition(j, k + 1).BackColor =
												GameBoard.GetControlFromPosition(j, k).BackColor;
										GameBoard.GetControlFromPosition(j, 0).BackColor = SystemColors.Control;
									}
							}
					}

					if (_indexes.Count > 1)
					{
						_game.AddPoints(_indexes.Count);

						HighScoreList.Items.Clear();

						ListViewItem lvi1 = new ListViewItem("Current: " + _game.Score);
						HighScoreList.Items.Add(lvi1);

						var highScores = HighScoreRegistry.GetAllRecords().Select(score => new ListViewItem(score));
						HighScoreList.Items.AddRange(highScores.ToArray());
					}
				}

				_indexes.Clear();
				for (int i = 0; i < _game.Board.Columns; i++)
				for (int j = 0; j < _game.Board.Rows; j++)
					_visited[i, j] = false;
			}

			CheckIsGameOver();
		}

		private void RemoveFieldInEasyVersion(int row, int column)
		{
			for (int i = row; i > 0; i--)
			{
				Control con1 = GameBoard.GetControlFromPosition(column, i);
				Control con2 = GameBoard.GetControlFromPosition(column, i - 1);
				con1.BackColor = con2.BackColor;
			}

			Control con = GameBoard.GetControlFromPosition(column, 0);
			con.BackColor = SystemColors.Control;
			_game.Board.RemoveField();
			_game.AddPoints(1);
			HighScoreList.Items.Clear();

			ListViewItem lvi1 = new ListViewItem("Current: " + _game.Score);
			HighScoreList.Items.Add(lvi1);

			var highScores = HighScoreRegistry.GetAllRecords().Select(score => new ListViewItem(score));
			HighScoreList.Items.AddRange(highScores.ToArray());

			CheckIsGameOver();
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

		private void CheckIsGameOver()
		{
			if (_game.Board.AllFieldsRemoved)
				MessageBox.Show(Properties.Resources.GameWon);
		}

		private void ChooseEasyVersion(object sender, EventArgs e)
		{
			_game.IsEasyVersion = true;
			HighScoreList.Visible = false;
			GameBoard.Width = Width;
		}

		private void ChooseHardVersion(object sender, EventArgs e)
		{
			_game.IsEasyVersion = false;
			HighScoreList.Visible = true;
			GameBoard.Width = Width - HighScoreList.Width;
		}

		private void ChangeColorNumber(object sender, EventArgs e)
		{
			StartGame(_game.Board.Columns, _game.Board.Rows, trackBar.Value, _game.IsEasyVersion);
		}

		private void Start5X5Game(object sender, EventArgs e)
		{
			StartGame(5, 5, _game.Board.ColorNumber, _game.IsEasyVersion);
		}

		private void Start10X10Game(object sender, EventArgs e)
		{
			StartGame(10, 10, _game.Board.ColorNumber, _game.IsEasyVersion);
		}

		private void Start5X10Game(object sender, EventArgs e)
		{
			StartGame(5, 10, _game.Board.ColorNumber, _game.IsEasyVersion);
		}

		private void Start10X5Game(object sender, EventArgs e)
		{
			StartGame(10, 5, _game.Board.ColorNumber, _game.IsEasyVersion);
		}
	}
}
