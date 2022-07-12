using Hw1;
using Xunit;

namespace Hw1Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_WrongInputData_WillReturnNotZero()
        {
            var args = new[] { "1", "+", "&&&" };
            var result = Program.Main(args);
            Assert.True(result != 0);
        }
        
        [Fact]
        public void Main_CorrectInputData_WillReturnZero()
        {
            var args = new[] { "1", "+", "3" };
            var result = Program.Main(args);
            Assert.True(result == 0);
        }
    }
}