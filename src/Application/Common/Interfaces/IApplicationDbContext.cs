using Microsoft.EntityFrameworkCore;
using RestTest.Domain.Entities;

namespace RestTest.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<ResourceItem> ResourceItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
