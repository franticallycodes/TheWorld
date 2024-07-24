using Microsoft.Extensions.Caching.Memory;
using TheWorld.Models;

namespace TheWorld.RestCountriesIntegration;

public class RestCountriesApiClient : ApiClientBase, ICountriesClient
{
	private readonly IMemoryCache _cache;
	private const string CacheKey = "AllTheCountriesOfTheWorld"; 

	public RestCountriesApiClient(HttpClient httpClient, ILogger<RestCountriesApiClient> logger, IMemoryCache cache)
		: base(httpClient, logger)
	{
		_cache = cache;
	}

	public async Task<IEnumerable<Country>> GetAllCountries()
	{
		if (_cache.TryGetValue(CacheKey, out IEnumerable<Country> cachedCountries))
			return cachedCountries;

		var allCountries = await GetAsync<Country>("all");
		var sortedCountries = allCountries?.OrderBy(cntry => cntry.Name.Common);

		if (sortedCountries is not null)
			_cache.Set(CacheKey, sortedCountries, TimeSpan.FromHours(6));

		return sortedCountries ?? Enumerable.Empty<Country>();
	}

	public async Task<IEnumerable<Country>> GetCountryByCode(string countryCode) =>
		await GetAsync<Country>($"alpha/{countryCode}") ?? Enumerable.Empty<Country>();

	public async Task<IEnumerable<Region>> GetRegions()
	{
		var allCountries = await GetAllCountries();
		return allCountries?.GroupBy(cntry => cntry.Region)
			.Select(group => new Region
			{
				RegionName = group.Key,
				Countries = group.Select(cntry => cntry.Name.Common)
			}) ?? Enumerable.Empty<Region>();
	}

	public async Task<IEnumerable<Language>> GetLanguages()
	{
		var allCountries = await GetAllCountries();
		return allCountries?.Where(cntry => cntry.Languages is not null)
			.SelectMany(cntry => cntry.Languages.Values.Select(lang =>
					(Language: lang, Country: cntry.Name.Common)))
			.GroupBy(tuple => tuple.Language)
			.Select(group => new Language
			{
				LanguageName = group.Key,
				Countries = group.Select(tuple => tuple.Country)
			}) ?? Enumerable.Empty<Language>();
	}
}

public interface ICountriesClient
{
	Task<IEnumerable<Country>> GetAllCountries();
	Task<IEnumerable<Country>> GetCountryByCode(string countryCode);
	Task<IEnumerable<Region>> GetRegions();
	Task<IEnumerable<Language>> GetLanguages();
}