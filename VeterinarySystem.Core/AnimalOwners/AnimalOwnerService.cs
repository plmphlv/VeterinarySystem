﻿using Animal.Animal;
using AnimalOwner.AnimalOwner;
using AnimalOwner.Contracts;
using Common.Common;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Data;

namespace VeterinarySystem.Core.Services
{
	public class AnimalOwnerService : IAnimalOwnerService
	{
		private readonly VeterinarySystemDbContext data;

		public AnimalOwnerService(VeterinarySystemDbContext dbContext)
		{
			data = dbContext;
		}

		public async Task<OwnerQueryModel> Search(string? searchTerm,
			OwnerSearchParameters parameter = OwnerSearchParameters.FullName,
			int pageSize = 5,
			int currentPage = 1)
		{
			IQueryable<Data.Domain.Entities.AnimalOwner> ownerQuery = data.AnimalOwners
				.AsNoTracking()
				.AsQueryable();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				searchTerm = searchTerm.Trim();

				if (parameter == OwnerSearchParameters.FullName)
				{
					string[] searchTermSplit = searchTerm.Split(' ');

					ownerQuery = data.AnimalOwners.Where(owner => owner.FirstName == searchTermSplit[0] && owner.LastName == searchTermSplit[1]);
				}
				else if (parameter == OwnerSearchParameters.PhoneNumber)
				{
					ownerQuery = data.AnimalOwners.Where(owner => owner.PhoneNumber == searchTerm);
				}
			}

			int totalCount = ownerQuery.Count();

			int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

			ICollection<AnimalOwnerMiniServiceModel> owners = await ownerQuery
				.OrderBy(owner => owner.Id)
				.Skip((currentPage - 1) * pageSize)
				.Take(pageSize)
				.Select(owner => new AnimalOwnerMiniServiceModel()
				{
					Id = owner.Id,
					FullName = $"{owner.FirstName} {owner.LastName}",
					PhoneNumber = owner.PhoneNumber
				}
			).ToListAsync();

			OwnerQueryModel searchResults = new OwnerQueryModel()
			{
				TotalOwnersFound = totalCount,
				OwnersFound = owners,
				TotalPages = totalPages
			};

			return searchResults;
		}

		public async Task<bool> AnimalOwnerExists(AnimalOwnerFormModel model)
		{
			Data.Domain.Entities.AnimalOwner? owner = await data.AnimalOwners
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
				.Where(owner => owner.Id == id)
				.Select(owner => new OwnerServiceModel()
				{
					Id = owner.Id,
					FullName = $"{owner.FirstName} {owner.LastName}",
					PhoneNumber = owner.PhoneNumber,
					LastAppointments = owner.Appointments
					.OrderByDescending(a => a.AppointmentDate)
					.Select(a => a.AppointmentDate.ToString(EntityConstants.DateFormat))
					.FirstOrDefault() ?? "This owner has no previous appointments",
					Animals = owner.Animals.Select(animal => new AnimalServiceModel()
					{
						Id = animal.Id,
						AnimalName = animal.Name,
						Age = animal.Age,
						Weight = animal.Weight,
						AnimalTypeName = animal.AnimalType.Name
					}).ToList()
				})
				.SingleOrDefaultAsync();

			if (animalOwnerDetails is null)
			{
				throw new NullReferenceException();
			}

			animalOwnerDetails.TotalAnimalsCount = animalOwnerDetails.Animals.Count();

			return animalOwnerDetails;
		}

		public async Task<int> AddAnimalOwner(AnimalOwnerFormModel model)
		{

			Data.Domain.Entities.AnimalOwner animalOwner = new Data.Domain.Entities.AnimalOwner()
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

			if (ownerForm is null)
			{
				throw new NullReferenceException();
			}

			return ownerForm;
		}

		public async Task EditAnimalOwner(int id, AnimalOwnerFormModel model)
		{
			Data.Domain.Entities.AnimalOwner? animalOwner = await data.AnimalOwners.FindAsync(id);

			if (animalOwner is null)
			{
				throw new NullReferenceException();
			}

			animalOwner.FirstName = model.FirstName;
			animalOwner.LastName = model.LastName;
			animalOwner.PhoneNumber = model.PhoneNumber;

			await data.SaveChangesAsync();
		}

		public async Task DeleteAnimalOwner(int id)
		{
			Data.Domain.Entities.AnimalOwner? owner = await data.AnimalOwners.FindAsync(id);

			if (owner is null)
			{
				throw new NullReferenceException();
			}

			data.AnimalOwners.Remove(owner);
			await data.SaveChangesAsync();
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.AnimalOwners
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Description = $"{e.FirstName} {e.LastName}",
					Controller = controllerName
				}
			).FirstOrDefaultAsync();

			if (model is null)
			{
				throw new NullReferenceException();
			}

			return model;
		}
	}
}