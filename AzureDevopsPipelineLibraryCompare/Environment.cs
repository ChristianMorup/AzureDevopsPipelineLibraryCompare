using System.Text.Json;
using Microsoft.VisualStudio.Services.Common;

namespace AzureDevopsPipelineLibraryCompare;

public class Environment
{
    private const string PatKey = "AzureDevOpsLibComp";
    private static Environment? _instance = null;
    private static Dictionary<string, string> _serviceToPAT = new();
    private static string? _currentService = null;

    private Environment()
    {
        var serializedPatInformation = System.Environment.GetEnvironmentVariable(PatKey, EnvironmentVariableTarget.User);

        if (serializedPatInformation == null) return;

        var serviceToPAT = JsonSerializer.Deserialize<Dictionary<string, string>>(serializedPatInformation);
        if (serviceToPAT != null)
        {
            _serviceToPAT = serviceToPAT;
        }
    }

    public static Environment Instance
    {
        get { return _instance ??= new Environment(); }
    }

    public List<string> GetAllAzureDevopsServices()
    {
        return _serviceToPAT.Keys.ToList();
    }

    public void RequestUserForNewAzureDevopsService()
    {
        var baseUri = GetBaseUri();

        while (string.IsNullOrWhiteSpace(baseUri))
        {
            baseUri = GetBaseUri(useFallbackMessage: true);
        }

        var pat = GetPAT();

        while (string.IsNullOrWhiteSpace(pat))
        {
            pat = GetPAT(useFallbackMessage: true);
        }

        _serviceToPAT[baseUri] = pat;

        var serviceToPAT = JsonSerializer.Serialize(_serviceToPAT);
        System.Environment.SetEnvironmentVariable(PatKey, serviceToPAT, EnvironmentVariableTarget.User);
        _currentService = baseUri;
    }

    public void RefreshAzureDevOpsPAT()
    {
        Console.WriteLine("Your Azure DevOps PAT has expired.");
        var pat = GetPAT();

        while (string.IsNullOrWhiteSpace(pat))
        {
            pat = GetPAT(useFallbackMessage: true);
        }
    }

    public void SetCurrentAzureDevOpsInstance(string service)
    {
        _currentService = service;
    }

    public string? GetAzureDevOpsPAT()
    {
        return string.IsNullOrWhiteSpace(_currentService)
            ? null
            : _serviceToPAT!.GetValueOrDefault(_currentService);
    }

    public string? GetAzureDevOpsBaseUrl()
    {
        return _currentService;
    }


    private string? GetPAT(bool useFallbackMessage = false)
    {
        if (useFallbackMessage)
            Console.WriteLine("Your PAT is invalid.");

        Console.WriteLine("Please insert your PAT here:");
        var pat = Console.ReadLine();

        return pat;
    }

    private string? GetBaseUri(bool useFallbackMessage = false)
    {
        if (useFallbackMessage)
            Console.WriteLine("Your base URI is invalid.");

        Console.WriteLine("Please insert your base URI here (e.g. https://dev.azure.com/{YourOrganization}/:");
        var baseUri = Console.ReadLine();

        return baseUri;
    }
}