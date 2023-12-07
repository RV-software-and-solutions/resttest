using RestTest.Core.Services.Graph.Models;

namespace RestTest.Domain.Dtos.Synonym;

/// <summary>
/// Represents a vertex in a synonym graph, storing a word and its synonyms.
/// </summary>
public class SynonymVertex : AbstractVertex<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SynonymVertex"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor creates a SynonymVertex with no initial value. 
    /// Use the <see cref="InitVertex"/> method to initialize the vertex's value.
    /// </remarks>
    public SynonymVertex()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SynonymVertex"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value (word) to store in the vertex.</param>
    /// <remarks>
    /// This constructor initializes a SynonymVertex with the provided word value.
    /// </remarks>
    public SynonymVertex(string value)
    {
        InitVertex(value);
    }
}
