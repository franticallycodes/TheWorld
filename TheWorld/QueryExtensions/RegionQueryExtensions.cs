using TheWorld.Models;

namespace TheWorld.QueryExtensions;

public static class RegionQueryExtensions
{
	public static IEnumerable<Region> SearchRegions(this IEnumerable<Region> regions, string? searchTerm) =>
		string.IsNullOrWhiteSpace(searchTerm)
			? regions
			: regions.Where(rgn => rgn.RegionName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

	public static IEnumerable<Region> SortRegions(this IEnumerable<Region> regions, bool sortDesc) =>
		sortDesc
			? regions.OrderByDescending(rgn => rgn.RegionName)
			: regions.OrderBy(rgn => rgn.RegionName);
}