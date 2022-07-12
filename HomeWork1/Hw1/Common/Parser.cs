using Hw1.Calculator;
using Hw1.Errors;

namespace Hw1.Common
{
    public static class Parser
    {
        public static ErrorCode ParseCalcArguments(string[] args, 
            out int val1, 
            out CalculatorOperation operation, 
            out int val2)
        {
            val1 = val2 = (int) (operation = 0);
            if (!CheckArgLength(args)) return ErrorCode.WrongArgLength;
            if (!int.TryParse(args[0], out val1) || !int.TryParse(args[2], out val2)) return ErrorCode.WrongArgFormatInt;
            return !TryParseOperation(args[1], out operation) ? ErrorCode.WrongArgFormatOperation : ErrorCode.Correct;
        }
        
        private static bool CheckArgLength(string[] array) => array.Length == 3;

        private static bool TryParseOperation(string arg, out CalculatorOperation operation)
        {
            operation = arg switch
            {
                "+" => CalculatorOperation.Plus,
                "-" => CalculatorOperation.Minus,
                "*" => CalculatorOperation.Multiply,
                "/" => CalculatorOperation.Divide,
                _ => 0
            };

            return operation != 0;
        }
    }
}