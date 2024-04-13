namespace VeterinarySystem.Core.Models.Prescription
{
	public class PrescriptionsQueryServiceModel
	{
		public int TotalPrescriptionsCount { get; set; }
		public int TotalPages { get; set; }
		public ICollection<PrescriptionServiceModel> Prescriptions { get; set; } = new List<PrescriptionServiceModel>();
	}
}
