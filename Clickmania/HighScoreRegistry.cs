using System.Collections.Generic;
using System.Linq;

namespace Clickmania
{
	public static class HighScoreRegistry
	{
		private static IList<string> HighScores { get; }

		static HighScoreRegistry()
		{
			HighScores = new List<string>();
		}

		/// <summary>
		/// Adds a high score to the registry.
		/// </summary>
		/// <param name="points">Gathered points.</param>
		/// <param name="maxPoints">Max points to gather.</param>
		public static void AddHighScore(int points, int maxPoints)
		{
			HighScores.Add($"{points}/{maxPoints}");
		}

		/// <summary>
		/// Gets all high scores from oldest to newest.
		/// </summary>
		/// <returns>Collection of high scores.</returns>
		public static IEnumerable<string> GetAllRecords()
		{
			return HighScores.Reverse();
		}
	}
}
