using System.Net.Http.Formatting; // From Microsoft.AspNet.WebApi.Client
using Microsoft.Data.OData;
using Microsoft.Extensions.Logging;
using ICSharpCode.SharpZipLib.Zip;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace VulnerableLibrary;

public class DataProcessor
{
    private readonly ILogger<DataProcessor> _logger;
    private readonly HttpClient _httpClient;
    
    public DataProcessor(ILogger<DataProcessor> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }
    
    public async Task<string> ProcessData(string input)
    {
        // Using vulnerable regex pattern (can cause ReDoS)
        var regex = new Regex(@"^(([a-z])+.)+[A-Z]([a-z])+$");
        var isMatch = regex.IsMatch(input);
        
        _logger.LogInformation("Regex match result: {IsMatch}", isMatch);
        
        // Using vulnerable HTTP client from old version
        var response = await _httpClient.GetAsync("https://api.example.com/data");
        response.EnsureSuccessStatusCode();
        
        // Using vulnerable OData processing
        var odataContext = new Microsoft.Data.OData.ODataMessageReaderSettings();
        
        // Using vulnerable zip library
        using var zipFile = new ZipFile(input);
        
        return "Processed";
    }
}
