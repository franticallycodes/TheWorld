using System.Net;

namespace TheWorld.RestCountriesIntegration;

public abstract class ApiClientBase
{
	private readonly HttpClient _httpClient;
	private readonly ILogger _logger;
	private const string Message = "Error accessing external API @ {baseAddress} for endpoint '{urlPath}'";

	protected ApiClientBase(HttpClient httpClient, ILogger logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	protected async Task<IEnumerable<T>?> GetAsync<T>(string urlPath)
	{
		try
		{
			var response = await _httpClient.GetAsync(urlPath);
			if (response.StatusCode == HttpStatusCode.BadRequest) return null;
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
		}
		catch (Exception e)
		{
			_logger.LogError(e, Message, _httpClient.BaseAddress, urlPath);
			throw;
		}
	}
}