using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcTaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTaskManager.Identity
{
    public class ApplicationUserStore :UserStore<ApplicationUser>
    {
        public ApplicationUserStore(TaskManagerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
