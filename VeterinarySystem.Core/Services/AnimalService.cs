using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.Common;
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
				.AsNoTracking()
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
				.AsNoTracking()
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
			Animal? animal = await data.Animals
				.FirstOrDefaultAsync(animal => animal.Id == id);

			if (animal is null)
			{
				throw new NullReferenceException();
			}

			data.Animals.Remove(animal);
			await data.SaveChangesAsync();
		}

		public async Task EditAnimal(int id, AnimalFormModel animalForm)
		{
			Animal? animal = await data.Animals
				.FirstOrDefaultAsync(animal => animal.Id == id);

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
			AnimalServiceModel? animal = await data.Animals
				.AsNoTracking()
				.Where(animal => animal.Id == id)
				.Select(animal => new AnimalServiceModel()
				{
					Id = animal.Id,
					AnimalName = animal.Name,
					Weight = animal.Weight,
					Age = animal.Age,
					AnimalTypeName = animal.AnimalType.Name,
					OwnerFullName = $"{animal.AnimalOwner.FirstName} {animal.AnimalOwner.LastName}"
				})
				.FirstOrDefaultAsync();

			if (animal is null)
			{
				throw new NullReferenceException();
			}

			return animal;
		}

		public async Task<int> GetAnimalTypeById(int animalId)
		{
			int typeId = await data.Animals
				.AsNoTracking()
				.Where(animal => animal.Id == animalId)
				.Select(animal => animal.AnimalTypeId)
				.FirstOrDefaultAsync();

			return typeId;
		}

		public async Task<int> GetOwnerByPetId(int animalId)
		{
			int ownerId = await data.Animals
				.AsNoTracking()
				.Where(animal => animal.Id == animalId)
				.Select(animal => animal.AnimalOwnerId)
				.FirstOrDefaultAsync();

			return ownerId;
		}

		public async Task<AnimalFormModel> GetAnimalEditingForm(int animalId)
		{
			AnimalFormModel? animal = await data.Animals
				.AsNoTracking()
				.Where(animal => animal.Id == animalId
				).Select(animal => new AnimalFormModel()
				{
					Name = animal.Name,
					Age = animal.Age,
					Weight = animal.Weight,
					AnimalTypeId = animal.AnimalTypeId,

				}).FirstOrDefaultAsync();

			if (animal is null)
			{
				throw new NullReferenceException();
			}

			return animal;
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.Animals
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Description = e.Name,
					Controller = controllerName
				}
			).FirstOrDefaultAsync();

			if (model is null)
			{
				throw new NullReferenceException();
			}

			return model;
		}

		public async Task AddNewAnimalType(AnimalTypeFormModel form)
		{
			await data.AnimalTypes.AddAsync(new AnimalType()
			{
				Name = form.TypeName
			});

			await data.SaveChangesAsync();
		}

		public async Task DeleteAnimalType(AnimalTypeDeleteFormModel form)
		{
			AnimalType? type = await data.AnimalTypes.FirstOrDefaultAsync(aT => aT.Id == form.TypeId);

			if (type is null)
			{
				throw new NullReferenceException();
			}

			data.AnimalTypes.Remove(type);
			await data.SaveChangesAsync();
		}
	}
}
