using FluentAssertions;
using TheWorld.Models;
using TheWorld.QueryExtensions;

namespace TheWorld.Tests;

public class CountryQueryExtensionsTests
{
	private readonly List<Country> _countries = new()
	{
		new() { Name = new() { Common = "Afghanistan" } },
		new() { Name = new() { Common = "Australia" } },
		new() { Name = new() { Common = "Brazil" } },
		new() { Name = new() { Common = "Canada" } },
		new() { Name = new() { Common = "China" } },
		new() { Name = new() { Common = "Dominican Republic" } },
		new() { Name = new() { Common = "Egypt" } },
		new() { Name = new() { Common = "Finland" } },
		new() { Name = new() { Common = "Greenland" } },
		new() { Name = new() { Common = "Hong Kong" } },
		new() { Name = new() { Common = "Israel" } },
		new() { Name = new() { Common = "Japan" } },
		new() { Name = new() { Common = "Kenya" } },
		new() { Name = new() { Common = "Liechtenstein" } },
		new() { Name = new() { Common = "Martinique" } },
		new() { Name = new() { Common = "Mexico" } },
		new() { Name = new() { Common = "New Zealand" } },
		new() { Name = new() { Common = "Oman" } },
		new() { Name = new() { Common = "Philippines" } },
		new() { Name = new() { Common = "Romania" } },
		new() { Name = new() { Common = "Spain" } },
		new() { Name = new() { Common = "Thailand" } },
		new() { Name = new() { Common = "Ukraine" } },
		new() { Name = new() { Common = "United States" } },
		new() { Name = new() { Common = "Vatican City" } },
		new() { Name = new() { Common = "Wallis and Futuna" } },
		new() { Name = new() { Common = "Western Sahara" } },
		new() { Name = new() { Common = "Yemen" } },
		new() { Name = new() { Common = "Zimbabwe" } }
	};

	[Fact]
	public void SearchCountriesWithNullSearchTermReturnsAllCountries()
	{
		var result = _countries.SearchCountries(searchTerm: null);

		result.Should().Equal(_countries);
	}

	[Fact]
	public void SearchCountriesWithEmptySearchTermReturnsAllCountries()
	{
		var result = _countries.SearchCountries(searchTerm: string.Empty);

		result.Should().Equal(_countries);
	}

	[Fact]
	public void SearchCountriesWithMatchingSearchTermReturnsFilteredCountries()
	{
		const string searchTerm = "martin";
		var result = _countries.SearchCountries(searchTerm);

		var expected = new List<Country>
		{
			new() { Name = new() { Common = "Martinique" } },
		};

		result.Should().Equal(expected, CountryNameComparer);
	}

	[Fact]
	public void SearchCountriesWithNonMatchingSearchTermReturnsEmpty()
	{
		const string searchTerm = "zz";
		var result = _countries.SearchCountries(searchTerm);

		result.Should().BeEmpty();
	}

	[Fact]
	public void SortCountriesAscendingReturnsSortedCountries()
	{
		var result = _countries.SortCountries(sortDesc: false);

		var expected = new List<Country>
		{
			new() { Name = new() { Common = "Afghanistan" } },
			new() { Name = new() { Common = "Australia" } },
			new() { Name = new() { Common = "Brazil" } },
			new() { Name = new() { Common = "Canada" } },
			new() { Name = new() { Common = "China" } },
			new() { Name = new() { Common = "Dominican Republic" } },
			new() { Name = new() { Common = "Egypt" } },
			new() { Name = new() { Common = "Finland" } },
			new() { Name = new() { Common = "Greenland" } },
			new() { Name = new() { Common = "Hong Kong" } },
			new() { Name = new() { Common = "Israel" } },
			new() { Name = new() { Common = "Japan" } },
			new() { Name = new() { Common = "Kenya" } },
			new() { Name = new() { Common = "Liechtenstein" } },
			new() { Name = new() { Common = "Martinique" } },
			new() { Name = new() { Common = "Mexico" } },
			new() { Name = new() { Common = "New Zealand" } },
			new() { Name = new() { Common = "Oman" } },
			new() { Name = new() { Common = "Philippines" } },
			new() { Name = new() { Common = "Romania" } },
			new() { Name = new() { Common = "Spain" } },
			new() { Name = new() { Common = "Thailand" } },
			new() { Name = new() { Common = "Ukraine" } },
			new() { Name = new() { Common = "United States" } },
			new() { Name = new() { Common = "Vatican City" } },
			new() { Name = new() { Common = "Wallis and Futuna" } },
			new() { Name = new() { Common = "Western Sahara" } },
			new() { Name = new() { Common = "Yemen" } },
			new() { Name = new() { Common = "Zimbabwe" } }
		};

		result.Should().Equal(expected, CountryNameComparer);
	}

	[Fact]
	public void SortCountriesDescendingReturnsSortedCountries()
	{
		var result = _countries.SortCountries(sortDesc: true);

		var expected = new List<Country>
		{
			new() { Name = new() { Common = "Zimbabwe" } },
			new() { Name = new() { Common = "Yemen" } },
			new() { Name = new() { Common = "Western Sahara" } },
			new() { Name = new() { Common = "Wallis and Futuna" } },
			new() { Name = new() { Common = "Vatican City" } },
			new() { Name = new() { Common = "United States" } },
			new() { Name = new() { Common = "Ukraine" } },
			new() { Name = new() { Common = "Thailand" } },
			new() { Name = new() { Common = "Spain" } },
			new() { Name = new() { Common = "Romania" } },
			new() { Name = new() { Common = "Philippines" } },
			new() { Name = new() { Common = "Oman" } },
			new() { Name = new() { Common = "New Zealand" } },
			new() { Name = new() { Common = "Mexico" } },
			new() { Name = new() { Common = "Martinique" } },
			new() { Name = new() { Common = "Liechtenstein" } },
			new() { Name = new() { Common = "Kenya" } },
			new() { Name = new() { Common = "Japan" } },
			new() { Name = new() { Common = "Israel" } },
			new() { Name = new() { Common = "Hong Kong" } },
			new() { Name = new() { Common = "Greenland" } },
			new() { Name = new() { Common = "Finland" } },
			new() { Name = new() { Common = "Egypt" } },
			new() { Name = new() { Common = "Dominican Republic" } },
			new() { Name = new() { Common = "China" } },
			new() { Name = new() { Common = "Canada" } },
			new() { Name = new() { Common = "Brazil" } },
			new() { Name = new() { Common = "Australia" } },
			new() { Name = new() { Common = "Afghanistan" } }
		};

		result.Should().Equal(expected, CountryNameComparer);
	}

	[Fact]
	public void GetPageWithValidPageSizeAndNumberReturnsPagedCountries()
	{
		var result = _countries.GetPage(pageSize: 2, pageNumber: 2);

		var expected = new List<Country>
		{
			new() { Name = new() { Common = "Brazil" } },
			new() { Name = new() { Common = "Canada" } }
		};

		result.Should().Equal(expected, CountryNameComparer);
	}

	[Theory]
	[InlineData(0, 1)]
	[InlineData(0, 0)]
	public void GetPageWithInvalidPageSizeOrNumberReturnsEmpty(int pageSize, int pageNumber)
	{
		var result = _countries.GetPage(pageSize, pageNumber);

		result.Should().BeEmpty();
	}

	private bool CountryNameComparer(Country cntry1, Country cntry2) => cntry1.Name.Common == cntry2.Name.Common;
}