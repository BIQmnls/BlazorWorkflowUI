# BlazorWorkflowUI

A Blazor WebAssembly client application for building workflows in Elsa 3 JSON format. The UI uses [MudBlazor](https://mudblazor.com/) components (v6.11.2) to design workflows with triggers, optional conditions, steps, delays, and branching.

## Features
- Define workflow name, trigger, and condition.
- Add multiple steps with optional delays.
- Specify branching by listing next step IDs.
- Generate Elsa 3-like workflow JSON.

## Build
Requires .NET 7 SDK.

```bash
dotnet restore
dotnet build
```

Run the application with:

```bash
dotnet run
```
