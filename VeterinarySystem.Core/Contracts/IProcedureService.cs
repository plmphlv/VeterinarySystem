using VeterinarySystem.Core.Models.Procedure;

namespace VeterinarySystem.Core.Contracts
{
	public interface IProcedureService
	{
		Task CreateNewProcetude(ProcedureFormModel model);

		Task EditProcetude(ProcedureFormModel model);

		Task EditProcetude(int id);
	}
}
