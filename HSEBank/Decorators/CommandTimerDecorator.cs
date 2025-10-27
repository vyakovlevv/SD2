using System.Diagnostics;
using HSEBank.Commands;

namespace HSEBank.Decorators;

public class CommandTimerDecorator : ICommand
{
    private readonly ICommand _inner;
    private readonly string _name;
    public CommandTimerDecorator(ICommand inner, string name = "")
    {
        _inner = inner; 
        _name = name;
    }

    public void Execute()
    {
        var sw = Stopwatch.StartNew();
        _inner.Execute();
        sw.Stop();
        Console.WriteLine($"[Timer] Command {_name} executed in {sw.ElapsedMilliseconds} ms");
    }

    public void Undo()
    {
        var sw = Stopwatch.StartNew();
        _inner.Undo();
        sw.Stop();
        Console.WriteLine($"[Timer] Command {_name} undone in {sw.ElapsedMilliseconds} ms");
    }
}