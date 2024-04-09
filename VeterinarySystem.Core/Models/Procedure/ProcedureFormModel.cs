using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Models.Procedure
{
	public class ProcedureFormModel
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public DateTime Date { get; set; }

		public int AnimalId { get; set; }

		public string StaffMemberId { get; set; } = null!;

		public ICollection<StaffServiceModel> Staff { get; set; } = null!;
	}
}