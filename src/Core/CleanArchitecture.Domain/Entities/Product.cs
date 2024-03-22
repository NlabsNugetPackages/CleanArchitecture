using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;
public sealed class Product : Entity
{
    public string CategoryId { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public ICollection<ProductDetail>? Details { get; set; }
}