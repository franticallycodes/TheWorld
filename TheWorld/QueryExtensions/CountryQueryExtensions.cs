using TheWorld.Models;

namespace TheWorld.QueryExtensions;

public static class CountryQueryExtensions
{
	public static IEnumerable<Country> SearchCountries(this IEnumerable<Country> countries, string? searchTerm) =>
		string.IsNullOrWhiteSpace(searchTerm)
			? countries
			: countries.Where(cntry =>
				cntry.Name.Common.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

	public static IEnumerable<Country> SortCountries(this IEnumerable<Country> countries, bool sortDesc) =>
		sortDesc
			? countries.OrderByDescending(cntry => cntry.Name.Common)
			: countries.OrderBy(cntry => cntry.Name.Common);

	public static IEnumerable<Country> GetPage(this IEnumerable<Country> countries, int pageSize, int pageNumber) =>
		countries.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}