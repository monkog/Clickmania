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

		/// <summary>
		/// Gets the current score.
		/// </summary>
		public int Score { get; private set; }

		public Game(int width, int height, int colorNumber, bool isEasyVersion)
		{
			IsEasyVersion = isEasyVersion;
			Score = 0;
			Board = new Board(width, height, colorNumber);
		}

		/// <summary>
		/// Adds points to the current score.
		/// </summary>
		/// <param name="points">Points to add.</param>
		public void AddPoints(int points)
		{
			Score += points;
			Board.RemoveFields(points);
		}
	}
}
