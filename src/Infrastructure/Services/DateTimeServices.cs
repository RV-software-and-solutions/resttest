using RestTest.Application.Common.Interfaces;

namespace RestTest.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
