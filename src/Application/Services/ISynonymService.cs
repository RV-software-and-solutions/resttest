namespace RestTest.Application.Services;
public interface ISynonymService
{
    void AddSynonym(string word, string synonymOfWord);
    List<string>? GetAllSynonymsOfWord(string word);
}
