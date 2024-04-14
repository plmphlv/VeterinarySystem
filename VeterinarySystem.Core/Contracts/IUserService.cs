using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Core.Models.User;
using static System.Runtime.InteropServices.JavaScript.JSType;
using VeterinarySystem.Data.DataSeeding.Admin;

namespace VeterinarySystem.Core.Contracts
{
	public interface IUserService
	{
		Task<IdentityResult?> Register(RegisterUserModel model);

		Task Login(LoginUserModel model);

		Task Edit(EditUserModel model);

		Task Delete(string id);

		Task EditRoles(UserRoleModel model);

		Task<EditUserModel> GetEditModel(string id);

		Task<UserProfileModel> GetUserProfile(string userId);

		Task<(UserRoleModel user, IEnumerable<SelectListItem> roles)> GetUserWithRoles(string id);

		Task<ICollection<StaffServiceModel>> GetStaffMembers();

	}
}
