namespace TheWorld.Models;

public class Language
{
	public string LanguageName { get; set; }
	public IEnumerable<string> Countries { get; set; }
}