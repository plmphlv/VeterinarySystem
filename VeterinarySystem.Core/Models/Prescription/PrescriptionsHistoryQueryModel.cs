using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Models.Prescription
{
	public class PrescriptionsHistoryQueryModel
	{
        public int AnimalId { get; set; }

		public MedicalHistoryOrder Order { get; set; } = MedicalHistoryOrder.Newest;

		public int CurrentPage { get; set; } = 1;

		public string AnimalName { get; set; } =string.Empty;

		public const int PrescriptionsPerPage = 8; 

		public int TotalPrescriptions { get; set; }

		public int TotalPages { get; set; }

		public ICollection<PrescriptionServiceModel> Prescriptions { get; set; } = new List<PrescriptionServiceModel>();
	}
}
