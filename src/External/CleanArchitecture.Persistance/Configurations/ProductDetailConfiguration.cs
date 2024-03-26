using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configurations;
internal class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.ToTable("ProductDetails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PropertyName).HasColumnType("varchar(100)");
        builder.Property(x => x.PropertyValues).HasColumnType("varchar(1000)");
    }
}

