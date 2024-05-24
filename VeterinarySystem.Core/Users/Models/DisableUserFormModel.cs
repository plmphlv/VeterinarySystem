using Common.Models;

namespace Users.User
{
    public class DisableUserFormModel
    {
        public string StaffMemberId { get; set; } = string.Empty;
        public ICollection<StaffServiceModel> Staff { get; set; } = null!;
    }
}
