using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VulnerableLibrary;

// Configure services
var services = new ServiceCollection();
services.AddLogging(builder => 
{
    builder.AddConsole();
});

services.AddTransient<DataProcessor>();

var serviceProvider = services.BuildServiceProvider();

// Using vulnerable log4net version
var log = LogManager.GetLogger(typeof(Program));
log.Info("Application started");

// Using vulnerable JSON library
var jsonSettings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All // Vulnerable setting
};

var data = JsonConvert.SerializeObject(new { Message = "Test" }, jsonSettings);
Console.WriteLine($"Serialized data: {data}");

// Using vulnerable library
var processor = serviceProvider.GetRequiredService<DataProcessor>();
try 
{
    await processor.ProcessData("test-data");
}
catch (Exception ex) 
{
    Console.WriteLine($"Error processing data: {ex.Message}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
