using System;
using Microsoft.AspNetCore.Identity;

namespace Deepin.Domain.UserAggregates;

public class UserRole: IdentityUserRole<Guid>, IEntity
{

}
