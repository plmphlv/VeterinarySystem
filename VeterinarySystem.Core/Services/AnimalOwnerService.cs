using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
            IQueryable<AnimalOwner> ownerQuery = data.AnimalOwners.AsQueryable();

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
                    PhoneNumber = owner.PhoneNumber,
                    TotalAnimalsCount = owner.Animals.Count(),
                    TotalVisits = owner.Appointments.Count(),
                    Animals = owner.Animals.Select(animal => new AnimalServiceModel()
                    {
                        Id = animal.Id,
                        Age = animal.Age,
                        Weight = animal.Weight,
                        AnimalTypeName = animal.AnimalType.Name
                    }).ToList()
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
            bool result = await data.AnimalOwners.AnyAsync(owner => owner.FirstName == model.FirstName && owner.LastName == model.LastName && owner.PhoneNumber == model.PhoneNumber);

            return result;
        }

        public async Task<bool> AnimalOwnerExists(int id)
        {
            bool result = await data.AnimalOwners.AnyAsync(owner => owner.Id == id);

            return result;
        }

        public async Task<OwnerServiceModel> GetOwnerDetails(int id)
        {
            OwnerServiceModel? animalOwnerDetails = await data.AnimalOwners
                .Where(owner => owner.Id == id)
                .Select(owner => new OwnerServiceModel()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    PhoneNumber = owner.PhoneNumber,
                    TotalAnimalsCount = owner.Animals.Count(),
                    TotalVisits = owner.Appointments.Count(),
                    Appointments = owner.Appointments.Select(a => new AppointmentServiceModel
                    {
                        Id = a.Id,
                        AppointmentDate = a.AppointmentDate.ToString(EntityConstants.DateFormat),
                        Description = a.AppointmentDesctiption,
                        IsUpcoming = a.IsUpcoming,
                        //AnimalOwnerId = a.AnimalOwnerId,
                        OwnerFullName = $"{a.AnimalOwner.FirstName} {a.AnimalOwner.LastName}",
                        //StaffMemberId = a.StaffMemberId,
                        StaffName = $"{a.StaffMember.FirstName} {a.StaffMember.LastName}"
                    }).ToList(),
                    Animals = owner.Animals.Select(animal => new AnimalServiceModel()
                    {
                        Id = animal.Id,
                        Name = animal.Name,
                        Age = animal.Age,
                        Weight = animal.Weight,
                        AnimalTypeName = animal.AnimalType.Name
                    }).ToList()
                }).FirstOrDefaultAsync();


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
