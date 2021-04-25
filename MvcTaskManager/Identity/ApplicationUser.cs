using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTaskManager.Identity
{
    public class ApplicationUser : IdentityUser<String>
    {
        [NotMapped]
        public string Token { get; set; }
    }
}
