# Azure DevOps Library Comparator

A specialized .NET tool that helps you compare variable groups across different Azure DevOps environments. This tool makes it easy to spot differences in configuration values between environments (test, acceptance, production).

## Features

- Compare variable groups across multiple environments
- Visual highlighting of differences (green for matching values, yellow for differences, red for missing values)
- Interactive filtering of variable groups
- Support for multiple Azure DevOps organizations
- Expandable/collapsible value viewing

## Prerequisites

- .NET 8.0 or higher
- An Azure DevOps Personal Access Token (PAT) with the following permissions:
  - Project and Team (Read)
  - Variable Groups (Read)

## Installation

### Option 1: Install from NuGet

1. Open a terminal.
2. Run the following command:
   ```
   dotnet tool install --global AzureDevopsPipelineLibraryCompare
   ```

### Option 2: Build from Source

1. Clone the repository.
2. Change directory to AzureDevOpsLibraryComparor:
   ```
   cd .\AzureDevOpsLibraryComparor\
   ```
3. Run:
   ```
   dotnet pack
   dotnet tool install --global --add-source ./nupkg AzureDevOpsLibraryComparor
   ```

## Documentation

For detailed usage instructions, interactive controls, and troubleshooting, please see the [detailed documentation](./AzureDevopsPipelineLibraryCompare/README.md).

## License

This project is licensed under the MIT License - see the LICENSE file for details.

