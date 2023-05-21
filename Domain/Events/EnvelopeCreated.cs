using Domain.Common;

namespace Domain.Events
{
    public record EnvelopeCreated(string From, string Destination, string Message, string Subject) : BaseEvent;
}
