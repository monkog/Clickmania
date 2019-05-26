namespace Clickmania
{
	public class Game
	{
		/// <summary>
		/// Gets the game board.
		/// </summary>
		public Board Board { get; }

		/// <summary>
		/// Gets the value indicating whether the game is performed in easy version.
		/// </summary>
		public bool IsEasyVersion { get; set; }

		public Game(int width, int height, int colorNumber, bool isEasyVersion)
		{
			IsEasyVersion = isEasyVersion;
			Board = new Board(width, height, colorNumber);
		}
	}
}
