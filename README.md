# AspNetCore with Fable.Remoting: Pure Kestrel sample 
The simplest it can get

### Project file
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="Program.fs" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Fable.Remoting.AspNetCore" Version="1.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Hosting" Version="2.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Server.Kestrel" Version="2.1.2" />
  </ItemGroup>

</Project>
```
and `Program.fs`
```fs
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
    // Add the API to the ASP.NET Core pipeline
    app.UseRemoting(webApp)


WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .Build()
    .Run()
```