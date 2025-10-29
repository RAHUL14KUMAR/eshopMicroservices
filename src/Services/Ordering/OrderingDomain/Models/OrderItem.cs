namespace OrderingDomain.Models;
public class OrderItem : Entity<Guid>
{
    internal OrderItem(Guid productId, Guid orderId, decimal price, int quantity)
    {
        ProductId = productId;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }
    public Guid ProductId { get; private set; } = default!;
    public Guid OrderId { get; private set; }= default!;
    public decimal Price { get; private set; }= default!;
    public int Quantity { get; private set; }= default!;
}