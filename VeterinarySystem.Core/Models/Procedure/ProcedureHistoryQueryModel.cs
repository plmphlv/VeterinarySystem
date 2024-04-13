using VeterinarySystem.Core.Infrastructure;

namespace VeterinarySystem.Core.Models.Procedure
{
	public class ProcedureHistoryQueryModel
	{
		public int AnimalId { get; set; }

		public MedicalHistoryOrder Order { get; set; } = MedicalHistoryOrder.Newest;

		public int CurrentPage { get; set; } = 1;

		public string AnimalName { get; set; } = string.Empty;

		public const int ProceduresPerPage = 8;

		public int TotalProcedures { get; set; }

		public int TotalPages { get; set; }

		public ICollection<ProcedureServiceModel> Procedures { get; set; } = new List<ProcedureServiceModel>();
	}
}
