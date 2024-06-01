using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApiApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}