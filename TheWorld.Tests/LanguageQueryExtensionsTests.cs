using FluentAssertions;
using TheWorld.Models;
using TheWorld.QueryExtensions;

namespace TheWorld.Tests;

public class LanguageQueryExtensionsTests
{
	private readonly List<Language> _languages = new()
	{
		new() { LanguageName = "Arabic" },
		new() { LanguageName = "Bosnian" },
		new() { LanguageName = "Chinese" },
		new() { LanguageName = "Dutch" },
		new() { LanguageName = "English" },
		new() { LanguageName = "Finnish" },
		new() { LanguageName = "French" },
		new() { LanguageName = "German" },
		new() { LanguageName = "Greek" },
		new() { LanguageName = "Hebrew" },
		new() { LanguageName = "Hindi" },
		new() { LanguageName = "Irish" },
		new() { LanguageName = "Italian" },
		new() { LanguageName = "Japanese" },
		new() { LanguageName = "Korean" },
		new() { LanguageName = "Latin" },
		new() { LanguageName = "Mauritian Creole" },
		new() { LanguageName = "Mongolian" },
		new() { LanguageName = "New Zealand Sign Language" },
		new() { LanguageName = "Norwegian" },
		new() { LanguageName = "Polish" },
		new() { LanguageName = "Portuguese" },
		new() { LanguageName = "Quechua" },
		new() { LanguageName = "Romanian" },
		new() { LanguageName = "Spanish" },
		new() { LanguageName = "Thai" },
		new() { LanguageName = "Turkish" },
		new() { LanguageName = "Ukrainian" },
		new() { LanguageName = "Vietnamese" },
		new() { LanguageName = "Xhosa" },
		new() { LanguageName = "Zimbabwean Sign Language" }
	};

	[Fact]
	public void SearchLanguagesWithNullSearchTermReturnsAllLanguages()
	{
		var result = _languages.SearchLanguages(searchTerm: null);

		result.Should().Equal(_languages);
	}

	[Fact]
	public void SearchLanguagesWithEmptySearchTermReturnsAllLanguages()
	{
		var result = _languages.SearchLanguages(searchTerm: string.Empty);

		result.Should().Equal(_languages);
	}

	[Fact]
	public void SearchLanguagesWithMatchingSearchTermReturnsFilteredLanguages()
	{
		const string searchTerm = "eng";
		var result = _languages.SearchLanguages(searchTerm);

		var expected = new List<Language>
		{
			new() { LanguageName = "English" }
		};

		result.Should().Equal(expected, LanguageNameComparer);
	}

	[Fact]
	public void SearchLanguagesWithNonMatchingSearchTermReturnsEmpty()
	{
		const string searchTerm = "zz";
		var result = _languages.SearchLanguages(searchTerm);

		result.Should().BeEmpty();
	}

	[Fact]
	public void SortLanguagesAscendingReturnsSortedLanguages()
	{
		var result = _languages.SortLanguages(sortDesc: false);

		var expected = new List<Language>
		{
			new() { LanguageName = "Arabic" },
			new() { LanguageName = "Bosnian" },
			new() { LanguageName = "Chinese" },
			new() { LanguageName = "Dutch" },
			new() { LanguageName = "English" },
			new() { LanguageName = "Finnish" },
			new() { LanguageName = "French" },
			new() { LanguageName = "German" },
			new() { LanguageName = "Greek" },
			new() { LanguageName = "Hebrew" },
			new() { LanguageName = "Hindi" },
			new() { LanguageName = "Irish" },
			new() { LanguageName = "Italian" },
			new() { LanguageName = "Japanese" },
			new() { LanguageName = "Korean" },
			new() { LanguageName = "Latin" },
			new() { LanguageName = "Mauritian Creole" },
			new() { LanguageName = "Mongolian" },
			new() { LanguageName = "New Zealand Sign Language" },
			new() { LanguageName = "Norwegian" },
			new() { LanguageName = "Polish" },
			new() { LanguageName = "Portuguese" },
			new() { LanguageName = "Quechua" },
			new() { LanguageName = "Romanian" },
			new() { LanguageName = "Spanish" },
			new() { LanguageName = "Thai" },
			new() { LanguageName = "Turkish" },
			new() { LanguageName = "Ukrainian" },
			new() { LanguageName = "Vietnamese" },
			new() { LanguageName = "Xhosa" },
			new() { LanguageName = "Zimbabwean Sign Language" }
		};

		result.Should().Equal(expected, LanguageNameComparer);
	}

	[Fact]
	public void SortLanguagesDescendingReturnsSortedLanguages()
	{
		var result = _languages.SortLanguages(sortDesc: true);

		var expected = new List<Language>
		{
			new() { LanguageName = "Zimbabwean Sign Language" },
			new() { LanguageName = "Xhosa" },
			new() { LanguageName = "Vietnamese" },
			new() { LanguageName = "Ukrainian" },
			new() { LanguageName = "Turkish" },
			new() { LanguageName = "Thai" },
			new() { LanguageName = "Spanish" },
			new() { LanguageName = "Romanian" },
			new() { LanguageName = "Quechua" },
			new() { LanguageName = "Portuguese" },
			new() { LanguageName = "Polish" },
			new() { LanguageName = "Norwegian" },
			new() { LanguageName = "New Zealand Sign Language" },
			new() { LanguageName = "Mongolian" },
			new() { LanguageName = "Mauritian Creole" },
			new() { LanguageName = "Latin" },
			new() { LanguageName = "Korean" },
			new() { LanguageName = "Japanese" },
			new() { LanguageName = "Italian" },
			new() { LanguageName = "Irish" },
			new() { LanguageName = "Hindi" },
			new() { LanguageName = "Hebrew" },
			new() { LanguageName = "Greek" },
			new() { LanguageName = "German" },
			new() { LanguageName = "French" },
			new() { LanguageName = "Finnish" },
			new() { LanguageName = "English" },
			new() { LanguageName = "Dutch" },
			new() { LanguageName = "Chinese" },
			new() { LanguageName = "Bosnian" },
			new() { LanguageName = "Arabic" }
		};

		result.Should().Equal(expected, LanguageNameComparer);
	}

	private bool LanguageNameComparer(Language lang1, Language lang2) => lang1.LanguageName == lang2.LanguageName;
}