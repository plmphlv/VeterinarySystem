using AnimalOwner.AnimalOwner;
using Common.Common;
using Common.Infrastructure;

namespace AnimalOwner.Contracts
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

        Task<OwnerQueryModel> Search(string searchTerm, OwnerSearchParameters parameter = OwnerSearchParameters.FullName, int pageSize = 5, int currentPage = 1);

        Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);
    }
}
