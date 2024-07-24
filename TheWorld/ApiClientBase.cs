﻿using System.Text.Json;

namespace TheWorld;

public abstract class ApiClientBase
{
	// if we had multiple API integrations we'd probably want to identify HttpClient by name
	private readonly HttpClient _httpClient;
	private readonly ILogger _logger;
	const string Message = "Error accessing external API @ {baseAddress} for endpoint '{urlPath}'";

	protected ApiClientBase(HttpClient httpClient, ILogger logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	protected async Task<IEnumerable<T>?> GetMultiple<T>(string urlPath)
	{
		try
		{
			return await _httpClient.GetFromJsonAsync<IEnumerable<T>>(urlPath);
		}
		catch (Exception e)
		{
			_logger.LogError(e, Message, _httpClient.BaseAddress, urlPath);
			throw;
		}
	}

	protected async Task<T?> GetSingle<T>(string urlPath) where T : class =>
		(await GetMultiple<T>(urlPath))?.FirstOrDefault();
}