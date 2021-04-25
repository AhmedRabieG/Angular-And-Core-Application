using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcTaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTaskManager.Identity
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, TaskManagerDbContext>
    {
        public ApplicationRoleStore(TaskManagerDbContext applicationDbContext, IdentityErrorDescriber identityErrorDescriber) : base(applicationDbContext, identityErrorDescriber)
        {

        }
    }
}
