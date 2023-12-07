using System.Diagnostics.CodeAnalysis;
using Application.IntegrationTests.Core;
using FluentAssertions;
using NUnit.Framework;
using RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;

namespace Application.IntegrationTests.ResourceItems.Queries;

[ExcludeFromCodeCoverage]
public class GetResourceItemsWithPaginationTests : BaseTesting
{
    [Test]
    public async Task SendAsync_Should_BeEmpty()
    {
        await RunAsDefaultUserAsync();

        var query = new GetResourceItemsWithPaginationQuery
        {
            Title = "todo"
        };

        var result = await SendAsync(query);

        result.Items.Should().BeEmpty();
    }
}