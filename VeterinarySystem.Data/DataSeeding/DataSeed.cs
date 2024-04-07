using Microsoft.AspNetCore.Identity;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.DataSeed
{
	internal static class DataSeed
	{
		public static StaffMember[] SeedUsers()
		{
			PasswordHasher<StaffMember> hasher = new PasswordHasher<StaffMember>();

			StaffMember administrator = new StaffMember()
			{
				Id = "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
				UserName = "chad@admin.com",
				NormalizedUserName = "CHAD@ADMIM.COM",
				Email = "chad@admin.com",
				NormalizedEmail = "CHAD@ADMIN.COM",
				FirstName = "Гига",
				LastName = "Чад",

			};
			administrator.PasswordHash = hasher.HashPassword(administrator, "DealWithIt!");

			StaffMember vet = new StaffMember()
			{
				Id = "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
				UserName = "steli@vet.com",
				NormalizedUserName = "STELI@VET.COM",
				Email = "steli@vet.com",
				NormalizedEmail = "STELI@VET.COM",
				FirstName = "Steliyana",
				LastName = "Tifonova",
			};
			vet.PasswordHash = hasher.HashPassword(vet, "Steli123");


			return new StaffMember[] { vet, administrator };
		}

		public static AnimalType[] SeedAnimalTypes()
		{
			AnimalType[] AnimalTypes = new AnimalType[]
			{
				new AnimalType()
				{
					Id = 1,
					Name = "Cat"
				},
				new AnimalType()
				{
					Id = 2,
					Name = "Dog"
				},
				new AnimalType()
				{
					Id = 3,
					Name = "Bird"
				},
				new AnimalType()
				{
					Id = 5,
					Name = "Livestock"
				},
				new AnimalType()
				{
					Id = 6,
					Name = "Transportation Animal"
				},
				new AnimalType()
				{
					Id = 7,
					Name = "Other mammal"
				}
			};

			return AnimalTypes;
		}

		public static AnimalOwner SeedOwner()
		{
			AnimalOwner owner = new AnimalOwner()
			{
				Id = 1,
				FirstName = "Plamen",
				LastName = "Pehlivanov",
				PhoneNumber = "0123456789",
			};

			return owner;
		}

	};
}



