using NUnit.Framework;
using CalcLibrary;

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new SimpleCalculator();
        }

        [TearDown]
        public void Cleanup()
        {
            calculator = null;
        }

        [TestCase(10,20,30)]
        [TestCase(5,5,10)]
        [TestCase(-10,20,10)]
        [TestCase(100,200,300)]
        [TestCase(1.5,2.5,4)]
        public void Addition_ReturnsExpectedResult(
            double a,
            double b,
            double expected)
        {
            double actual =
                calculator.Addition(a,b);

            Assert.That(
                actual,
                Is.EqualTo(expected));
        }

        [Test]
        public void GetResult_ReturnsLastResult()
        {
            calculator.Addition(10,20);

            Assert.That(
                calculator.GetResult,
                Is.EqualTo(30));
        }

        [Ignore("Demonstration of Ignore Attribute")]
        [Test]
        public void IgnoredTest()
        {
        }
    }
}