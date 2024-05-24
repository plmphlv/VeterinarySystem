using Animal;
using Animal.Contracts;
using AnimalOwner.Contracts;
using Appointments;
using Appointments.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prescriptions;
using Prescriptions.Contracts;
using Procedures;
using Procedures.Contracts;
using Users;
using Users.Contracts;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;
using VeterinarySystem.Data.Infrastructure;

namespace VeterinarySystem.Web
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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

			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddScoped<IAnimalOwnerService, AnimalOwnerService>();

			builder.Services.AddScoped<IAnimalService, AnimalService>();

			builder.Services.AddScoped<IAppointmentService, AppointmentService>();

			builder.Services.AddScoped<IProcedureService, ProcedureService>();

			builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

			builder.Services.AddControllersWithViews(options =>
			{
				options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
			});

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.Cookie.Name = "UserAccess";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(30);
				options.LoginPath = "/User/Login";
				options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
				options.SlidingExpiration = true;
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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "AnimalOwner Details",
					pattern: "/AnimalOwner/Details/{id}/{information}",
					defaults: new { Controller = "AnimalOwner", Action = "Details" }
					);
				endpoints.MapControllerRoute(
					name: "Animal Details",
					pattern: "/Animal/Details/{id}/{information}",
					defaults: new { Controller = "Animal", Action = "Details" }
					);
				endpoints.MapControllerRoute(
					name: "Appointment Details",
					pattern: "/Appointment/Details/{id}/{information}",
					defaults: new { Controller = "Appointment", Action = "Details" }
					);
				endpoints.MapControllerRoute(
					name: "Prescription Details",
					pattern: "/Prescription/Details/{id}/{information}",
					defaults: new { Controller = "Prescription", Action = "Details" }
					);
				endpoints.MapControllerRoute(
					name: "Procedure Details",
					pattern: "/Procedure/Details/{id}/{information}",
					defaults: new { Controller = "Procedure", Action = "Details" }
					);
			});

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
