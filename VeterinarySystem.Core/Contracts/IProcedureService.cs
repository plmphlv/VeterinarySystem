using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Contracts
{
	public interface IProcedureService
	{
		Task<int> CreateNewProcetude(int id, ProcedureFormModel model);

		Task<ProcedureServiceModel> GetProcetudeDetails(int id);

		Task EditProcetude(ProcedureFormModel model, int id);
		Task DeleteProcetude(int id);

		Task<bool> ProcedureExists(int id);

		Task<ICollection<StaffServiceModel>> GetStaffMembers();


	}
}
