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

		public async Task AddAnimalOwner(AnimalOwnerFormModel model)
		{

			AnimalOwner animalOwner = new AnimalOwner(model.FirstName, model.LastName, model.PhoneNumber);

			await data.AnimalOwners.AddAsync(animalOwner);
			await data.SaveChangesAsync();
		}

		public async Task EditAnimalOwner(int id, AnimalOwnerFormModel model)
		{
			AnimalOwner? animalOwner = await data.AnimalOwners.FindAsync(id);

			animalOwner.FirstName = model.FirstName;
			animalOwner.LastName = model.LastName;
			animalOwner.PhoneNumber = model.PhoneNumber;
			
			await data.SaveChangesAsync();
		}

		public Task DeleteAnimalOwner(int id)
		{
			throw new NotImplementedException();
		}
	}
}
