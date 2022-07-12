using Hw1.Calculator;
using Hw1.Common;
using Hw1.Errors;
using Xunit;

namespace Hw1Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("+", CalculatorOperation.Plus)]
        [InlineData("-", CalculatorOperation.Minus)]
        [InlineData("*", CalculatorOperation.Multiply)]
        [InlineData("/", CalculatorOperation.Divide)]
        public void ParseCalcArguments_Operation_WillParse(string operation, CalculatorOperation operationExpected)
        {
            var args = new[] { "4", operation, "777" };
            var check = Parser.ParseCalcArguments(args, out var val1, out var operationResult, out var val2);
            Assert.Equal(ErrorCode.Correct, check);
            Assert.Equal(4, val1);
            Assert.Equal(operationExpected, operationResult);
            Assert.Equal(777, val2);
        }

        [Fact]
        public void ParseCalcArguments_NotNumber_WillReturnWrongArgFormatInt()
        {
            var args = new[] { "4", "+", "turn around" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(ErrorCode.WrongArgFormatInt, check);
        }
        
        [Fact]
        public void ParseCalcArguments_NotOperation_WillReturnWrongArgFormatOperation()
        {
            var args = new[] { "4", "turn around", "1337" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(ErrorCode.WrongArgFormatOperation, check);
        }

        [Fact]
        public void ParseCalcArguments_WrongLengthArgs_WillReturnWrongArgLength()
        {
            var args = new[] { "4", "+", "1337", "Timur" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(ErrorCode.WrongArgLength, check);
        }
    }
}