using System;
using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.UserAggregates;

public class UserToken: IdentityUserToken<Guid>, IEntity
{

}
