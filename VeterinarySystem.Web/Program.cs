using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VeterinarySystem.Data.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace VeterinarySystem.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<VeterinarySystemDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddDefaultIdentity<StaffMember>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			})
			  .AddRoles<IdentityRole>()
			  .AddEntityFrameworkStores<VeterinarySystemDbContext>();

			builder.Services.AddTransient<IUserService, UserService>();

			builder.Services.AddTransient<IAnimalOwnerService, AnimalOwnerService>();

			builder.Services.AddTransient<IAnimalService, AnimalService>();

			builder.Services.AddTransient<IAppointmentService, AppointmentService>();

			builder.Services.AddTransient<IProcedureService, ProcedureService>();

			builder.Services.AddTransient<IPrescriptionService, PrescriptionService>();

			builder.Services.AddControllersWithViews(options =>
			{
				options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
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
				name: "Admin",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapRazorPages();

			app.SeedAdmin();

			app.Run();
		}
	}
}
