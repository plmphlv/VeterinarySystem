using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Models.Appointment;
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

		public async Task<OwnerQueryModel> Search(string? searchTerm, SearchParameter parameter = SearchParameter.FullName)
		{
			IQueryable<AnimalOwner> ownerQuery = data.AnimalOwners
				.AsNoTracking()
				.AsQueryable();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				searchTerm = searchTerm.Trim();

				if (parameter == SearchParameter.FullName)
				{
					string[] searchTermSplit = searchTerm.Split(' ');

					ownerQuery = data.AnimalOwners.Where(owner => owner.FirstName == searchTermSplit[0] && owner.LastName == searchTermSplit[1]);
				}
				else if (parameter == SearchParameter.PhoneNumber)
				{
					ownerQuery = data.AnimalOwners.Where(owner => owner.PhoneNumber == searchTerm);
				}
			}

			ICollection<OwnerServiceModel> owners = await ownerQuery
				.Select(owner => new OwnerServiceModel()
				{
					Id = owner.Id,
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					PhoneNumber = owner.PhoneNumber
				}
			).ToListAsync();

			OwnerQueryModel searchResults = new OwnerQueryModel()
			{
				SearchResults = owners.Count(),
				OwnersFound = owners
			};

			return searchResults;
		}

		public async Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model)
		{
			AnimalOwner? owner = await data.AnimalOwners
				.AsNoTracking()
					.SingleOrDefaultAsync(owner => owner.FirstName == model.FirstName &&
					owner.LastName == model.LastName &&
					owner.PhoneNumber == model.PhoneNumber);

			if (owner is null)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> AnimalOwnerExists(int id)
		{
			bool result = await data.AnimalOwners
				.AsNoTracking()
				.AnyAsync(owner => owner.Id == id);

			return result;
		}

		public async Task<OwnerServiceModel> GetOwnerDetails(int id)
		{
			OwnerServiceModel? animalOwnerDetails = await data.AnimalOwners
				.AsNoTracking()
				.Select(owner => new OwnerServiceModel()
				{
					Id = owner.Id,
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					PhoneNumber = owner.PhoneNumber,
					Appointments = owner.Appointments.Select(a => new AppointmentServiceModel
					{
						Id = a.Id,
						AppointmentDate = a.AppointmentDate.ToString(EntityConstants.DateFormat),
						Description = a.AppointmentDesctiption,
					}).ToList(),
					Animals = owner.Animals.Select(animal => new AnimalServiceModel()
					{
						Id = animal.Id,
						Name = animal.Name,
						Age = animal.Age,
						Weight = animal.Weight,
						AnimalTypeName = animal.AnimalType.Name
					}).ToList()
				}).SingleOrDefaultAsync(owner => owner.Id == id);

			animalOwnerDetails.TotalAnimalsCount = animalOwnerDetails.Animals.Count();
			animalOwnerDetails.TotalVisits = animalOwnerDetails.Appointments.Count();

			return animalOwnerDetails;
		}

		public async Task<int> AddAnimalOwner(AnimalOwnerFormModel model)
		{

			AnimalOwner animalOwner = new AnimalOwner()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber
			};

			await data.AnimalOwners.AddAsync(animalOwner);
			await data.SaveChangesAsync();

			return animalOwner.Id;
		}

		public async Task<AnimalOwnerFormModel> GetEditingForm(int id)
		{
			AnimalOwnerFormModel? ownerForm = await data.AnimalOwners
				.AsNoTracking()
				.Where(owner => owner.Id == id)
				.Select(owner => new AnimalOwnerFormModel()
				{
					FirstName = owner.FirstName,
					LastName = owner.LastName,
					PhoneNumber = owner.PhoneNumber
				}).FirstOrDefaultAsync();

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
