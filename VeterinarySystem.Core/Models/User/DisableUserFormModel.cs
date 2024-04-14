using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Models.User
{
	public class DisableUserFormModel
	{
		public string StaffMemberId { get; set; } = string.Empty;
		public ICollection<StaffServiceModel> Staff { get; set; } = null!;
	}
}
