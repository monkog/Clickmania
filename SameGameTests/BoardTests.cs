using NUnit.Framework;
using SameGame;

namespace SameGameTests
{
	[TestFixture]
	public class BoardTests
	{
		private Board _unitUnderTest;

		[SetUp]
		public void Initialize()
		{
			_unitUnderTest = new Board(1, 2, 3);
		}

		[Test]
		public void Ctor_ValidParams_PropertiesAssigned()
		{
			const int width = 23;
			const int height = 5;
			const int colorNumber = 44;

			var unitUnderTest = new Board(width, height, colorNumber);

			Assert.AreEqual(width, unitUnderTest.Columns);
			Assert.AreEqual(height, unitUnderTest.Rows);
			Assert.AreEqual(colorNumber, unitUnderTest.ColorNumber);
		}

		[Test]
		public void AreAllFieldsRemoved_InitialObject_False()
		{
			Assert.IsFalse(_unitUnderTest.AllFieldsRemoved);
		}

		[Test]
		public void RemoveField_AllFields_AllFieldsRemoved()
		{
			_unitUnderTest.RemoveFields(2);

			Assert.IsTrue(_unitUnderTest.AllFieldsRemoved);
		}
	}
}
