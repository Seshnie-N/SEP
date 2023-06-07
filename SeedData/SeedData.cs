using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using System.Security.Cryptography.Xml;

namespace SEP.SeedData
{
    public static class TaskInitializer
    {
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                IList<PartTimeHours> partTimeHours = new List<PartTimeHours>();
                partTimeHours.Add(new PartTimeHours { timeRange = " < 2 " });
                partTimeHours.Add(new PartTimeHours { timeRange = "2 - 4" });
                partTimeHours.Add(new PartTimeHours { timeRange = "4 - 6" });
                partTimeHours.Add(new PartTimeHours { timeRange = "6 - 8" });
                partTimeHours.Add(new PartTimeHours { timeRange = "8 - 12" });
                partTimeHours.Add(new PartTimeHours { timeRange = "> 12" });
                foreach (var hour in partTimeHours)
                {
                    if (!context.partTimeHours.Any(f => f.timeRange == hour.timeRange))
                    {
                        context.partTimeHours.Add(hour);
                    }
                }
                context.SaveChanges();

                //facultiess
                IList<Faculty> faculties = new List<Faculty>();
                faculties.Add(new Faculty { facultyName = "Commerce, Law and Management" });
                faculties.Add(new Faculty { facultyName = "Engineering and the Built Environment" });
                faculties.Add(new Faculty { facultyName = "Health Sciences" });
                faculties.Add(new Faculty { facultyName = "Humanities" });
                faculties.Add(new Faculty { facultyName = "Science" });
                foreach (var faculty in faculties)
                {
                    if (!context.Faculties.Any(f => f.facultyName == faculty.facultyName))
                    {
                        context.Faculties.Add(faculty);
                    }
                }
                context.SaveChanges();



                // Departments
                IList<Department> departments = new List<Department>();
                departments.Add(new Department { departmentName = "Accountancy", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Business Sciences", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Economics and Finance", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Law", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Wits Business School", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Wits School of Governance", FacultyId = 1 });
                departments.Add(new Department { departmentName = "Architecture and Planning", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Civil & Environmental Engineering", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Chemical & Metallurgical Engineering", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Construction Economics & Management", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Electrical & Information Engineering", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Mechanical, Industrial & Aeronautical Engineering", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Mining Engineering", FacultyId = 2 });
                departments.Add(new Department { departmentName = "Anatomical Sciences", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Clinical Medicine", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Oral Health Sciences", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Pathology", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Physiology", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Public Health", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Therapeutic Sciences", FacultyId = 3 });
                departments.Add(new Department { departmentName = "Wits School of Arts", FacultyId = 4 });
                departments.Add(new Department { departmentName = "Wits School of Education", FacultyId = 4 });
                departments.Add(new Department { departmentName = "Human and Community Development", FacultyId = 4 });
                departments.Add(new Department { departmentName = "Literature, Language and Media", FacultyId = 4 });
                departments.Add(new Department { departmentName = "Social Sciences", FacultyId = 4 });
                departments.Add(new Department { departmentName = "Animal, Plant and Environmental Sciences", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Chemistry", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Computer Science and Applied Mathematics", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Geography, Archaeology and Environmental Sciences", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Geosciences", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Mathematics", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Molecular and Cell Biology", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Physics", FacultyId = 5 });
                departments.Add(new Department { departmentName = "Statistics and Actuarial Science", FacultyId = 5 });
                foreach (var dep in departments)
                {
                    if (!context.Departments.Any(d => d.departmentName == dep.departmentName))
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
                    ApplicationUser user = new ApplicationUser
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
				// userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
				if (userManager.FindByEmailAsync("approver0@gmail.com").Result == null)
				{
					ApplicationUser user = new ApplicationUser
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

				return app;
			}
		}
	}
}
