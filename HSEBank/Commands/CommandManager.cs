namespace HSEBank.Commands;

public class CommandManager
{
    private readonly Stack<ICommand> _history = new();
    public void Execute(ICommand cmd)
    {
        cmd.Execute();
        _history.Push(cmd);
    }

    public void Undo()
    {
        if (!_history.Any()) return;
        var command = _history.Pop();
        command.Undo();
    }
}