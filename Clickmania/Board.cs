using System;
using System.Drawing;

namespace Clickmania
{
	public class Board
	{
		private readonly Color[] _colorTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };

		private readonly Random _random = new Random();

		/// <summary>
		/// Gets the number of columns.
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// Gets the number of rows.
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// Gets the number of different colors used in the game.
		/// </summary>
		public int ColorNumber { get; }

		public Board(int width, int height, int colorNumber)
		{
			Width = width;
			Height = height;
			ColorNumber = colorNumber;
		}

		/// <summary>
		/// Gets the next generated color.
		/// </summary>
		/// <returns>Generated color.</returns>
		public Color GetColor()
		{
			return _colorTab[_random.Next() % ColorNumber];
		}
	}
}
