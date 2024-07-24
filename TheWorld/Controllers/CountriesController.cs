using Microsoft.AspNetCore.Mvc;

namespace TheWorld.Controllers;

[ApiController]
[Route("api")]
[ResponseCache(Duration = 3600)]
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
	public async Task<IActionResult> GetCountries([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 400)
	{
		try
		{
			if (pageNumber <= 0 || pageSize <= 0)
				return BadRequest($"Invalid pageNumber of {pageNumber} or pageSize of {pageSize} provided.");

			var data = await _apiClient.GetAllCountries();

			if (data is null) return NoContent(); // this seems highly unlikely that the API call succeeds but no countries return

			var pagedData = data.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.Select(country => country.Name.Common);

			return Ok(pagedData);
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
			if (countryCode.Length == 0)
				return BadRequest("Provided country code cannot be blank");

			var data = await _apiClient.GetCountryByCode(countryCode);

			if (data is null) return NoContent();

			return Ok(data);
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
	public async Task<IActionResult> GetCountriesByRegion()
	{
		try
		{
			var data = await _apiClient.GetRegions();

			if (data is null) return NoContent();

			return Ok(data);
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
	public async Task<IActionResult> GetCountriesByLanguages()
	{
		try
		{
			var data = await _apiClient.GetLanguages();

			if (data is null) return NoContent();

			return Ok(data);
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