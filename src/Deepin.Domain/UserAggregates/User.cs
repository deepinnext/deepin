using System;
using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.UserAggregates;

public class User : IdentityUser<Guid>, IEntity, IAggregateRoot
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<UserLogin> _userLogins = [];
    private readonly List<UserClaim> _userClaims = [];
    private readonly List<UserToken> _userTokens = [];
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
    public IReadOnlyCollection<UserLogin> UserLogins => _userLogins.AsReadOnly();
    public IReadOnlyCollection<UserClaim> UserClaims => _userClaims.AsReadOnly();
    public IReadOnlyCollection<UserToken> UserTokens => _userTokens.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
