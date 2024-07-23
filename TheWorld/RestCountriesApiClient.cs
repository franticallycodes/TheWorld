namespace TheWorld;

public class RestCountriesApiClient : ApiClientBase, ICountriesClient
{
	public RestCountriesApiClient(HttpClient httpClient, ILogger<RestCountriesApiClient> logger)
		: base(httpClient, logger) { }

	public async Task<IEnumerable<Country>?> GetAllCountries() =>
		await GetMultiple<Country>("all");

	public async Task<Country?> GetCountryByCode(string countryCode) =>
		await GetSingle<Country>($"alpha/{countryCode}");
}

public interface ICountriesClient
{
	Task<IEnumerable<Country>?> GetAllCountries();
	Task<Country?> GetCountryByCode(string countryCode);
}