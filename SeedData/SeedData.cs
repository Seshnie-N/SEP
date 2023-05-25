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
				try
				{
					
					//seed data

					//Faculties
					//context.Faculties.AddRange(
					//	new Faculty { facultyId = 1, facultyName = "Commerce, Law and Management" }
					//);

					//Departments
					//.context.Departments.AddRange(
					//	new Department { departmentId=1, departmentName="Accountancy", FacultyId = 1 }
					//);

					IList<Faculty> faculties = new List<Faculty>();
					faculties.Add(new Faculty { facultyName = "Commerce, Law and Management" });
					context.Faculties.AddRange(faculties);
					context.SaveChanges();
					
					// Departments
					IList<Department> departments = new List<Department>();
					departments.Add(new Department { departmentName = "Accountancy", FacultyId = 1 });
					context.Departments.AddRange(departments);
					context.SaveChanges();
				}
				catch (Exception)
				{

					throw;
				}

				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				try
				{
					var roles = new[] { "Admin", "Approver", "Employer", "Student" };

					foreach (var role in roles)
					{
						//we want to add the roles to the system - only if role does not already exist in system
						//ensure main task is async*
						if (!await roleManager.RoleExistsAsync(role))
							await roleManager.CreateAsync(new IdentityRole(role));
					}
				} catch (Exception ex)
				{
					throw;
				}

				var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
				try
				{
					if ( userManager.FindByEmailAsync("admin@gmail.com").Result==null)
					{
						ApplicationUser user = new ApplicationUser
						{
							Email = "admin@gmail.com",
							FirstName = "admin",
							LastName = "admin",
							UserName = "admin"
						};

						var result = userManager.CreateAsync(user);

						if (result.IsCompletedSuccessfully)
						{
							userManager.AddToRoleAsync(user, "Admin").Wait();
						}


						
					}
				}
				catch (Exception ex)
				{

					throw;
				}

				return app;
			}
		}
	}
    
}
