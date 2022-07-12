using IlClasses;
using Xunit;

namespace Hw2Tests
{
    public class Tests
    {
        [Theory]
        [InlineData(15, 5, CalculatorOperation.Plus, 20)]
        [InlineData(15, 5, CalculatorOperation.Minus, 10)]
        [InlineData(15, 5, CalculatorOperation.Multiply, 75)]
        [InlineData(15, 5, CalculatorOperation.Divide, 3)]
        public void TestAllOperations(int value1, int value2, CalculatorOperation operation, int expectedValue)
        {
            Assert.Equal(expectedValue, Calculator.Calculate(value1, operation, value2));
        }

        [Fact]
        public void TestDividingNonZeroByZero()
        {
            Assert.Equal(0, Calculator.Calculate(0, CalculatorOperation.Divide, 10));
        }

        [Fact]
        public void TestDividingZeroByNonZero()
        {
            Assert.Equal(double.PositiveInfinity, Calculator.Calculate(10, CalculatorOperation.Divide, 0));
        }
        
        [Fact]
        public void TestDividingZeroByZero()
        {
            Assert.Equal(double.NaN, Calculator.Calculate(0, CalculatorOperation.Divide, 0));
        }
    }
}