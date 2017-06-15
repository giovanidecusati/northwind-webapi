namespace NorthWind.Domain.Entities
{
    public enum OrderStatus : byte
    {
        Created = 1,
        InProgress = 2,
        Shipped = 3,
        Cancelled = 4,
    }
}