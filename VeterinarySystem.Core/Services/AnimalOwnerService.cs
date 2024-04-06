using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class AnimalOwnerService : IAnimalOwnerService
	{
		private readonly VeterinarySystemDbContext data;

		public AnimalOwnerService(VeterinarySystemDbContext dbContext)
		{
			data = dbContext;
		}

		public async Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model)
		{
			bool result = await data.AnimalOwners.AnyAsync(owner => owner.FirstName == model.FirstName && owner.LastName == model.LastName && owner.PhoneNumber == model.PhoneNumber);

			return result;
		}

		public async Task<bool> AnimalOwnerExists(int id)
		{
			bool result = await data.AnimalOwners.AnyAsync(owner => owner.AnimalOwnerId == id);

			return result;
		}

		public async Task<AnimalOwnerDetailsModel> GetOwnerDetails(int id)
		{
			AnimalOwnerDetailsModel? animalOwnerDetails = await data.AnimalOwners
				.Where(owner => owner.AnimalOwnerId == id)
				.Select(owner => new AnimalOwnerDetailsModel()
				{
					Id = owner.AnimalOwnerId,
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					PhoneNumber = owner.PhoneNumber,
					TotalAnimalsCount = owner.Animals.Count(),
					TotalVisits = owner.Appointments.Count(),
					Animals = owner.Animals.Select(animal => new AnimalDetailsModel()
					{
						Id = animal.Id,
						Age = animal.Age,
						Weight = animal.Weight,
						AnimalTypeName = animal.AnimalType.Name
					}).ToList()
				}).FirstOrDefaultAsync();


			return animalOwnerDetails;
		}

		public async Task<int> AddAnimalOwner(AnimalOwnerFormModel model)
		{

			AnimalOwner animalOwner = new AnimalOwner(model.FirstName, model.LastName, model.PhoneNumber);

			await data.AnimalOwners.AddAsync(animalOwner);
			await data.SaveChangesAsync();

			return animalOwner.AnimalOwnerId;
		}

		public async Task<AnimalOwnerFormModel> GetForm(int id)
		{
			AnimalOwner owner = await data.AnimalOwners.FindAsync(id);

			AnimalOwnerFormModel ownerForm = new AnimalOwnerFormModel()
			{
				FirstName = owner.FirstName,
				LastName = owner.LastName,
				PhoneNumber = owner.PhoneNumber
			};

			return ownerForm;
		}

		public async Task EditAnimalOwner(int id, AnimalOwnerFormModel model)
		{
			AnimalOwner? animalOwner = await data.AnimalOwners.FindAsync(id);

			animalOwner.FirstName = model.FirstName;
			animalOwner.LastName = model.LastName;
			animalOwner.PhoneNumber = model.PhoneNumber;

			await data.SaveChangesAsync();
		}

		public async Task DeleteAnimalOwner(int id)
		{
			AnimalOwner owner = await data.AnimalOwners.FindAsync(id);

			data.AnimalOwners.Remove(owner);
			await data.SaveChangesAsync();
		}
	}
}
