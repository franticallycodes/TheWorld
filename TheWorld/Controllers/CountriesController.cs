using Microsoft.AspNetCore.Mvc;
using TheWorld.QueryExtensions;
using TheWorld.RestCountriesIntegration;

namespace TheWorld.Controllers;

[ApiController]
[Route("api")]
[ResponseCache(Duration = 3600, VaryByQueryKeys = new[]{"*"})]
public class CountriesController : ControllerBase
{
	private readonly ICountriesClient _apiClient;
	private readonly ILogger<CountriesController> _logger;

	public CountriesController(ICountriesClient apiClient, ILogger<CountriesController> logger)
	{
		_apiClient = apiClient;
		_logger = logger;
	}

	[HttpGet("countries")]
	public async Task<IActionResult> GetCountries([FromQuery] string? searchTerm = null, [FromQuery] bool sortDesc = false, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 400)
	{
		try
		{
			if (pageNumber <= 0 || pageSize <= 0)
				return BadRequest($"Invalid pageNumber of {pageNumber} or pageSize of {pageSize} provided.");

			var countries = await _apiClient.GetAllCountries();

			var selectedCountries = countries
				.SearchCountries(searchTerm)
				.SortCountries(sortDesc)
				.GetPage(pageSize, pageNumber)
				.Select(country => country.Name.Common);

			return Ok(selectedCountries);
		}
		catch (Exception e)
		{
			const string message = "Error ID {errorNumber} failed retrieving countries. pageNumber:{pageNumber} pageSize:{pageSize}";
			var errorNumber = Guid.NewGuid();
			_logger.LogError(e, message, errorNumber, pageNumber, pageSize);
			return MyBad(errorNumber);
		}
	}

	[HttpGet("countries/{countryCode}")]
	public async Task<IActionResult> GetCountryByCode(string countryCode)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(countryCode))
				return BadRequest("Provided country code cannot be blank");

			var country = await _apiClient.GetCountryByCode(countryCode);

			return Ok(country);
		}
		catch (Exception e)
		{
			const string message = "Error ID {errorNumber} failed retrieving country. countryCode:{countryCode}";
			var errorNumber = Guid.NewGuid();
			_logger.LogError(e, message, errorNumber, countryCode);
			return MyBad(errorNumber);
		}
	}

	[HttpGet("regions")]
	public async Task<IActionResult> GetCountriesByRegion([FromQuery] string? searchTerm = null, [FromQuery] bool sortDesc = false)
	{
		try
		{
			var regions = await _apiClient.GetRegions();

			var selectedRegions = regions
				.SearchRegions(searchTerm)
				.SortRegions(sortDesc);

			return Ok(selectedRegions);
		}
		catch (Exception e)
		{
			const string message = "Error ID {errorNumber} failed retrieving regions.";
			var errorNumber = Guid.NewGuid();
			_logger.LogError(e, message, errorNumber);
			return MyBad(errorNumber);
		}
	}

	[HttpGet("languages")]
	public async Task<IActionResult> GetCountriesByLanguage([FromQuery] string? searchTerm = null, [FromQuery] bool sortDesc = false)
	{
		try
		{
			var languages = await _apiClient.GetLanguages();

			var selectedLanguages = languages
				.SearchLanguages(searchTerm)
				.SortLanguages(sortDesc);

			return Ok(selectedLanguages);
		}
		catch (Exception e)
		{
			const string message = "Error ID {errorNumber} failed retrieving languages.";
			var errorNumber = Guid.NewGuid();
			_logger.LogError(e, message, errorNumber);
			return MyBad(errorNumber);
		}
	}

	private ObjectResult MyBad(Guid errorNumber) =>
		StatusCode(500, $"Unexpected error occured. Error reference number: {errorNumber}");
}