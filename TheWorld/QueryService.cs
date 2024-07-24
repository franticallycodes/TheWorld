using TheWorld.Models;

namespace TheWorld;

public class QueryService : IQueryService
{
	public IEnumerable<string> GetPage(IEnumerable<Country> countries, int pageSize, int pageNumber) =>
		countries.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Select(country => country.Name.Common);
}

public interface IQueryService
{
	IEnumerable<string> GetPage(IEnumerable<Country> countries, int pageSize, int pageNumber);
}