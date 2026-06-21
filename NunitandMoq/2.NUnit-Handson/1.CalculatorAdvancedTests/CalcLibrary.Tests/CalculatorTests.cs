using NUnit.Framework;
using CalcLibrary;
using System;

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

        [TestCase(20, 10, 10)]
        [TestCase(50, 20, 30)]
        [TestCase(-10, -5, -5)]
        [TestCase(100, 25, 75)]
        public void Subtraction_ReturnsExpectedResult(
            double a,
            double b,
            double expected)
        {
            double actual =
                calculator.Subtraction(a, b);

            Assert.AreEqual(
                expected,
                actual);
        }

        [TestCase(10, 5, 50)]
        [TestCase(20, 2, 40)]
        [TestCase(-10, 5, -50)]
        [TestCase(1.5, 2, 3)]
        public void Multiplication_ReturnsExpectedResult(
            double a,
            double b,
            double expected)
        {
            double actual =
                calculator.Multiplication(a, b);

            Assert.AreEqual(
                expected,
                actual);
        }

        [TestCase(20, 5, 4)]
        [TestCase(100, 10, 10)]
        [TestCase(9, 3, 3)]
        [TestCase(7.5, 2.5, 3)]
        public void Division_ReturnsExpectedResult(
            double a,
            double b,
            double expected)
        {
            double actual =
                calculator.Division(a, b);

            Assert.AreEqual(
                expected,
                actual);
        }

        [Test]
        public void Division_ByZero_ShouldThrowException()
        {
            try
            {
                calculator.Division(10, 0);

                Assert.Fail("Division by zero");
            }
            catch (ArgumentException ex)
            {
                Assert.That(
                    ex.Message,
                    Is.EqualTo(
                        "Second Parameter Can't be Zero"));

                Assert.That(
                    ex,
                    Is.TypeOf<ArgumentException>());
            }
        }

        [Test]
        public void TestAddAndClear()
        {
            double result =
                calculator.Addition(20, 30);

            Assert.AreEqual(
                50,
                result);

            calculator.AllClear();

            Assert.AreEqual(
                0,
                calculator.GetResult);
        }
    }
}