using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

// Create logger using vulnerable Microsoft.Extensions.Logging.Console 3.1.0
using var loggerFactory = LoggerFactory.Create(builder =>
    builder.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Starting Vulnerable Dependencies Console Sample");

// Example 1: Demonstrate vulnerable Microsoft.Data.SqlClient usage
await DemonstrateVulnerableSqlClient(logger);

// Example 2: Demonstrate vulnerable transitive dependencies through logging
DemonstrateVulnerableLogging(logger);

logger.LogInformation("Sample completed");

static async Task DemonstrateVulnerableSqlClient(ILogger logger)
{
    try
    {
        // This uses Microsoft.Data.SqlClient 1.0.19239.1 which has known vulnerabilities
        var connectionString = "Server=localhost;Database=TestDb;Integrated Security=true;TrustServerCertificate=true;";
        
        // Note: This will fail to connect since there's no SQL Server, but demonstrates the usage
        using var connection = new SqlConnection(connectionString);
        logger.LogInformation("Attempting to connect using vulnerable SqlClient...");
        
        // In a real scenario, this would attempt to connect
        logger.LogWarning("SqlClient connection attempt (will fail - no server available)");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Expected error - demonstrating vulnerable SqlClient usage");
    }
}

static void DemonstrateVulnerableLogging(ILogger logger)
{
    // This logging infrastructure uses vulnerable transitive dependencies
    logger.LogDebug("Debug message using vulnerable logging infrastructure");
    logger.LogInformation("Information message demonstrating vulnerable transitive dependencies");
    logger.LogWarning("Warning about using outdated logging packages");
    logger.LogError("Error message showing security risks in dependency chain");
}
