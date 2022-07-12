module Hw6.Parser

open MaybeBuilder

let inline isOperationSupported (arg1, operation, arg2) =
    match operation with
        | "Plus" -> Ok (arg1, CalculatorOperation.Plus, arg2)
        | "Minus" -> Ok (arg1, CalculatorOperation.Minus, arg2)
        | "Multiply" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
        | "Divide" -> Ok (arg1, CalculatorOperation.Divide, arg2)

let parseArgs (args: string[]) =
    Ok (args[0].Replace(",", ".") |> decimal, args[1], args[2].Replace(",", ".") |> decimal)

let inline isDividingByZero (arg1, operation, arg2) =
    match operation with
    | CalculatorOperation.Divide when arg2.ToString() = "0" -> Error Message.DivideByZero
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]) =
    maybe {
        let! tryParseArgs = parseArgs args
        let! checkOperation = isOperationSupported tryParseArgs
        let! checkDividingByZero = isDividingByZero checkOperation
        return checkDividingByZero
    }