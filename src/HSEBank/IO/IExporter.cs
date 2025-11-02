using HSEBank.Domain.Models;

namespace HSEBank.IO;

public interface IExporter
{
    void Export(BankAccount account);
    void Export(Category category);
    void Export(Operation operation);
}