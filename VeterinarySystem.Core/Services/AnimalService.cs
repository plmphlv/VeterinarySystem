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

		public async Task<bool> AnimalExists(AnimalFormModel animalForm)
		{
			bool result = await data.Animals.AnyAsync(animal =>
			animal.Name == animalForm.Name &&
			animal.Weight == animalForm.Weight &&
			animal.AnimalTypeId == animalForm.AnimalTypeId &&
			animal.AnimalOwnerId == animalForm.AnimalOwnerId);

			return result;
		}

		public async Task AddNewAnimal(AnimalFormModel animalForm)
		{
			Animal animal = new Animal()
			{
				Weight = animalForm.Weight,
				AnimalTypeId = animalForm.AnimalTypeId,
				AnimalOwnerId = animalForm.AnimalOwnerId
			};

			if (!animalForm.Name.IsNullOrEmpty())
			{
				animal.Name = animalForm.Name;
			}

			await data.Animals.AddAsync(animal);
			await data.SaveChangesAsync();
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

			animal.Weight = animalForm.Weight;
			animal.AnimalTypeId = animalForm.AnimalTypeId;
			animal.AnimalOwnerId = animalForm.AnimalOwnerId;


			if (!animalForm.Name.IsNullOrEmpty())
			{
				animal.Name = animalForm.Name;
			}


			await data.SaveChangesAsync();
		}
	}
}
