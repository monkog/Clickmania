using Clickmania;
using NUnit.Framework;

namespace ClickmaniaTests
{
	[TestFixture()]
	public class GameTests
	{
		[Test]
		public void Ctor_ValidParams_PropertiesAssigned()
		{
			const int width = 20;
			const int height = 22;
			const int colorNumber = 4;
			const bool isEasyVersion = false;

			var unitUnderTest = new Game(width, height, colorNumber, isEasyVersion);

			Assert.AreEqual(isEasyVersion, unitUnderTest.IsEasyVersion);
			Assert.IsNotNull(unitUnderTest.Board);
			Assert.AreEqual(width, unitUnderTest.Board.Width);
			Assert.AreEqual(height, unitUnderTest.Board.Height);
			Assert.AreEqual(colorNumber, unitUnderTest.Board.ColorNumber);
		}
	}
}
