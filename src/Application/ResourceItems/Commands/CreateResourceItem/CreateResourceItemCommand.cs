using MediatR;
using RestTest.Application.Common.Interfaces;
using RestTest.Domain.Entities;
using RestTest.Domain.Events;

namespace RestTest.Application.ResourceItems.Commands.CreateResourceItem;
public record CreateResourceItemCommand : IRequest<int>
{
    public required string Title { get; init; }
    public required string Location { get; init; }
}

public class CreateResourceItemCommandHandler : IRequestHandler<CreateResourceItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateResourceItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateResourceItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ResourceItem
        {
            Title = request.Title,
            Location = request.Location,
        };

        entity.AddDomainEvent(new ResourceItemCreatedEvent(entity));

        _context.ResourceItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
