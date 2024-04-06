using VeterinarySystem.Core.Models.AnimalOwner;

namespace VeterinarySystem.Core.Contracts
{
    public interface IAnimalOwnerService
	{
		Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model);

		Task<bool> AnimalOwnerExists(int id);

		Task<AnimalOwnerDetailsModel> GetOwnerDetails(int id);

		Task<int> AddAnimalOwner(AnimalOwnerFormModel model);

		Task<AnimalOwnerFormModel> GetForm(int id);

		Task EditAnimalOwner(int id, AnimalOwnerFormModel model);

		Task DeleteAnimalOwner(int id);
	}
}
