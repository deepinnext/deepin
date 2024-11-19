using System;
using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.UserAggregates;

public class UserClaim : IdentityUserClaim<Guid>, IEntity
{

}
