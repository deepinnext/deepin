using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.UserAggregates;

public class UserLogin : IdentityUserLogin<Guid>, IEntity
{

}
