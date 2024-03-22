namespace CleanArchitecture.Domain.Entities;
public sealed class OrderDetail
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrderId { get; set; } = string.Empty;
    public Order? Order { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public Product? Product { get; set; }
    public decimal Amount { get; set; } = 0;
    public decimal Price { get; set; } = 0;
}
