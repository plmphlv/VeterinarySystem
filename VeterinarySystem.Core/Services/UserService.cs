using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Core.Models.User;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class UserService : IUserService
	{
		private readonly VeterinarySystemDbContext data;
		private readonly UserManager<StaffMember> userManager;
		private readonly SignInManager<StaffMember> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserService(VeterinarySystemDbContext _context,
			UserManager<StaffMember> _userManager,
			SignInManager<StaffMember> _signInManager,
			RoleManager<IdentityRole> _roleManager)
		{
			data = _context;
			userManager = _userManager;
			signInManager = _signInManager;
			roleManager = _roleManager;
		}

		public async Task<IdentityResult?> Register(RegisterUserModel model)
		{
			StaffMember staffMember = new StaffMember()
			{
				UserName = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email
			};

			IdentityResult? result = await userManager.CreateAsync(staffMember, model.Password);

			return result;
		}

		public async Task Login(LoginUserModel model)
		{
			StaffMember? user = await userManager.FindByEmailAsync(model.Email);

			if (user is null || user.IsDisabled is true)
			{
				throw new ArgumentException();
			}

			bool isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);

			if (!isPasswordValid)
			{
				throw new ArgumentException();
			}

			await signInManager.SignInAsync(user, isPasswordValid);
		}

		public async Task Edit(EditUserModel model)
		{
			StaffMember? staffMember = await data.Users.FirstOrDefaultAsync(user => user.Id == model.Id);

			if (staffMember is null)
			{
				throw new NullReferenceException();
			}

			staffMember.FirstName = model.FirstName;
			staffMember.LastName = model.LastName;
			staffMember.Email = model.Email;

			await data.SaveChangesAsync();
		}

		public async Task Delete(string id)
		{
			StaffMember? staffMember = await data.Users
				.FirstOrDefaultAsync(user => user.Id == id);

			if (staffMember is null)
			{
				throw new NullReferenceException();
			}

			staffMember.IsDisabled = true;
			await data.SaveChangesAsync();
		}

		public async Task EditRoles(UserRoleModel model)
		{
			StaffMember? user = await data.Users.FirstOrDefaultAsync(user => user.Id == model.Id);

			if (user is null)
			{
				throw new NullReferenceException();
			}

			IEnumerable<string> userRoles = await userManager.GetRolesAsync(user);

			await userManager.RemoveFromRolesAsync(user, userRoles);

			if (model.RoleNames.Count() > 0)
			{
				await userManager.AddToRolesAsync(user, model.RoleNames);
			}
		}

		public async Task<EditUserModel> GetEditModel(string id)
		{
			EditUserModel? model = await data.Users
				.AsNoTracking()
				.Where(user => user.Id == id)
				.Select(
				user => new EditUserModel()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email
				}).FirstOrDefaultAsync();

			if (model is null)
			{
				throw new NullReferenceException();
			}

			return model;
		}

		public async Task<UserProfileModel> GetUserProfile(string userId)
		{
			UserProfileModel? staffMember = await data.Users
				.AsNoTracking()
				.Where(user => user.Id == userId)
				.Select(user => new UserProfileModel()
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email

				})
				.FirstOrDefaultAsync();

			if (staffMember is null)
			{
				throw new NullReferenceException();
			}

			return staffMember;
		}

		public async Task<(UserRoleModel user, IEnumerable<SelectListItem> roles)> GetUserWithRoles(string id)
		{
			StaffMember? user = await data.Users.FirstOrDefaultAsync(user => user.Id == id);

			if (user is null)
			{
				throw new NullReferenceException();
			}

			UserRoleModel model = new UserRoleModel()
			{
				Id = user.Id,
				Name = $"{user.FirstName} {user.LastName}",
			};

			IEnumerable<SelectListItem> roles = roleManager.Roles
				.ToList()
				.Select(r => new SelectListItem()
				{
					Text = r.Name,
					Value = r.Name,
					Selected = userManager.IsInRoleAsync(user, r.Name).Result
				})
				.ToArray();

			return (model, roles);
		}

		public async Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			ICollection<StaffServiceModel> staff = await data.Users
				.AsNoTracking()
				.Where(u => u.Email != AdminUser.AdminEmail && u.IsDisabled == false)
				.Select(u => new StaffServiceModel()
				{
					StaffId = u.Id,
					StaffName = $"{u.FirstName} {u.LastName}"
				})
				.ToListAsync();

			return staff;
		}
	}
}
