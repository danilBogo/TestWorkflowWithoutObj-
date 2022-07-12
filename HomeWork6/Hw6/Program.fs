module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open MaybeBuilder
open Hw6.InputExpression

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result =
            maybe {
                let! expression = ctx.TryBindQueryString<InputExpression>()
                let values = [|expression.value1.ToString();expression.operation.ToString();expression.value2.ToString()|]
                let result = Parser.parseCalcArguments values
                match result with
                | Ok resultOk -> return (Calculator.calculate resultOk.Item1 resultOk.Item2 resultOk.Item3).ToString()
                | Error resultError -> return resultError.ToString()
            }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx
let webApp =
    choose [ GET
             >=> choose [ route "/"
                          >=> text
                                  "Если хочешь что-нибудь посчитать введи http://localhost:5000/calculate?value1= &operation= &value2= "
                          route "/calculate" >=> calculatorHandler ]
             setStatusCode 404 >=> text "Not Found" ]
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder)
                        (_ : IHostEnvironment)
                        (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .UseStartup<Startup>()
                    |> ignore)
        .Build()
        .Run()
    0