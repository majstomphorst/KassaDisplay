namespace EventHandeling
{
    public interface IProduct
    {
        string Barcode { get; }
        string Description { get; }
        decimal Amount { get; }

        string ToFormattedString();
    }
    
}