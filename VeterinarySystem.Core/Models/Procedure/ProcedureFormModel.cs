namespace VeterinarySystem.Core.Models.Procedure
{
	public class ProcedureFormModel
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = string.Empty;

		public int AnimalId { get; set; }

		public bool IsMedical { get; set; }

		public string StaffMemberId { get; set; } = null!;
	}
}