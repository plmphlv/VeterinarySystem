using VeterinarySystem.Core.Models.AnimalOwner;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAnimalOwnerService
	{
		Task AddAnimalOwner(AnimalOwnerFormModel model);

		Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model);

		Task EditAnimalOwner(int id, AnimalOwnerFormModel model);

		Task DeleteAnimalOwner(int id);
	}
}
