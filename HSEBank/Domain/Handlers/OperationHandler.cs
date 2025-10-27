using HSEBank.Domain.Models;

namespace HSEBank.Domain.Handlers;

public abstract class OperationHandler
{
    protected OperationHandler? Next { get; private set; }
    public OperationHandler SetNext(OperationHandler next) { Next = next; return next; }
    public virtual bool Handle(Operation op)
    {
        if (Next != null) return Next.Handle(op);
        return true;
    }
}