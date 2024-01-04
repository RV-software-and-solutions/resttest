using NUnit.Framework;
using RestTest.Application.Services;
using RestTest.Core.Services.Graph;
using RestTest.Domain.Dtos.Synonym;

namespace Application.UnitTests.Services;
public class SynonymServiceTests
{
    private ISynonymService _synonymService = null!;
    private IGraphService<SynonymVertex, string> _graphService = null!;

    [SetUp]
    public void Setup()
    {
        _graphService = new GraphService<SynonymVertex, string>();
        _synonymService = new SynonymService(_graphService);
    }

    [Test]
    public void AddSynonym_Should_Add_Three_Synonyms()
    {
        //arrange

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");

        //assert
        List<SynonymVertex> result = _synonymService.GetAllSynonyms();

        Assert.That(3, Is.EqualTo(result.Count));
    }

    [Test]
    public void GetAllSynonymsByWord_Should_Return_B_C()
    {
        //arrange
        string findSynonymsFrom = "A";

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");

        //assert
        List<string> result = _synonymService.GetAllSynonymsByWord(findSynonymsFrom)!;

        Assert.That(2, Is.EqualTo(result!.Count));
        Assert.That("B", Is.AnyOf(result));
        Assert.That("C", Is.AnyOf(result));
    }

    [Test]
    public void GetAllSynonymsByWord_Should_Return_A_C()
    {
        //arrange
        string findSynonymsFrom = "B";

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");

        //assert
        List<string> result = _synonymService.GetAllSynonymsByWord(findSynonymsFrom)!;

        Assert.That(2, Is.EqualTo(result!.Count));
        Assert.That("A", Is.AnyOf(result));
        Assert.That("C", Is.AnyOf(result));
    }

    [Test]
    public void GetAllSynonymsByWord_Should_Return_A_B()
    {
        //arrange
        string findSynonymsFrom = "C";

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");

        //assert
        List<string> result = _synonymService.GetAllSynonymsByWord(findSynonymsFrom)!;

        Assert.That(2, Is.EqualTo(result!.Count));
        Assert.That("A", Is.AnyOf(result));
        Assert.That("B", Is.AnyOf(result));
    }

    [Test]
    public void GetAllSynonymsByWord_Should_Return_NullList()
    {
        //arrange
        string findSynonymsFrom = "rvstest";

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");

        //assert
        List<string>? result = _synonymService.GetAllSynonymsByWord(findSynonymsFrom);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAllSynonymsByWord_Should_Return_Synonyms_For_Ratomir()
    {
        //arrange
        string findSynonymsFrom = "ratomir";

        //act
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");
        _synonymService.AddSynonym("ratomir", "marko");

        //assert
        List<string>? result = _synonymService.GetAllSynonymsByWord(findSynonymsFrom);

        Assert.That("marko", Is.AnyOf(result!));
    }

    [Test]
    public void ClearAllSynonyms_Should_Return_EmptyList()
    {
        //arrange
        _synonymService.AddSynonym("A", "B");
        _synonymService.AddSynonym("B", "C");
        _synonymService.AddSynonym("ratomir", "marko");

        //act
        _synonymService.ClearAllSynonyms();
        List<SynonymVertex> result = _synonymService.GetAllSynonyms();

        //assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void LoadSynonymsIntoState_Should_Return_Three_Vertex()
    {
        //arrange
        List<SynonymVertex> synonymVertices = new()
        {
            { new SynonymVertex(){ Id = "1", Value = "A", } },
            { new SynonymVertex(){ Id = "2", Value = "B", } },
            { new SynonymVertex(){ Id = "3", Value = "C", } },
        };
        _synonymService.LoadSynonymsIntoState(synonymVertices);

        //act
        List<SynonymVertex> result = _synonymService.GetAllSynonyms();

        //assert
        Assert.That(result.Exists(vertex => vertex.Value == "A"));
        Assert.That(result.Exists(vertex => vertex.Value == "B"));
        Assert.That(result.Exists(vertex => vertex.Value == "C"));
    }
}
