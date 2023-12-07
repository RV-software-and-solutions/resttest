namespace RestTest.Web.Models.Requests.Synonym;

public class AddNewSynonymRequest
{
    public required string SynonymFrom { get; set; }
    public required string SynonymTo { get; set; }
}
