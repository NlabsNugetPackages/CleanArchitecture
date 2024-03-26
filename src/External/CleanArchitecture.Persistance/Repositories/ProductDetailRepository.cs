using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Repositories;
internal class ProductDetailRepository : Repository<ProductDetail, AppDbContext>, IProductDetailRepository
{
    public ProductDetailRepository(AppDbContext context) : base(context)
    {
    }
}
