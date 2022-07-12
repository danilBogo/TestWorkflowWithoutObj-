using Hw1.Errors;
using Xunit;

namespace Hw1Tests
{
    public class ErrorMessagesTests
    {
        [Fact]
        public void IsErrorCodeDisplayErrorMessage_WrongInput_WillReturnFalse()
        {
            var args = new[] { "1", "+", "2" };
            var result = ErrorMessages.IsErrorCodeDisplayErrorMessage(ErrorCode.Correct, args);
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(ErrorCode.WrongArgLength, "1", "+", "1", "")]
        [InlineData(ErrorCode.WrongArgFormatInt, "1", "+", "s")]
        [InlineData(ErrorCode.WrongArgFormatOperation, "1", "fd", "1")]
        public void IsErrorCodeDisplayErrorMessage_WrongInput_WillReturnTrue(ErrorCode code, params string[] args)
        {
            var result = ErrorMessages.IsErrorCodeDisplayErrorMessage(code, args);
            Assert.True(result);
        }
    }
}