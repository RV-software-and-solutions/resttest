using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;

namespace Application.IntegrationTests.ResourceItems.Queries;

[ExcludeFromCodeCoverage]
public class GetResourceItemsWihtPaginationTests : BaseTesting
{
    [Test]
    public async Task Shoud()
    {
        await RunAsDefaultUserAsync();

        var query = new GetResourceItemsWithPaginationQuery();
        query.Title = "todo";

        var result = await SendAsync(query);

        result.Items.Should().NotBeEmpty();
    }
}