using NLog;
using System.IdentityModel.Tokens.Jwt;

namespace VulnerableLibrary;

/// <summary>
/// Sample library demonstrating usage of vulnerable dependencies
/// </summary>
public class VulnerableLibraryService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Demonstrates usage of vulnerable NLog 4.4.0
    /// </summary>
    public void LogMessage(string message)
    {
        // This uses vulnerable NLog 4.4.0 which has known security issues
        Logger.Info($"Processing message: {message}");
    }

    /// <summary>
    /// Demonstrates usage of vulnerable System.IdentityModel.Tokens.Jwt 5.1.0
    /// </summary>
    public string ProcessJwtToken(string token)
    {
        try
        {
            // This uses vulnerable JWT library 5.1.0 with known security vulnerabilities
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            
            Logger.Info($"JWT processed successfully. Subject: {jsonToken.Subject}");
            return jsonToken.Subject ?? "Unknown";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to process JWT token");
            throw;
        }
    }
}
