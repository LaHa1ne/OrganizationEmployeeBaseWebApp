using Microsoft.EntityFrameworkCore;
using OrganizationEmployeeBaseWebApp.DataAccessLayer;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.Repositories;
using OrganizationEmployeeBaseWebApp.DataLayer.Helpers;
using OrganizationEmployeeBaseWebApp.MapperProfiles.Employees;
using OrganizationEmployeeBaseWebApp.MapperProfiles.Organizations;
using OrganizationEmployeeBaseWebApp.Services.Interfaces;
using OrganizationEmployeeBaseWebApp.Services.Services;

namespace OrganizationEmployeeBaseWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connection, x => x.UseDateOnlyTimeOnly()));

            builder.Services.AddTransient<IOrganizationService, OrganizationService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();

            builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddAutoMapper(
                typeof(CreateOrganizationToOrganizationDTO),
                typeof(EditOrganizationToOrganizationDTO),
                typeof(OrganizationToOrganizationDTO),
                typeof(CreateEmployeeViewModelToEmployeeDTO),
                typeof(EditEmployeeViewModelToEmployeeDTO),
                typeof(EmployeeToEmployeeDTO),
                typeof(EmployeeCSVToEmployee)
                );

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            app.Run();
        }
    }
}