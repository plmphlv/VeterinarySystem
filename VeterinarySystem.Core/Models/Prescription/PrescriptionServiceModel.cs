namespace VeterinarySystem.Core.Models.Prescription
{
	public class PrescriptionServiceModel
	{
		public int Id { get; set; }
		public string Number { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string IssueDate {  get; set; } = string.Empty;
		public int AnimalId { get; set; }
		public string AnimalName { get; set;} = string.Empty;
		public string StaffMemberFullName {  get; set; } = string.Empty;
	}
}
