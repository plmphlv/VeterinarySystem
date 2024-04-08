using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class AnimalService : IAnimalService
	{
		private readonly VeterinarySystemDbContext data;

		public AnimalService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public async Task<ICollection<AnimalTypesServiceModels>> AllAnimalTypes()
		{
			ICollection<AnimalTypesServiceModels> names = await data.AnimalTypes
				.Select(c => new AnimalTypesServiceModels()
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToListAsync();

			return names;
		}

		public async Task<bool> AnimalExists(AnimalFormModel animalForm, int ownerId)
		{
			bool result = await data.Animals
				.AnyAsync(animal =>
			animal.Name == animalForm.Name &&
			animal.AnimalTypeId == animalForm.AnimalTypeId &&
			animal.AnimalOwnerId == ownerId);

			return result;
		}

		public async Task<bool> AnimalExists(int id)
		{
			bool result = await data.Animals
				.AnyAsync(animal => animal.Id == id);

			return result;
		}

		public async Task<int> AddNewAnimal(AnimalFormModel animalForm, int ownerId)
		{
			Animal animal = new Animal()
			{
				Weight = animalForm.Weight,
				Age = animalForm.Age,
				AnimalTypeId = animalForm.AnimalTypeId,
				AnimalOwnerId = ownerId
			};

			if (!animalForm.Name.IsNullOrEmpty())
			{
				animal.Name = "No name was given";
			}

			await data.Animals.AddAsync(animal);
			await data.SaveChangesAsync();

			return animal.Id;
		}

		public async Task DeleteAnimal(int id)
		{
			Animal animal = await data.Animals.FirstOrDefaultAsync(animal => animal.Id == id);

			data.Animals.Remove(animal);
			await data.SaveChangesAsync();
		}

		public async Task EditAnimal(int id, AnimalFormModel animalForm)
		{
			Animal animal = await data.Animals.FirstOrDefaultAsync(animal => animal.Id == id);

			if (animal is null)
			{
				throw new ArgumentNullException(nameof(animal));
			}

			if (animalForm.Name.IsNullOrEmpty())
			{
				animal.Name = "No name was given";
			}
			else
			{
				animal.Name = animalForm.Name;
			}

			animal.Age = animalForm.Age;
			animal.Weight = animalForm.Weight;
			animal.AnimalTypeId = animalForm.AnimalTypeId;

			await data.SaveChangesAsync();
		}

		public async Task<AnimalServiceModel> GetAnimalDetails(int id)
		{
			AnimalServiceModel animal = await data.Animals.Select(animal => new AnimalServiceModel()
			{
				Id = animal.Id,
				Name = animal.Name,
				Weight = animal.Weight,
				Age = animal.Age,
				AnimalTypeName = animal.AnimalType.Name,
				OwnerId = animal.AnimalOwnerId,
				OwnerFullName = $"{animal.AnimalOwner.FirstName} {animal.AnimalOwner.LastName}"
			})
				.FirstOrDefaultAsync(animal => animal.Id == id);

			return animal;
		}

		public async Task<int> GetAnimalTypeId(int animalId)
		{
			int animalTypeId = (await data.Animals.FindAsync(animalId)).AnimalTypeId;

			return animalTypeId;
		}

		public async Task<int> GetOwnerByPetId(int animalId)
		{
			int animalTypeId = (await data.Animals.FindAsync(animalId)).AnimalOwnerId;

			return animalTypeId;
		}

		public async Task<AnimalFormModel> GetAnimalForm(int animalId)
		{
			AnimalFormModel? animal = await data.Animals
				.Where(animal => animal.Id == animalId
				).Select(animal => new AnimalFormModel()
				{
					Name = animal.Name,
					Age = animal.Age,
					Weight = animal.Weight,
				}).FirstOrDefaultAsync();

			
				//AnimalTypeId = animalTypeId,
				//AnimalTypes = await animalService.AllAnimalTypes()
			

			return animal;
		}
	}
}
