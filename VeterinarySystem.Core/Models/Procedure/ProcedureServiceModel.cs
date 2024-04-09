namespace VeterinarySystem.Core.Models.Procedure
{
	public class ProcedureServiceModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Date { get; set; } = string.Empty;

		public string AnimalName { get; set; } = string.Empty;

		public string StaffMemberFullName { get; set; } = string.Empty;
	}
}
