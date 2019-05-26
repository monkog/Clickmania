using Clickmania;
using NUnit.Framework;

namespace ClickmaniaTests
{
	[TestFixture]
	public class BoardTests
	{
		[Test]
		public void Ctor_ValidParams_PropertiesAssigned()
		{
			const int width = 23;
			const int height = 5;
			const int colorNumber = 44;

			var unitUnderTest = new Board(width, height, colorNumber);

			Assert.AreEqual(width, unitUnderTest.Width);
			Assert.AreEqual(height, unitUnderTest.Height);
			Assert.AreEqual(colorNumber, unitUnderTest.ColorNumber);
		}
	}
}
