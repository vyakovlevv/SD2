using HSEBank.Domain.Models;

namespace HSEBank.Domain.Handlers;

public class SecuritySystemHandler : OperationHandler
{
    public override bool Handle(Operation op)
    {
        // здесь происходит проверка операции на благонадежность...
        if (op.Amount > 100_000 * 100)
        {
            // Уважаемый, пройдемте на проверку...
            op.UpdateStatus(OperationStatus.Blocked);
            Console.WriteLine("[Security] Операция заблокирована системой безопасности!");
            return false;
        }
        return base.Handle(op);
    }
}