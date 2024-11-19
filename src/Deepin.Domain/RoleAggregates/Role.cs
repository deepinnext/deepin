using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.RoleAggregates;

public class Role : IdentityRole<Guid>, IEntity, IAggregateRoot
{
    private readonly List<RoleClaim> roleClaims = [];
    public virtual ICollection<RoleClaim> RoleClaims => roleClaims;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
