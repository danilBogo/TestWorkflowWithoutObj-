using Hw1.Common;
using Hw1.Errors;

namespace Hw1
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var check = Parser.ParseCalcArguments(args, out var val1, out var operation, out var val2);
            var isError = ErrorMessages.IsErrorCodeDisplayErrorMessage(check, args);
            if (isError) return (int) check;
            
            var result = Calculator.Calculator.Calculate(val1, operation, val2);
            Console.WriteLine($"Result is: {result}");
            return (int) ErrorCode.Correct;
        }
    }
}