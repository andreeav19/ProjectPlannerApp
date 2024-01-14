using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        // public int Id { get; set; }
        // public string Name { get; set; }

        //public virtual ICollection<AppUser> Users { get; set; }
    }
}