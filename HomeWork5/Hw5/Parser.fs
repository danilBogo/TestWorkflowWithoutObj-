module Hw5.Parser

open MaybeBuilder

let isArgLengthSupported (args:string[]) =
    match args.Length with
    | 3 -> Ok args
    | _ -> Error Message.WrongArgLength
    
let inline isOperationSupported (arg1, operation, arg2) =
    match operation with
        | "+" -> Ok (arg1, CalculatorOperation.Plus, arg2)
        | "-" -> Ok (arg1, CalculatorOperation.Minus, arg2)
        | "*" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
        | "/" -> Ok (arg1, CalculatorOperation.Divide, arg2)
        | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]) =
    try
        Ok (args[0] |> decimal, args[1], args[2] |> decimal)
    with | _ -> Error Message.WrongArgFormat

let inline isDividingByZero (arg1, operation, arg2) =
    match operation with
    | CalculatorOperation.Divide when arg2.ToString() = "0" -> Error Message.DivideByZero
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]) =
    maybe {
        let! checkArgLength = isArgLengthSupported args
        let! tryParseArgs = parseArgs checkArgLength
        let! checkOperation = isOperationSupported tryParseArgs
        let! checkDividingByZero = isDividingByZero checkOperation
        return checkDividingByZero
    }