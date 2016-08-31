namespace G2O_Framework
{
    public interface IItem
    {
        int Amount { get; }

        IItemInstance Instance { get; }
    }
}