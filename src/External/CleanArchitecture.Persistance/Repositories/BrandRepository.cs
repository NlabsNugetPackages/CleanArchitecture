using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Repositories;
internal class BrandRepository : Repository<Brand, AppDbContext>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }
}
