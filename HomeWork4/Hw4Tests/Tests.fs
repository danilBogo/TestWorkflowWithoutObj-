module Hw4Tests.Tests

open System
open Hw4
open Xunit
        
[<Theory>]
[<InlineData(15, 5, CalculatorOperation.Plus, 20)>]
[<InlineData(15, 5, CalculatorOperation.Minus, 10)>]
[<InlineData(15, 5, CalculatorOperation.Multiply, 75)>]
[<InlineData(15, 5, CalculatorOperation.Divide, 3)>]
let ``TestAllOperations`` (value1, value2, operation, expectedValue) =
    Assert.Equal(expectedValue, Calculator.calculate value1 operation value2)
    
[<Fact>]
let ``TestDividingNonZeroByZero`` () =
    Assert.Equal(0 |> double, Calculator.calculate 0 CalculatorOperation.Divide 10)
    
[<Fact>]
let ``TestDividingZeroByNonZero`` () =
    Assert.Equal(Double.PositiveInfinity, Calculator.calculate 10 CalculatorOperation.Divide 0)
    
[<Fact>]
let ``TestDividingZeroByZero`` () =
    Assert.Equal(Double.NaN, Calculator.calculate 0 CalculatorOperation.Divide 0)
    

