using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Contracts
{
	public interface IProcedureService
	{
		Task<int> CreateNewProcetude(int id, ProcedureFormModel model);

		Task<ProcedureServiceModel> GetProcetudeDetails(int id);

		Task EditProcetude(ProcedureFormModel model, int id);
		Task<int> DeleteProcetude(int id);

		Task<bool> ProcedureExists(int id);

		Task<ICollection<StaffServiceModel>> GetStaffMembers();

		Task<ProcedureFormModel> GetEditingForm(int id);

		Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

		Task<ProcedureQueryServiceModel> GetProcedureHistory(int animalId, MedicalHistoryOrder order = MedicalHistoryOrder.Newest,
			int currentPage = 1,
			int proceduresPerPage = 1);
	}
}
