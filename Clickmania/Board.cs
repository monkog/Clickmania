using System;
using System.Drawing;

namespace Clickmania
{
	public class Board
	{
		private readonly Color[] _colorTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };

		private readonly Random _random = new Random();

		private int _fieldsToRemove;

		/// <summary>
		/// Gets the number of columns.
		/// </summary>
		public int Columns { get; }

		/// <summary>
		/// Gets the number of rows.
		/// </summary>
		public int Rows { get; }

		/// <summary>
		/// Gets the number of different colors used in the game.
		/// </summary>
		public int ColorNumber { get; }

		/// <summary>
		/// Gets a value determining whether all fields were removed.
		/// </summary>
		public bool AllFieldsRemoved => _fieldsToRemove == 0;

		public Board(int columns, int rows, int colorNumber)
		{
			Columns = columns;
			Rows = rows;
			ColorNumber = colorNumber;

			_fieldsToRemove = Columns * Rows;
		}

		/// <summary>
		/// Gets the next generated color.
		/// </summary>
		/// <returns>Generated color.</returns>
		public Color GetColor()
		{
			return _colorTab[_random.Next() % ColorNumber];
		}

		/// <summary>
		/// Removes the field.
		/// </summary>
		public void RemoveField()
		{
			_fieldsToRemove--;
		}
	}
}
