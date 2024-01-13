using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Models;

namespace taskarescu.Server.Utils
{
    public static class DatabaseSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope =  serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskarescuDbContext>();

                context.Database.Migrate();

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Name = "Admin" },
                        new Role { Name = "Professor" },
                        new Role { Name = "Student" }
                    );

                    context.SaveChanges();
                }

                if (!context.Difficulties.Any())
                {
                    context.Difficulties.AddRange(
                        new Difficulty { Name = "Easy" },
                        new Difficulty { Name = "Moderate" },
                        new Difficulty { Name = "Intermediate" },
                        new Difficulty { Name = "Challenging" },
                        new Difficulty { Name = "Advanced" }
                    );

                    context.SaveChanges();
                }

                if (!context.Statuses.Any()) {

                    context.Statuses.AddRange(
                        new Status { Name = "To do"},
                        new Status { Name = "In progress"},
                        new Status { Name = "Done"}
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
