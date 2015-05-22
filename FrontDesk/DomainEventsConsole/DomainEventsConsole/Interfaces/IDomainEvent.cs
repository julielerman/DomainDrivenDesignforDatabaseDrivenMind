using System;
using System.Linq;

namespace DomainEventsConsole.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}
