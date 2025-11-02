namespace HSEBank.UI.Menu;

public interface IMenuItem
{
    string Title { get; }
    void Execute();
}