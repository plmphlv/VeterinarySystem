using VeterinarySystem.Core.Models.Animal;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAnimalService
	{
		Task<int> AddNewAnimal(AnimalFormModel animalForm, int ownerId);

		Task<bool> AnimalExists(AnimalFormModel animalForm, int ownerId);

		Task<bool> AnimalExists(int id);

		Task<AnimalServiceModel> GetAnimalDetails(int id);

		Task EditAnimal(int id,AnimalFormModel animalForm);

		Task DeleteAnimal(int id);

		Task<ICollection<AnimalTypesServiceModels>> AllAnimalTypes();
	}
}
