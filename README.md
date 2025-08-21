# BlazorWorkflowUI

A Blazor WebAssembly client application for building workflows in Elsa 3 JSON format. The UI uses [MudBlazor](https://mudblazor.com/) components (v6.11.2) to design workflows with triggers, steps with optional conditions, and delay activities tailored for insurance and mortgage management.

## Features
- Define workflow name and select triggers within the workflow diagram using domain-specific options such as **PolicyCreated**, **PremiumDue**, and **MortgageApplicationSubmitted**.
- Add multiple steps like **SendPolicyDocument**, **EvaluateMortgageApplication**, and **WaitForDocuments**, each with optional conditions.
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
