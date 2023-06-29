using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEP.CustomValidation;
using SEP.Data;
using SEP.Areas.Identity.Data;
using SEP.Mapper;
using SEP.Models;
using SEP.Models.DomainModels;
using SEP.Models.FrontEndModels;
using SEP.SeedData;


namespace SEP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            //var mappingConfig = new MapperConfiguration(cfg =>
            //{ 
            //    cfg.AddProfile(new AutoMapperProfile());
            //});
            //var mapper = mappingConfig.CreateMapper();
            //builder.Services.AddSingleton(mapper);

            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<DuplicateUserValidator>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // add mapper service
            builder.Services.AddAutoMapper(typeof(Program));

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            //configure default password validation
            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireUppercase = false;
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
          
            await app.SeedDataAsync();

            app.Run();
        }

    }
}