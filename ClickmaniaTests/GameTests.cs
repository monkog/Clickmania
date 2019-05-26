using Clickmania;
using NUnit.Framework;

namespace ClickmaniaTests
{
	[TestFixture()]
	public class GameTests
	{
		private Game _unitUnderTest;

		[SetUp]
		public void Initialize()
		{
			_unitUnderTest = new Game(1, 2, 3, true);
		}

		[Test]
		public void Ctor_ValidParams_PropertiesAssigned()
		{
			const int width = 20;
			const int height = 22;
			const int colorNumber = 4;
			const bool isEasyVersion = false;

			var unitUnderTest = new Game(width, height, colorNumber, isEasyVersion);

			Assert.AreEqual(isEasyVersion, unitUnderTest.IsEasyVersion);
			Assert.AreEqual(0, unitUnderTest.Score);
		}

		[Test]
		public void Ctor_ValidParams_BoardCreated()
		{
			const int width = 12;
			const int height = 13;
			const int colorNumber = 55;
			const bool isEasyVersion = false;

			var unitUnderTest = new Game(width, height, colorNumber, isEasyVersion);

			Assert.IsNotNull(unitUnderTest.Board);
			Assert.AreEqual(width, unitUnderTest.Board.Width);
			Assert.AreEqual(height, unitUnderTest.Board.Height);
			Assert.AreEqual(colorNumber, unitUnderTest.Board.ColorNumber);
		}

		[Test]
		public void AddPoints_PointNumber_PointsAssigned()
		{
			const int points = 14;

			_unitUnderTest.AddPoints(points);

			Assert.AreEqual(points, _unitUnderTest.Score);
		}
	}
}
