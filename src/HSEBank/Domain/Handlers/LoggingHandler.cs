using HSEBank.Domain.Models;

namespace HSEBank.Domain.Handlers;

public class LoggingHandler : OperationHandler
{
    public override bool Handle(Operation op)
    {
        op.UpdateStatus(OperationStatus.Completed);
        Console.WriteLine($"[Handler] Logging operation {op.Id} Amount={op.Amount} Date={op.Date}");
        return base.Handle(op);
    }
}