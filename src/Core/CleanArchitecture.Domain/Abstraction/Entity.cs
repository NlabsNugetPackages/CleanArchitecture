using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Abstraction;
public abstract class Entity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string CreatedBy { get; set; } = string.Empty;
    public AppUser? CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public AppUser? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? DeletedBy { get; set; }
    public AppUser? DeletedUser { get; set; }
    public DateTime? DeletedDate { get; set; }
}