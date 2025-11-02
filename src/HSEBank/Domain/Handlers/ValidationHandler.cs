using HSEBank.Domain.Models;

namespace HSEBank.Domain.Handlers;

public class ValidationHandler : OperationHandler
{
    public override bool Handle(Operation op)
    {
        // здесь происходят валидации данных...
        return base.Handle(op);
    }
}