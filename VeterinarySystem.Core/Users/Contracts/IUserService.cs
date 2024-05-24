using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Users.User;

namespace Users.Contracts
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

        Task<string> GetUserLastName(string userId);
    }
}
