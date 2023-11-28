using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RestTest.Application.Common.Interfaces;
using RestTest.Application.Common.Mappings;
using RestTest.Application.Common.Models;

namespace RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;
public record GetResourceItemsWithPaginationQuery : IRequest<PaginatedList<ResourceItemDto>>
{
    public string? Title { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetResourceItemsWithPaginationQueryHandler : IRequestHandler<GetResourceItemsWithPaginationQuery, PaginatedList<ResourceItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetResourceItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ResourceItemDto>> Handle(GetResourceItemsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await _context.ResourceItems
            .Where(x => x.Title == query.Title)
            .OrderBy(x => x.Title)
            .ProjectTo<ResourceItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(query.PageNumber, query.PageSize);
    }
}
