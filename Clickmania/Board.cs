using System;
using System.Drawing;

namespace Clickmania
{
	public class Board
	{
		private readonly Color[] _colorTab = { Color.Blue, Color.Green, Color.Crimson, Color.Cyan, Color.Gray, Color.Indigo, Color.Navy, Color.HotPink, Color.Red, Color.Yellow };

		private readonly Random _random = new Random();

		public int Width { get; }

		public int Height { get; }

		public int ColorNumber { get; }

		public Board(int width, int height, int colorNumber)
		{
			Width = width;
			Height = height;
			ColorNumber = colorNumber;
		}

		public Color GetColor()
		{
			return _colorTab[_random.Next() % ColorNumber];
		}
	}
}
