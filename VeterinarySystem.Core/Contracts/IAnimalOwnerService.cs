using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Models.Common;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAnimalOwnerService
	{
		Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model);

		Task<bool> AnimalOwnerExists(int id);

		Task<OwnerServiceModel> GetOwnerDetails(int id);

		Task<int> AddAnimalOwner(AnimalOwnerFormModel model);

		Task<AnimalOwnerFormModel> GetEditingForm(int id);

		Task EditAnimalOwner(int id, AnimalOwnerFormModel model);

		Task DeleteAnimalOwner(int id);

		Task<DeleteViewModel> GetDeleteModel(int id);

		Task<OwnerQueryModel> Search(string searchTerm, SearchParameter parameter = SearchParameter.FullName, int pageSize = 5, int currentPage = 1);

		Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);
	}
}
