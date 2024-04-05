using VeterinarySystem.Core.Models.Animal;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAnimalService
	{
		Task AddNewAnimal(AnimalFormModel animalForm);

		Task<bool> AnimalExists(AnimalFormModel animalForm);

		Task EditAnimal(int id,AnimalFormModel animalForm);

		Task DeleteAnimal(int id);
	}
}
