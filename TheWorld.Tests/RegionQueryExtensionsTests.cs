using FluentAssertions;
using TheWorld.Models;
using TheWorld.QueryExtensions;

namespace TheWorld.Tests;

public class RegionQueryExtensionsTests
{
	private readonly List<Region> _regions = new()
	{
		new() { RegionName = "Asia" },
		new() { RegionName = "Europe" },
		new() { RegionName = "Oceania" },
		new() { RegionName = "Americas" },
		new() { RegionName = "Africa" }
	};

	[Fact]
	public void SearchRegionsWithNullSearchTermReturnsAllRegions()
	{
		var result = _regions.SearchRegions(searchTerm: null);

		result.Should().Equal(_regions);
	}

	[Fact]
	public void SearchRegionsWithEmptySearchTermReturnsAllRegions()
	{
		var result = _regions.SearchRegions(searchTerm: string.Empty);

		result.Should().Equal(_regions);
	}

	[Fact]
	public void SearchRegionsWithMatchingSearchTermReturnsFilteredRegions()
	{
		const string searchTerm = "c";
		var result = _regions.SearchRegions(searchTerm);

		var expected = new List<Region>
		{
			new() { RegionName = "Oceania" },
			new() { RegionName = "Americas" },
			new() { RegionName = "Africa" }
		};

		result.Should().Equal(expected, RegionNameComparer);
	}

	[Fact]
	public void SearchRegionsWithNonMatchingSearchTermReturnsEmpty()
	{
		const string searchTerm = "x";
		var result = _regions.SearchRegions(searchTerm);

		result.Should().BeEmpty();
	}

	[Fact]
	public void SortRegionsAscendingReturnsSortedRegions()
	{
		var result = _regions.SortRegions(sortDesc: false);

		var expected = new List<Region>
		{
			new() { RegionName = "Africa" },
			new() { RegionName = "Americas" },
			new() { RegionName = "Asia" },
			new() { RegionName = "Europe" },
			new() { RegionName = "Oceania" }
		};

		result.Should().Equal(expected, RegionNameComparer);
	}

	[Fact]
	public void SortRegionsDescendingReturnsSortedRegions()
	{
		var result = _regions.SortRegions(sortDesc: true);

		var expected = new List<Region>
		{
			new() { RegionName = "Oceania" },
			new() { RegionName = "Europe" },
			new() { RegionName = "Asia" },
			new() { RegionName = "Americas" },
			new() { RegionName = "Africa" }
		};

		result.Should().Equal(expected, RegionNameComparer);
	}

	private bool RegionNameComparer(Region rgn1, Region rgn2) => rgn1.RegionName == rgn2.RegionName;
}