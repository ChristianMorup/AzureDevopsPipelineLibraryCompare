# Azure DevOps Library Comparator

A specialized tool that helps you compare variable groups across different Azure DevOps environments.

## Getting Started

### Running the tool
1. Run the following command: `azlib`
2. The tool will ask for a Personal Access Token (PAT). Obtain a PAT from Azure DevOps ([See guide](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops&tabs=Windows))
   Note: It is important that the PAT has read access to the following scopes: "Project and Team" and "Variable Groups". 
3. Afterwards the tool will ask for the base url (e.g. https://dev.azure.com/{YourOrganization}/)
4. Follow the instructions and compare variables across environments.

## Usage Guide

### Basic Navigation
1. Start the tool by running `azplc`
2. Select your Azure DevOps organization or add a new one
3. Choose the project you want to analyze
4. Filter and select the variable groups you want to compare

### Reading the Results
Values are color-coded for easy comparison:
- 🟢 Green: Values match across all selected groups
- 🟡 Yellow: Values differ between groups
- 🟥 Red: Variable is missing in one or more groups

### Interactive Controls
- Press `a` to expand/collapse all columns
- Press `0-9` to expand/collapse individual columns
- Press `Esc` to exit
- Use typing to filter variable groups during selection
- Use spacebar to select multiple groups
- Press Enter to confirm selection

### Managing Organizations
- The tool remembers your Azure DevOps organizations
- You can add multiple organizations
- Each organization requires its own PAT
- Select "Add a new service..." to configure a new organization

### PAT Management
If your PAT expires:
1. The tool will prompt you for a new one
2. Generate a new PAT in Azure DevOps
3. Enter the new PAT when prompted

Required PAT Permissions:
- Project and Team (Read)
- Variable Groups (Read)

## Tips
- Use the filter feature to quickly find related variable groups
- Expand columns to see full values when they differ
- Compare similar variable groups across environments to ensure consistency
- Regular PAT rotation is recommended for security
