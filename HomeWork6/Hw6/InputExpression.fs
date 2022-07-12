namespace Hw6.InputExpression

open Hw6

[<CLIMutable>]
type InputExpression = {
    value1: decimal
    operation: CalculatorOperation 
    value2:decimal
}