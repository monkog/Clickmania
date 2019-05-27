using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SameGame
{
	public partial class GameWindow : Form
	{
		private Game _game;

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

			if (control.BackColor == SystemColors.Control) return;

			if (_game.IsEasyVersion)
			{
				RemoveControlsInColumn(column, row);
				UpdateHighScoreList();
			}
			else
			{
				RemoveFieldInHardVersion(column, row, control);
			}

			CheckIsGameOver();
		}

		private void RemoveFieldInHardVersion(int column, int row, Control control)
		{
			var neighbors = GetSameColorNeighbors(column, row, control.BackColor);

			if (neighbors.Count <= 1) return;
			var columns = neighbors.GroupBy(n => n.X, n => n.Y);
			foreach (var c in columns)
			{
				RemoveControlsInColumn(c.Key, c.ToArray());
			}

			UpdateHighScoreList();
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

		private List<Point> GetSameColorNeighbors(int x, int y, Color color)
		{
			var startField = new Point(x, y);
			var neighbors = new List<Point> { startField };
			var visitedNeighbors = new List<Point> { startField };
			var sameColorFields = new List<Point>();

			while (neighbors.Any())
			{
				var currentField = neighbors.First();
				neighbors.Remove(currentField);
				sameColorFields.Add(currentField);

				var currentNeighbors = new[]
				{
					new Point(currentField.X - 1, currentField.Y),
					new Point(currentField.X, currentField.Y - 1),
					new Point(currentField.X + 1, currentField.Y),
					new Point(currentField.X, currentField.Y + 1)
				};

				foreach (var neighbor in currentNeighbors)
				{
					if (!CanVisitNeighbor(neighbor.X, neighbor.Y, color, visitedNeighbors)) continue;
					neighbors.Add(neighbor);
					visitedNeighbors.Add(neighbor);
				}
			}

			return sameColorFields;
		}

		private bool CanVisitNeighbor(int x, int y, Color color, ICollection<Point> visitedNeighbors)
		{
			if (x < 0 || x >= GameBoard.ColumnCount || y < 0 || y >= GameBoard.RowCount) return false;

			var currentField = new Point(x, y);
			if (visitedNeighbors.Contains(currentField)) return false;

			var fieldControl = GameBoard.GetControlFromPosition(x, y);
			return fieldControl.BackColor == color;
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
