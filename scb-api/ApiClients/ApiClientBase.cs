using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace scb_api.ApiClients
{
  public class ApiClientBase<T> where T : class
  {
    private const string MediaTypeHeaderValue = "application/json";
    private const string BaseAddressSetTo = "Base address set to";

    private static HttpClient _client = new HttpClient();

    protected readonly IConfiguration configuration;

    public ApiClientBase(IConfiguration configuration, Uri baseAddress)
    {
      this.configuration = configuration;
      _client.BaseAddress = baseAddress;
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeHeaderValue));
    }

    protected async Task<HttpResponseMessage> GetAsync(string apiEndpoint, IDictionary<string, string> queryParams = null)
    {
      if (queryParams != null)
      {
        return await _client.GetAsync(GetRequestQueryString(apiEndpoint, queryParams));
      }

      return await _client.GetAsync(apiEndpoint);
    }

    protected async Task<HttpResponseMessage> PostAsync<T1>(string apiEndpoint, T1 content)
    {
      return await _client.PostAsync(apiEndpoint, content, new JsonMediaTypeFormatter());
    }

    private static string GetRequestQueryString(string apiEndpoint, IDictionary<string, string> queryParams)
    {
      return QueryHelpers.AddQueryString(apiEndpoint, queryParams);
    }
  }
}