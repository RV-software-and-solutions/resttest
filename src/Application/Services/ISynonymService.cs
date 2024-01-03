using RestTest.Domain.Dtos.Synonym;

namespace RestTest.Application.Services;
public interface ISynonymService
{
    /// <summary>
    /// Adds a synonym relationship between two words.
    /// </summary>
    /// <param name="word">The word to add a synonym for.</param>
    /// <param name="synonymOfWord">The synonym of the word.</param>
    void AddSynonym(string word, string synonymOfWord);

    Task AddSynonymAsync(string word, string synonymOfWord);

    /// <summary>
    /// Retrieves all synonyms for a specific word.
    /// </summary>
    /// <param name="word">The word to find synonyms for.</param>
    /// <returns>A list of synonyms for the specified word or null if none are found.</returns>
    List<string>? GetAllSynonymsByWord(string word);

    /// <summary>
    /// Retrieves all synonyms in the graph.
    /// </summary>
    /// <returns>A list of all synonym vertices.</returns>
    List<SynonymVertex> GetAllSynonyms();

    /// <summary>
    /// Loads a list of synonyms into the graph.
    /// </summary>
    /// <param name="synonyms">The list of synonyms to load into the graph.</param>
    void LoadSynonymsIntoState(List<SynonymVertex> synonyms);

    /// <summary>
    /// Clears all synonyms from the graph.
    /// </summary>
    void ClearAllSynonyms();
}
