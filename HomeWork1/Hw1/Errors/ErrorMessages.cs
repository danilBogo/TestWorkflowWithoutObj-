namespace Hw1.Errors
{
    public static class ErrorMessages
    {
        public static bool IsErrorCodeDisplayErrorMessage(ErrorCode code, string[] args)
        {
            if (code == ErrorCode.Correct) return false;
            var message = code switch
            {
                ErrorCode.WrongArgLength => $"The program requires 3 arguments, but was {args.Length}",
                ErrorCode.WrongArgFormatOperation => $"Value is not operation: {args[1]}",
                ErrorCode.WrongArgFormatInt => $"Value(s) is/are not int: {args[0]}, {args[2]}",
            };

            Console.WriteLine(message);
            return true;
        }
    }
}