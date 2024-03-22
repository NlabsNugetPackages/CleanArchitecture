using CleanArchitecture.Domain.Abstraction;

namespace CleanArchitecture.Domain.Entities;
public sealed class ProductDetail : Entity
{
    public string ProductId { get; set; } = string.Empty;
    public Product? Product { get; set; }
    public string PropertyName { get; set; } = string.Empty;//Beden Renk gibi İsimler
    public string[] PropertyValues { get; set; } = new string[0]; // M,S,L,XL,XXL || Sarı Kırmızı Mavi
}
