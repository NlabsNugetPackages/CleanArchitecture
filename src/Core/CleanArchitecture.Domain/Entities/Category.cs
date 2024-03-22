using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;
public sealed class Category : Entity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product>? Products { get; set; }
}