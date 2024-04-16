using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Test.Test
{
	public class AnimalServiceTest
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IAnimalService service;
		private int animalId;
		private int ownerId;
		private AnimalOwner owner;
		private Animal animal;

		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IAnimalService, AnimalService>()
				.BuildServiceProvider();

			service = serviceProvider.GetService<IAnimalService>();

			VeterinarySystemDbContext context = serviceProvider
				.GetService<VeterinarySystemDbContext>();

			await SeedDb(context);
		}

		[Test]
		public async Task Test_AnimalExists1()
		{
			bool result = await service.AnimalExists(animalId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalExists2()
		{
			bool result = await service.AnimalExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AnimalExists3()
		{
			AnimalFormModel pet = new AnimalFormModel()
			{
				Name = "Test",
				Age = 2,
				AnimalTypeId = 1
			};

			bool result = await service.AnimalExists(pet, ownerId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalExists4()
		{
			AnimalFormModel pet = new AnimalFormModel()
			{
				Name = "Test",
				Age = 3,
				AnimalTypeId = 2,
			};

			bool result = await service.AnimalExists(pet, ownerId);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AnimalDetails()
		{
			AnimalServiceModel? animalActualResult = await service.GetAnimalDetails(animalId);

			string ownerFullname = $"{owner.FirstName} {owner.LastName}";

			Assert.That(animalActualResult.Id, Is.EqualTo(animalId));
			Assert.That(animalActualResult.Name, Is.EqualTo(animal.Name));
			Assert.That(animalActualResult.Weight, Is.EqualTo(animal.Weight));
			Assert.That(animalActualResult.AnimalTypeName, Is.EqualTo("Cat"));
			Assert.That(animalActualResult.OwnerFullName, Is.EqualTo(ownerFullname));
		}

		[Test]
		public async Task Test_EditAnimal()
		{
			AnimalFormModel animalTestForm = new AnimalFormModel()
			{
				Name = "Test",
				Weight = 50,
				Age = 20,
				AnimalTypeId = 2
			};

			await service.EditAnimal(animalId, animalTestForm);

			AnimalServiceModel? animalActualResult = await service.GetAnimalDetails(animalId);
			int animalTypeId = await service.GetAnimalTypeById(animalId);

			Assert.That(animalId, Is.EqualTo(animalActualResult.Id));
			Assert.That(animalTestForm.Name, Is.EqualTo(animalActualResult.Name));
			Assert.That(animalTestForm.Weight, Is.EqualTo(animalActualResult.Weight));
			Assert.That(animalTestForm.Age, Is.EqualTo(animalActualResult.Age));
			Assert.That(animalTypeId, Is.EqualTo(animalTestForm.AnimalTypeId));
		}

		[Test]
		public async Task Test_DeleteAnimal()
		{
			await service.DeleteAnimal(animalId);
			bool result = await service.AnimalExists(animalId);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_GetOwnerByPetId()
		{
			int ownerIdResult = await service.GetOwnerByPetId(animalId);

			Assert.That(ownerIdResult, Is.EqualTo(ownerId));
		}

		[Test]
		public async Task Test_AddAnimal()
		{
			AnimalFormModel animalTestForm = new AnimalFormModel()
			{
				Name = "Test",
				Weight = 50,
				Age = 20,
				AnimalTypeId = 2,
				OwnerId = ownerId
			};

			int newPetId = await service.AddNewAnimal(animalTestForm, ownerId);
			bool result = await service.AnimalExists(newPetId);


			Assert.That(result, Is.EqualTo(true));
			Assert.That(newPetId, Is.EqualTo(animalId + 1));
		}

		[TearDown]
		public async Task TearDown()
		{
			serviceProvider.Dispose();
			await dbContext.Dispose();
		}

		private async Task SeedDb(VeterinarySystemDbContext context)
		{
			AnimalOwner owner1 = new AnimalOwner()
			{
				FirstName = "Johny",
				LastName = "Test",
				PhoneNumber = "1234567890"
			};

			Animal pet = new Animal()
			{
				Name = "Test",
				Age = 2,
				AnimalTypeId = 1,
				AnimalOwnerId = 2,
			};

			await context.AnimalOwners.AddAsync(owner1);
			await context.Animals.AddAsync(pet);
			await context.SaveChangesAsync();

			ownerId = owner1.Id;
			animalId = pet.Id;
			owner = owner1;
			animal = pet;
		}
	}
}
