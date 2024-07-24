using TheWorld.Models;

namespace TheWorld.QueryExtensions;

public static class LanguageQueryExtensions
{
	public static IEnumerable<Language> SearchLanguages(this IEnumerable<Language> languages, string? searchTerm) =>
		string.IsNullOrWhiteSpace(searchTerm)
			? languages
			: languages.Where(lang => lang.LanguageName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

	public static IEnumerable<Language> SortLanguages(this IEnumerable<Language> languages, bool sortDesc) =>
		sortDesc
			? languages.OrderByDescending(lang => lang.LanguageName)
			: languages.OrderBy(lang => lang.LanguageName);
}