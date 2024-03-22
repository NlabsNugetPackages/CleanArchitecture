using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;
public sealed class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public AppUser? User { get; set; }
    public decimal TotalPrice { get; set; } = 0;
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<OrderDetail>? Details { get; set; }
}