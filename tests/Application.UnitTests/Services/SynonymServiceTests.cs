using NUnit.Framework;
using RestTest.Application.Services;
using RestTest.Core.Services.Graph;
using RestTest.Domain.Dtos.Synonym;

namespace Application.UnitTests.Services;
public class SynonymServiceTests
{
    private ISynonymService _synonymService;
    private IGraphService<SynonymVertex, string> _graphService;

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

        Assert.IsNotEmpty(result);
        Assert.AreEqual(3, result.Count);
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

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result!);
        Assert.AreEqual(2, result!.Count);
        Assert.Contains("B", result);
        Assert.Contains("C", result);
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

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result!);
        Assert.AreEqual(2, result!.Count);
        Assert.Contains("A", result);
        Assert.Contains("C", result);
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

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result!);
        Assert.AreEqual(2, result!.Count);
        Assert.Contains("A", result);
        Assert.Contains("B", result);
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

        Assert.IsNull(result);
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

        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result!);
        Assert.Contains("marko", result);
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
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
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
        Assert.IsNotNull(result);
        Assert.True(result.Exists(vertex => vertex.Value == "A"));
        Assert.True(result.Exists(vertex => vertex.Value == "B"));
        Assert.True(result.Exists(vertex => vertex.Value == "C"));
    }

}
