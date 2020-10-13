using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace scb_api.ApiClients
{
  public class ApiClientBase<T> where T : class
  {
    private const string MediaTypeHeaderValue = "application/json";
    private const string BaseAddressSetTo = "Base address set to";

    private static HttpClient _client = new HttpClient();

    protected readonly IConfiguration configuration;
    protected readonly ILogger<T> logger;

    public ApiClientBase(IConfiguration configuration, ILogger<T> logger, Uri baseAddress)
    {
      this.configuration = configuration;
      this.logger = logger;
      this.logger.LogInformation($"{BaseAddressSetTo}: {baseAddress}");
      _client.BaseAddress = baseAddress;
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeHeaderValue));
    }

    public async Task<HttpResponseMessage> GetAsync(string apiEndpoint, IDictionary<string, string> queryParams)
    {
      this.logger.LogInformation($"Get call {_client.BaseAddress}/{apiEndpoint}?{queryParams.ToString()}");
      return await _client.GetAsync(GetRequestQueryString(apiEndpoint, queryParams));
    }

    public async Task<HttpResponseMessage> PostAsync<O>(string apiEndpoint, O content)
    {
      this.logger.LogInformation($"Post call {_client.BaseAddress}/{apiEndpoint}\n\nContent:\n{JsonConvert.SerializeObject(content)}");
      return await _client.PostAsync(apiEndpoint, content, new JsonMediaTypeFormatter());
    }

    private static string GetRequestQueryString(string apiEndpoint, IDictionary<string, string> queryParams)
    {
      return QueryHelpers.AddQueryString(apiEndpoint, queryParams);
    }
  }
}