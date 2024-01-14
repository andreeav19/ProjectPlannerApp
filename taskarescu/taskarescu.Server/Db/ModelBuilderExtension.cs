using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Models;

namespace taskarescu.Server.Db
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
             
            // roles
            var pwd = "Admin1!";
            var passwordHasher = new PasswordHasher<AppUser>();

            var adminRole = new AppRole()
            {
                Name = "Admin",
            };
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var profRole = new AppRole()
            {
                Name = "Prof",
            };
            profRole.NormalizedName = profRole.Name.ToUpper();

            var studentRole = new AppRole()
            {
                Name = "Student",
            };
            studentRole.NormalizedName = studentRole.Name.ToUpper();

            List<AppRole> roles = new List<AppRole>() { adminRole, profRole, studentRole };
            builder.Entity<AppRole>().HasData(roles);


            // users
            var adminUser = new AppUser()
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                RoleId = adminRole.Id,
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            List<AppUser> users = new List<AppUser>()
            {
                adminUser,
            };

            builder.Entity<AppUser>().HasData(users);


        }
    }
}
