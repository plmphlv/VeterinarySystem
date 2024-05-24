using Animal.Animal;
using Common.Common;

namespace Animal.Contracts
{
    public interface IAnimalService
    {
        Task<int> AddNewAnimal(AnimalFormModel animalForm, int ownerId);//C

        Task<AnimalServiceModel> GetAnimalDetails(int id);//R

        Task EditAnimal(int id, AnimalFormModel animalForm);//U

        Task DeleteAnimal(int id);//D

        Task<bool> AnimalExists(int id);

        Task<bool> AnimalExists(AnimalFormModel animalForm, int ownerId);

        Task<ICollection<AnimalTypesServiceModels>> AllAnimalTypes();

        Task<int> GetAnimalTypeById(int animalId);

        Task<int> GetOwnerByPetId(int animalId);

        Task<AnimalFormModel> GetAnimalEditingForm(int animalId);

        Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

        Task AddNewAnimalType(AnimalTypeFormModel form);

        Task DeleteAnimalType(AnimalTypeDeleteFormModel form);
    }
}
