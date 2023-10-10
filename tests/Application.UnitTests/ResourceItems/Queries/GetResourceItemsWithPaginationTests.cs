using AutoMapper;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using RestTest.Application.Common.Interfaces;
using RestTest.Application.Common.Mappings;
using RestTest.Application.Common.Models;
using RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;
using RestTest.Domain.Entities;

namespace Application.UnitTests.ResourceItems.Queries;
public class GetResourceItemsWithPaginationTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    private Mock<IApplicationDbContext> _mockContext;

    public GetResourceItemsWithPaginationTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IApplicationDbContext>();
    }

    [Test]
    public async Task ShoudReturn_Not_EmptyList()
    {
        //arrange
        var resourceItems = new List<ResourceItem>()
        {
            new ResourceItem() { Id = 1, Location = "test location", Title = "Super title"}
        };
        _mockContext.Setup(ri => ri.ResourceItems).Returns(resourceItems.AsQueryable().BuildMockDbSet().Object);

        var query = new GetResourceItemsWithPaginationQuery()
        {
            Title = "title",
            PageNumber = 1,
            PageSize = 12
        };

        var handler = new GetResourceItemsWithPaginationQueryHandler(_mockContext.Object, _mapper);

        //act
        PaginatedList<ResourceItemDto> methodResult = await handler.Handle(query, default);

        //assert
        Assert.IsNotEmpty(methodResult.Items);
        Assert.AreEqual("test location", methodResult.Items.First().Location);
    }
}
