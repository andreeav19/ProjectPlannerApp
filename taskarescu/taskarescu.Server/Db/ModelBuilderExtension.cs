using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
                Id = "acbda893-a8e4-45f2-b3f9-2a0068b29f57",
                Name = "Admin",
            };
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var profRole = new AppRole()
            {
                Id = "311c9a88-fe29-4b7c-a8bb-43aef2f3013c",
                Name = "Prof",
            };
            profRole.NormalizedName = profRole.Name.ToUpper();

            var studentRole = new AppRole()
            {
                Id = "6d0fea85-946f-453a-9897-863f79b652cb",
                Name = "Student",
            };
            studentRole.NormalizedName = studentRole.Name.ToUpper();

            List<AppRole> roles = new List<AppRole>() { adminRole, profRole, studentRole };
            builder.Entity<AppRole>().HasData(roles);


            // users
            var adminUser = new AppUser()
            {
                Id = "bbfcea33-5568-4558-b6c0-9353518b9261",
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

            var student1 = new AppUser()
            {
                Id = "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3",
                UserName = "lisamiller",
                Email = "lisa.miller@student.com",
                EmailConfirmed = true,
                FirstName = "Lisa",
                LastName = "Miller",
                RoleId = studentRole.Id,
            };
            student1.NormalizedUserName = student1.UserName.ToUpper();
            student1.NormalizedEmail = student1.Email.ToUpper();
            student1.PasswordHash = passwordHasher.HashPassword(student1, "Student1!");

            var student2 = new AppUser()
            {
                Id = "590201ab-1c71-4d80-8da8-78be2bd3df9a",
                UserName = "alexwong",
                Email = "alex.wong@student.com",
                EmailConfirmed = true,
                FirstName = "Alex",
                LastName = "Wong",
                RoleId = studentRole.Id,
            };
            student2.NormalizedUserName = student2.UserName.ToUpper();
            student2.NormalizedEmail = student2.Email.ToUpper();
            student2.PasswordHash = passwordHasher.HashPassword(student2, "Student2!");

            var student3 = new AppUser
            {
                Id = "3b11ba9f-2b09-4b1a-b784-87e0040a2f56",
                UserName = "samjones",
                Email = "sam.jones@student.com",
                EmailConfirmed = true,
                FirstName = "Sam",
                LastName = "Jones",
                RoleId = studentRole.Id,
            };
            student3.NormalizedUserName = student3.UserName.ToUpper();
            student3.NormalizedEmail = student3.Email.ToUpper();
            student3.PasswordHash = passwordHasher.HashPassword(student3, "Student3!");

            var professor1 = new AppUser()
            {
                Id = "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f",
                UserName = "emilyjones",
                Email = "emily.jones@professor.com",
                EmailConfirmed = true,
                FirstName = "Emily",
                LastName = "Jones",
                RoleId = profRole.Id,
            };
            professor1.NormalizedUserName = professor1.UserName.ToUpper();
            professor1.NormalizedEmail = professor1.Email.ToUpper();
            professor1.PasswordHash = passwordHasher.HashPassword(professor1, "Professor1!");

            var professor2 = new AppUser()
            {
                Id = "f2517e43-07ae-4c0f-8f63-e2481b47a5c7",
                UserName = "danielwhite",
                Email = "daniel.white@professor.com",
                EmailConfirmed = true,
                FirstName = "Daniel",
                LastName = "White",
                RoleId = profRole.Id,
            };
            professor2.NormalizedUserName = professor2.UserName.ToUpper();
            professor2.NormalizedEmail = professor2.Email.ToUpper();
            professor2.PasswordHash = passwordHasher.HashPassword(professor2, "Professor2!");

            List<AppUser> users = new List<AppUser>()
            {
                adminUser, student1, student2, student3, professor1, professor2
            };

            builder.Entity<AppUser>().HasData(users);

            // badges
            builder.Entity<Badge>().HasData(
                new Badge { Id = 1, Name = "Project Planner", Description = "Awarded to users who excel in project planning." },
                new Badge { Id = 2, Name = "Task Master", Description = "Awarded to users who consistently complete tasks on time." },
                new Badge { Id = 3, Name = "Team Player", Description = "Awarded to users who demonstrate excellent teamwork skills." },
                new Badge { Id = 4, Name = "Innovator", Description = "Awarded to users who propose innovative solutions to project challenges." },
                new Badge { Id = 5, Name = "Problem Solver", Description = "Awarded to users who effectively solve complex problems within a project." },
                new Badge { Id = 6, Name = "Communication Pro", Description = "Awarded to users who excel in project communication and collaboration." }
            );

            //projects
            builder.Entity<Project>().HasData(
                new Project { Id = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Name = "Baze de Date Avansate", Description = "Proiect legat de optimizarea și gestionarea bazelor de date avansate.", UserId = adminUser.Id },
                new Project { Id = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Name = "Rețele de Calculatoare", Description = "Implementarea unei rețele de calculatoare eficiente.", UserId = professor1.Id },
                new Project { Id = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Name = "Inteligenta Artificiala", Description = "Proiect în domeniul inteligenței artificiale și învățare automată.", UserId = professor1.Id }
            );

            // statuses
            List<Status> statuses = new List<Status>() {
                new Status() {Id = 1, Name = "To Do" },
                new Status() {Id = 2, Name = "In Progress" },
                new Status() {Id = 3, Name = "Done" }
            };

            builder.Entity<Status>().HasData(statuses);

            // studentprojects
            builder.Entity<StudentProject>().HasData(
                new StudentProject { UserId = student1.Id, ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770") },
                new StudentProject { UserId = student2.Id, ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770") },
                new StudentProject { UserId = student3.Id, ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770") },
                new StudentProject { UserId = student2.Id, ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e") },
                new StudentProject { UserId = student3.Id, ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e") },
                new StudentProject { UserId = student3.Id, ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe") },
                new StudentProject { UserId = student2.Id, ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe") }
            );

            // task items
            builder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Name = "Implementare funcționalitate X", Description = "Implementare funcționalitate X în cadrul proiectului Y.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(7), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 2, Name = "Testare modul Y", Description = "Testare modul Y în cadrul proiectului Z.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(10), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 3, Name = "Documentare proiect", Description = "Documentare proiect pentru prezentare finală.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(5), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 4, Name = "Soluționare bug-uri", Description = "Soluționare bug-uri identificate în ultima versiune a proiectului.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(3), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 5, Name = "Optimizare performanță", Description = "Optimizare performanță în cadrul aplicației.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(8), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 6, Name = "Implementare interfață utilizator", Description = "Implementare interfață utilizator pentru secțiunea X a proiectului.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(6), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 7, Name = "Testare integrare", Description = "Testare integrare a modulelor proiectului principal.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(9), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 8, Name = "Analiză cerințe", Description = "Analiză cerințe pentru viitoarele iterații ale proiectului.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(4), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 9, Name = "Optimizare bază de date", Description = "Optimizare performanță și structură în baza de date a proiectului.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(7), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 10, Name = "Raport progres săptămânal", Description = "Generare raport de progres pentru săptămâna curentă.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(5), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 11, Name = "Integrare servicii terțe", Description = "Integrare servicii terțe în cadrul proiectului.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(12), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 12, Name = "Documentare API", Description = "Documentare API pentru a fi folosit de dezvoltatori terți.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(8), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 13, Name = "Optimizare algoritmi", Description = "Optimizare algoritmi utilizați în cadrul proiectului principal.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(6), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 14, Name = "Implementare testare automată", Description = "Implementare testare automată pentru modulele cheie ale proiectului.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(11), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 15, Name = "Configurare servere de producție", Description = "Configurare servere pentru lansarea în producție a proiectului.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(9), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 16, Name = "Soluționare probleme de securitate", Description = "Soluționare probleme identificate de auditul de securitate.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(7), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 17, Name = "Creare instrumente de analiză", Description = "Creare instrumente de analiză pentru datele generate de proiect.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(14), UserId = student2.Id, StatusId = 1 },
                new TaskItem { Id = 18, Name = "Integrare cu platformă externă", Description = "Integrare cu o platformă externă pentru funcționalitate adițională.", ProjectId = new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), Deadline = DateTime.Now.AddDays(6), UserId = student3.Id, StatusId = 1 },
                new TaskItem { Id = 19, Name = "Testare securitate", Description = "Testare de securitate pentru identificarea vulnerabilităților.", ProjectId = new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), Deadline = DateTime.Now.AddDays(8), UserId = student1.Id, StatusId = 1 },
                new TaskItem { Id = 20, Name = "Refactorizare cod", Description = "Refactorizare cod pentru îmbunătățirea structurii și performanței.", ProjectId = new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), Deadline = DateTime.Now.AddDays(7), UserId = student2.Id, StatusId = 1 }
            );


            // difficulty levels
            List<Difficulty> difficulties = new List<Difficulty>()
            {
                new Difficulty() { Id = 1, Name = "Easy" },
                new Difficulty() { Id = 2, Name = "Moderate" },
                new Difficulty() { Id = 3, Name = "Intermediate" },
                new Difficulty() { Id = 4, Name = "Challenging" },
                new Difficulty() { Id = 5, Name = "Advanced" }
            };

            builder.Entity<Difficulty>().HasData(difficulties);

            //feedback
            builder.Entity<Feedback>().HasData(
                new Feedback { Id = 1, Description = "Feedback pentru implementarea funcționalității X.", Points = 8, UserId = student1.Id, TaskItemId = 1, DifficultyId = 2 },
                new Feedback { Id = 2, Description = "Feedback pentru testarea modulului Y.", Points = 9, UserId = student2.Id, TaskItemId = 2, DifficultyId = 3 },
                new Feedback { Id = 3, Description = "Feedback pentru documentarea proiectului.", Points = 7, UserId = student3.Id, TaskItemId = 3, DifficultyId = 1 },
                new Feedback { Id = 4, Description = "Feedback pentru soluționarea bug-urilor.", Points = 6, UserId = student1.Id, TaskItemId = 4, DifficultyId = 2 },
                new Feedback { Id = 5, Description = "Feedback pentru optimizarea performanței.", Points = 9, UserId = student2.Id, TaskItemId = 5, DifficultyId = 3 },
                new Feedback { Id = 6, Description = "Feedback pentru implementarea interfeței utilizator.", Points = 8, UserId = student3.Id, TaskItemId = 6, DifficultyId = 2 },
                new Feedback { Id = 7, Description = "Feedback pentru testarea integrării modulelor.", Points = 7, UserId = student1.Id, TaskItemId = 7, DifficultyId = 1 },
                new Feedback { Id = 8, Description = "Feedback pentru analiza cerințelor.", Points = 8, UserId = student2.Id, TaskItemId = 8, DifficultyId = 2 },
                new Feedback { Id = 9, Description = "Feedback pentru optimizarea bazei de date.", Points = 9, UserId = student3.Id, TaskItemId = 9, DifficultyId = 3 },
                new Feedback { Id = 10, Description = "Feedback pentru raportul de progres săptămânal.", Points = 7, UserId = student1.Id, TaskItemId = 10, DifficultyId = 1 }
            );

            // user badges
            builder.Entity<UserBadge>().HasData(
                new UserBadge { UserId = student1.Id, BadgeId = 1 },
                new UserBadge { UserId = student2.Id, BadgeId = 2 },
                new UserBadge { UserId = student3.Id, BadgeId = 3 },
                new UserBadge { UserId = student1.Id, BadgeId = 4 },
                new UserBadge { UserId = student2.Id, BadgeId = 5 },
                new UserBadge { UserId = student3.Id, BadgeId = 6 },
                new UserBadge { UserId = student2.Id, BadgeId = 1 }
            );

            // user roles
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id },
                new IdentityUserRole<string> { UserId = student1.Id, RoleId = studentRole.Id },
                new IdentityUserRole<string> { UserId = student2.Id, RoleId = studentRole.Id },
                new IdentityUserRole<string> { UserId = student3.Id, RoleId = studentRole.Id },
                new IdentityUserRole<string> { UserId = professor1.Id, RoleId = profRole.Id },
                new IdentityUserRole<string> { UserId = professor2.Id, RoleId = profRole.Id }
            );
        }
    }
}
