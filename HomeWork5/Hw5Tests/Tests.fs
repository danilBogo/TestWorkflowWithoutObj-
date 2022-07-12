module Hw5Tests.Tests

open Hw5
open Microsoft.FSharp.Core
open Xunit

let epsilon: decimal = 0.001m
        
[<Theory>]
[<InlineData(15, 5, CalculatorOperation.Plus, 20)>]
[<InlineData(15, 5, CalculatorOperation.Minus, 10)>]
[<InlineData(15, 5, CalculatorOperation.Multiply, 75)>]
[<InlineData(15, 5, CalculatorOperation.Divide, 3)>]
let ``TestCalculatorAllOperationsInt`` (value1 : int, value2: int, operation, expectedValue : int) =
    Assert.Equal(expectedValue, Calculator.calculate value1 operation value2)

[<Theory>]
[<InlineData(15.6, 5.6, CalculatorOperation.Plus, 21.2)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Minus, 10)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Multiply, 87.36)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Divide, 2.7857)>]
let ``TestCalculatorAllOperationsFloat`` (value1 : float, value2: float, operation, expectedValue : float) =
    Assert.True((abs (expectedValue - Calculator.calculate value1 operation value2)) |> decimal < epsilon)
    
[<Theory>]
[<InlineData(15.6, 5.6, CalculatorOperation.Plus, 21.2)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Minus, 10)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Multiply, 87.36)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Divide, 2.7857)>]
let ``TestCalculatorAllOperationsDouble`` (value1 : double, value2: double, operation, expectedValue : double) =
    Assert.True((abs (expectedValue - Calculator.calculate value1 operation value2)) |> decimal < epsilon)
    
[<Theory>]
[<InlineData(15.6, 5.6, CalculatorOperation.Plus, 21.2)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Minus, 10)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Multiply, 87.36)>]
[<InlineData(15.6, 5.6, CalculatorOperation.Divide, 2.7857)>]
let ``TestCalculatorAllOperationsDecimal`` (value1 : decimal, value2: decimal, operation, expectedValue : decimal) =
    Assert.True((abs (expectedValue - Calculator.calculate value1 operation value2)) |> decimal < epsilon)
    
[<Theory>]
[<InlineData("15", "+", "5", 20)>]
[<InlineData("15", "-", "5", 10)>]
[<InlineData("15", "*", "5", 75)>]
[<InlineData("15", "/", "5",  3)>]
[<InlineData("15.6", "+", "5.6", 21.2)>]
[<InlineData("15.6", "-", "5.6", 10)>]
[<InlineData("15.6", "*", "5.6", 87.36)>]
[<InlineData("15.6", "/", "5.6", 2.7857)>]
let ``TestParserCorrectValues`` (value1, operation, value2, expectedValue) =
    let values = [|value1;operation;value2|]
    let result = Parser.parseCalcArguments values
    match result with
    | Ok resultOk ->
        Assert.True((abs (expectedValue - Calculator.calculate resultOk.Item1 resultOk.Item2 resultOk.Item3)) |> decimal < epsilon)
        
[<Theory>]
[<InlineData("f", "+", "3")>]
[<InlineData("3", "+", "f")>]
[<InlineData("a", "+", "f")>]
let ``TestParserWrongValues`` (value1, operation, value2) =
    let args = [|value1;operation;value2|]
    let result = Parser.parseCalcArguments args
    match result with
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgFormat)
    
[<Fact>]
let ``TestParserWrongOperation`` () =
    let args = [|"3";".";"4"|]
    let result = Parser.parseCalcArguments args
    match result with
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgFormatOperation)
    
[<Fact>]
let ``TestParserWrongLength`` () =
    let args = [|"3";"+";"4";"5"|]
    let result = Parser.parseCalcArguments args
    match result with
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgLength)
    
[<Fact>]
let ``TestParserDividingByZero`` () =
    let args = [|"3";"/";"0"|]
    let result = Parser.parseCalcArguments args
    match result with
    | Error resultError -> Assert.Equal(resultError, Message.DivideByZero)

