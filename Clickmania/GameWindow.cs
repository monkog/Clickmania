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

			for (var i = 0; i < GameBoard.RowCount; ++i)
			{
				GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)100.0 / _game.Board.Rows));
				GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, (float)100.0 / _game.Board.Columns));

				for (var j = 0; j < GameBoard.ColumnCount; ++j)
				{
					var field = new PictureBox
					{
						BackColor = _game.Board.GetColor(),
						Margin = new Padding(0),
						Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
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
				RemoveFieldInHardVersion(column, row);
			}
		}

		private void RemoveFieldInHardVersion(int c, int row)
		{
			var currentControl = GameBoard.GetControlFromPosition(c, row);

			if (currentControl.BackColor != SystemColors.Control)
			{
				Check(c, row, currentControl.BackColor);

				if (_indexes.Count > 1)
				{
					var columns = _indexes.GroupBy(i => i[0], i => i[1]);
					foreach (var column in columns)
					{
						RemoveControlsInColumn(column.Key, column.ToArray());
					}

					UpdateHighScoreList();
				}

				_indexes.Clear();
				_visited = new bool[_game.Board.Columns, _game.Board.Rows];
			}

			CheckIsGameOver();
		}

		private void RemoveFieldInEasyVersion(int row, int column)
		{
			RemoveControlsInColumn(column, row);
			UpdateHighScoreList();
			CheckIsGameOver();
		}

		private void RemoveControlsInColumn(int column, params int[] rows)
		{
			var orderedRows = rows.OrderBy(r => r);

			foreach (var row in orderedRows)
			{
				for (var i = row; i > 0; i--)
				{
					var currentControl = GameBoard.GetControlFromPosition(column, i);
					var previousControl = GameBoard.GetControlFromPosition(column, i - 1);
					currentControl.BackColor = previousControl.BackColor;
				}

				var control = GameBoard.GetControlFromPosition(column, 0);
				control.BackColor = SystemColors.Control;
			}

			_game.AddPoints(rows.Length);
		}

		private void UpdateHighScoreList()
		{
			HighScoreList.Items.Clear();

			var currentScore = new ListViewItem(string.Format(Properties.Resources.CurrentScore, _game.Score));
			HighScoreList.Items.Add(currentScore);

			var highScores = HighScoreRegistry.GetAllRecords().Select(score => new ListViewItem(score));
			HighScoreList.Items.AddRange(highScores.ToArray());
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
