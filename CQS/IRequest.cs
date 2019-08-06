using System;

namespace CQS.Data
{
    public interface IRequest
    {
        Guid CorrelationId { get; set; }
    }
}