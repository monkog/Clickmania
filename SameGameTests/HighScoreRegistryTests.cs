using System.Linq;
using NUnit.Framework;
using SameGame;

namespace SameGameTests
{
	[TestFixture]
	public class HighScoreRegistryTests
	{
		[Test]
		public void AddHighScore_PointsAndMaxPoints_HighScoreAdded()
		{
			const int points = 34;
			const int maxPoints = 44;

			HighScoreRegistry.AddHighScore(points, maxPoints);

			var records = HighScoreRegistry.GetAllRecords();

			CollectionAssert.Contains(records, $"{points}/{maxPoints}");
		}

		[Test]
		public void AddHighScore_SameAsExisting_HighScoreAdded()
		{
			const int points = 22;
			const int maxPoints = 65;

			HighScoreRegistry.AddHighScore(points, maxPoints);
			HighScoreRegistry.AddHighScore(points, maxPoints);

			var highScore = $"{points}/{maxPoints}";
			var highScoreCount = HighScoreRegistry.GetAllRecords().Count(s => s == highScore);

			Assert.AreEqual(2, highScoreCount);
		}

		[Test]
		public void GetAllRecords_MultipleRecords_FromNewestToOldest()
		{
			const int points1 = 1;
			const int points2 = 2;
			const int maxPoints = 12;

			HighScoreRegistry.AddHighScore(points1, maxPoints);
			HighScoreRegistry.AddHighScore(points2, maxPoints);

			var highScore1 = $"{points1}/{maxPoints}";
			var highScore2 = $"{points2}/{maxPoints}";
			var highScores = HighScoreRegistry.GetAllRecords().ToList();

			var highScore1Index = highScores.IndexOf(highScore1);
			var highScore2Index = highScores.IndexOf(highScore2);

			Assert.AreEqual(0, highScore2Index);
			Assert.AreEqual(1, highScore1Index);
		}
	}
}
