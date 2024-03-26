using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Repositories;
internal class OrderRepository : Repository<Order, AppDbContext>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}
