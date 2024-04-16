using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Contracts
{
	public interface IProcedureService
	{
		Task<int> CreateNewProcedude(int id, ProcedureFormModel model);

		Task<ProcedureServiceModel> GetProcedudeDetails(int id);

		Task EditProcedude(ProcedureFormModel model, int id);
		Task<int> DeleteProcedude(int id);

		Task<bool> ProcedureExists(int id);

		Task<ICollection<StaffServiceModel>> GetStaffMembers();

		Task<ProcedureFormModel> GetEditingForm(int id);

		Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

		Task<ProcedureQueryServiceModel> GetProcedureHistory(int animalId, MedicalHistoryOrder order = MedicalHistoryOrder.Newest,
			int currentPage = 1,
			int proceduresPerPage = 1);
	}
}
