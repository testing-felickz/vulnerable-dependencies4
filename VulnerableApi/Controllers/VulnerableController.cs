using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using VulnerableLibrary;

namespace VulnerableApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VulnerableController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly DataProcessor _dataProcessor;

        public VulnerableController(DataProcessor dataProcessor)
        {
            _httpClient = new HttpClient();
            _dataProcessor = dataProcessor;
        }

        [HttpGet("unsafe-json")]
        public ActionResult GetUnsafeJson(string input)
        {
            // Using vulnerable JSON deserialization
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All // CVE-2017-9785 - Known security vulnerability
            };

            var result = JsonConvert.DeserializeObject(input, settings);
            return Ok(result);
        }

        [HttpPost("process-data")]
        public async Task<ActionResult> ProcessData([FromBody] DataRequest request)
        {
            try
            {
                var result = await _dataProcessor.ProcessData(request.Data);
                return Ok(new { Success = true, Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("unsafe-request")]
        public async Task<ActionResult> UnsafeRequest(string url)
        {
            // Using vulnerable System.Net.Http version
            // SSRF vulnerability - allowing the client to specify any URL
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }

    public class DataRequest
    {
        public string Data { get; set; } = string.Empty;
    }
}
