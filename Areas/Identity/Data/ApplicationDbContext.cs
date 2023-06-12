using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;

namespace SEP.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Referee> Referees { get; set; }

    public DbSet<PartTimeHours> partTimeHours { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
		// Customize the ASP.NET Identity model and override the defaults if needed.
		// For example, you can rename the ASP.NET Identity table names and more.
		// Add your customizations after calling base.OnModelCreating(builder);
		base.OnModelCreating(builder);
	}

}
