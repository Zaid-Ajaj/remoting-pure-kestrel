open System
open Fable.Remoting.Server
open Fable.Remoting.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting

type IServer = { getLength : string -> Async<int> } 

let server = { getLength = fun input -> async { return input.Length } }

let webApp = 
    Remoting.createApi()
    |> Remoting.fromValue server

let configureApp (app : IApplicationBuilder) =
    app.UseRemoting(webApp)

WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .Build()
    .Run()