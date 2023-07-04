using Microsoft.AspNetCore.Identity;
using SEP.Models.DomainModels;

namespace SEP.Data
{
    public static class TaskInitializer
    {

        
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            IList<PartTimeHours> partTimeHours = new List<PartTimeHours>
                {
                    new PartTimeHours { TimeRange = " < 2 " },
                    new PartTimeHours { TimeRange = "2 - 4" },
                    new PartTimeHours { TimeRange = "4 - 6" },
                    new PartTimeHours { TimeRange = "6 - 8" },
                    new PartTimeHours { TimeRange = "8 - 12" },
                    new PartTimeHours { TimeRange = "> 12" }
                };
            foreach (var hour in partTimeHours)
            {
                if (!context.partTimeHours.Any(f => f.TimeRange == hour.TimeRange))
                {
                    context.partTimeHours.Add(hour);
                }
            }
            context.SaveChanges();

            //facultiess
            IList<Faculty> faculties = new List<Faculty>
                {
                    new Faculty { FacultyName = "Commerce, Law and Management" },
                    new Faculty { FacultyName = "Engineering and the Built Environment" },
                    new Faculty { FacultyName = "Health Sciences" },
                    new Faculty { FacultyName = "Humanities" },
                    new Faculty { FacultyName = "Science" }
                };
            foreach (var faculty in faculties)
            {
                if (!context.Faculties.Any(f => f.FacultyName == faculty.FacultyName))
                {
                    context.Faculties.Add(faculty);
                }
            }
            context.SaveChanges();

            // Departments
            IList<Department> departments = new List<Department>
                {
                    new Department { DepartmentName = "Accountancy", FacultyId = 1 },
                    new Department { DepartmentName = "Business Sciences", FacultyId = 1 },
                    new Department { DepartmentName = "Economics and Finance", FacultyId = 1 },
                    new Department { DepartmentName = "Law", FacultyId = 1 },
                    new Department { DepartmentName = "Wits Business School", FacultyId = 1 },
                    new Department { DepartmentName = "Wits School of Governance", FacultyId = 1 },
                    new Department { DepartmentName = "Architecture and Planning", FacultyId = 2 },
                    new Department { DepartmentName = "Civil & Environmental Engineering", FacultyId = 2 },
                    new Department { DepartmentName = "Chemical & Metallurgical Engineering", FacultyId = 2 },
                    new Department { DepartmentName = "Construction Economics & Management", FacultyId = 2 },
                    new Department { DepartmentName = "Electrical & Information Engineering", FacultyId = 2 },
                    new Department { DepartmentName = "Mechanical, Industrial & Aeronautical Engineering", FacultyId = 2 },
                    new Department { DepartmentName = "Mining Engineering", FacultyId = 2 },
                    new Department { DepartmentName = "Anatomical Sciences", FacultyId = 3 },
                    new Department { DepartmentName = "Clinical Medicine", FacultyId = 3 },
                    new Department { DepartmentName = "Oral Health Sciences", FacultyId = 3 },
                    new Department { DepartmentName = "Pathology", FacultyId = 3 },
                    new Department { DepartmentName = "Physiology", FacultyId = 3 },
                    new Department { DepartmentName = "Public Health", FacultyId = 3 },
                    new Department { DepartmentName = "Therapeutic Sciences", FacultyId = 3 },
                    new Department { DepartmentName = "Wits School of Arts", FacultyId = 4 },
                    new Department { DepartmentName = "Wits School of Education", FacultyId = 4 },
                    new Department { DepartmentName = "Human and Community Development", FacultyId = 4 },
                    new Department { DepartmentName = "Literature, Language and Media", FacultyId = 4 },
                    new Department { DepartmentName = "Social Sciences", FacultyId = 4 },
                    new Department { DepartmentName = "Animal, Plant and Environmental Sciences", FacultyId = 5 },
                    new Department { DepartmentName = "Chemistry", FacultyId = 5 },
                    new Department { DepartmentName = "Computer Science and Applied Mathematics", FacultyId = 5 },
                    new Department { DepartmentName = "Geography, Archaeology and Environmental Sciences", FacultyId = 5 },
                    new Department { DepartmentName = "Geosciences", FacultyId = 5 },
                    new Department { DepartmentName = "Mathematics", FacultyId = 5 },
                    new Department { DepartmentName = "Molecular and Cell Biology", FacultyId = 5 },
                    new Department { DepartmentName = "Physics", FacultyId = 5 },
                    new Department { DepartmentName = "Statistics and Actuarial Science", FacultyId = 5 }
                };
            foreach (var dep in departments)
            {
                if (!context.Departments.Any(d => d.DepartmentName == dep.DepartmentName))
                {
                    context.Departments.Add(dep);
                }
            }
            context.SaveChanges();

            //seed roles
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Approver", "Employer", "Student" };

            foreach (var role in roles)
            {
                //we want to add the roles to the system - only if role does not already exist in system
                //ensure main task is async*
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            //seed admin
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "admin",
                    LastName = "admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "123Pa$$321");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            //seed approver
            if (userManager.FindByEmailAsync("approver0@gmail.com").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "approver0@gmail.com",
                    Email = "approver0@gmail.com",
                    FirstName = "approver",
                    LastName = "approver",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "123Pa$app");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Approver").Wait();
                }
            }

            //seed faker data
            var dataGenerator = scope.ServiceProvider.GetService<DataGenerator>();
            var fakePosts = dataGenerator.GeneratePosts().Take(100);
            var existingPosts = context.Posts.Select(p => p.PostId).ToList();
            var newFakePosts = fakePosts.Where(p => !existingPosts.Contains(p.PostId)).ToList();
            context.Posts.AddRange(newFakePosts);
            context.SaveChanges();

            return app;
        }
    }
}