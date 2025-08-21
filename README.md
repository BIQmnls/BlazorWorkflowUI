# BlazorWorkflowUI

A Blazor WebAssembly client application for building workflows in Elsa 3 JSON format. The UI uses [MudBlazor](https://mudblazor.com/) components (v6.11.2) to design workflows with triggers, optional conditions, steps, and delay activities.

## Features
- Define workflow name, trigger, and condition.
- Add multiple steps, including Delay steps.
- Generate Elsa 3-like workflow JSON.

## Build
=======
This repository includes a `global.json` file that pins the .NET SDK to
version 7.0.x. Ensure the matching SDK is installed before building.

```bash
dotnet restore
dotnet build
```

Run the application with:

```bash
dotnet run
```
