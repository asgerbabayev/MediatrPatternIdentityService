using Code.Application.Common.Interfaces;

namespace Code.Infrastructure.Service
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
