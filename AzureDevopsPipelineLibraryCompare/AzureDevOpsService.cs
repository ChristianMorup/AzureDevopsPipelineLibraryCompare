﻿using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace AzureDevopsPipelineLibraryCompare;

public class AzureDevOpsService
{
    private Dictionary<string, Dictionary<string, string>>? _libraries;

    public async Task<List<string>> GetProjects(bool patIsRenewed = false)
    {
        try
        {
            var credentials = new VssCredentials(new VssBasicCredential("", Environment.Instance.GetAzureDevOpsPAT()));
            var vssConnection = new VssConnection(new Uri(Environment.Instance.GetAzureDevOpsBaseUrl() ?? ""), credentials);
            var client = vssConnection.GetClient<ProjectHttpClient>();
            var projectReferences = await client.GetProjects();
            var projects = projectReferences.Select(p => p.Name).ToList();
            return projects;
        }
        catch (Exception e)
        {
            if (patIsRenewed)
            {
                Console.WriteLine(e);
                throw;
            }

            Environment.Instance.RefreshAzureDevOpsPAT();
            return await GetProjects(true);
        }
    }

    public async Task<Dictionary<string, Dictionary<string, string>>> GetLibraries(string project, bool forceUpdate = false)
    {
        if (_libraries != null && forceUpdate is false)
        {
            return _libraries;
        }

        var variableGroups = await GetVariableGroups(project);

        Dictionary<string, Dictionary<string, string>> dictionary = new();

        foreach (var group in variableGroups.OrderBy(v => v.Name))
        {
            var name = group.Name.ToLowerInvariant();
            var variables = group.Variables.ToDictionary(g => g.Key, g => g.Value.Value);
            dictionary[name] = variables;
        }

        _libraries = dictionary;
        return _libraries;
    }


    private async Task<List<VariableGroup>> GetVariableGroups(string project, bool patIsRenewed = false)
    {
        try
        {
            var credentials = new VssCredentials(new VssBasicCredential("", Environment.Instance.GetAzureDevOpsPAT()));
            var vssConnection = new VssConnection(new Uri(Environment.Instance.GetAzureDevOpsBaseUrl() ?? ""), credentials);
            var taskAgentClient = vssConnection.GetClient<TaskAgentHttpClient>();
            var variableGroups = await taskAgentClient.GetVariableGroupsAsync(project: project);
            return variableGroups;
        }
        catch (Exception e)
        {
            if (patIsRenewed)
            {
                Console.WriteLine(e);
                throw;
            }


            Environment.Instance.RefreshAzureDevOpsPAT();
            return await GetVariableGroups(project, true);
        }
    }
}