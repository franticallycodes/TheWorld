namespace TheWorld;

public class RestCountriesApiClient : ApiClientBase, ICountriesClient
{
	public RestCountriesApiClient(HttpClient httpClient, ILogger<RestCountriesApiClient> logger)
		: base(httpClient, logger) { }

	public async Task<IEnumerable<Country>?> GetAllCountries() =>
		await GetMultiple<Country>("all");

	public async Task<Country?> GetCountryByCode(string countryCode) =>
		await GetSingle<Country>($"alpha/{countryCode}");

	public async Task<IEnumerable<Region>?> GetRegions()
	{
		var allCountries = await GetAllCountries();
		return allCountries?.GroupBy(cntry => cntry.Region)
			.Select(group => new Region
			{
				RegionName = group.Key,
				Countries = group.Select(cntry => cntry.Name.Common)
			});
	}

	public async Task<IEnumerable<Language>?> GetLanguages()
	{
		var allCountries = await GetAllCountries();
		return allCountries?.SelectMany(cntry =>
				cntry.Languages.Values.Select(lang =>
					(Language: lang, Country: cntry.Name.Common)))
			.GroupBy(tuple => tuple.Language)
			.Select(group => new Language
			{
				LanguageName = group.Key,
				Countries = group.Select(tuple => tuple.Country)
			});
	}
}

public interface ICountriesClient
{
	Task<IEnumerable<Country>?> GetAllCountries();
	Task<Country?> GetCountryByCode(string countryCode);
	Task<IEnumerable<Region>?> GetRegions();
	Task<IEnumerable<Language>?> GetLanguages();
}