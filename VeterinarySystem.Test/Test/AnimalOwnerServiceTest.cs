using Microsoft.Extensions.DependencyInjection;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Test.Test
{
	public class AnimalOwnerServiceTest
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IAnimalOwnerService service;
		private int ownerId;
		private AnimalOwner owner;

		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IAnimalOwnerService, AnimalOwnerService>()
				.BuildServiceProvider();

			service = serviceProvider.GetService<IAnimalOwnerService>();

			VeterinarySystemDbContext context = serviceProvider
				.GetService<VeterinarySystemDbContext>();

			await SeedDb(context);
		}

		[Test]
		public async Task Test_AnimalOwnerExists1()
		{
			bool result = await service.AnimalOwnerExists(ownerId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalOwnerExists2()
		{
			bool result = await service.AnimalOwnerExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AnimalOwnerExists3()
		{
			AnimalOwnerFormModel owner = new AnimalOwnerFormModel()
			{
				FirstName = "Johny",
				LastName = "Test",
				PhoneNumber = "1234567890"
			};

			bool result = await service.AnimalOwnerExists(owner);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalOwnerExists4()
		{
			AnimalOwnerFormModel owner = new AnimalOwnerFormModel()
			{
				FirstName = "Test",
				LastName = "Testov",
				PhoneNumber = "1234567890"
			};

			bool result = await service.AnimalOwnerExists(owner);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AnimalOwnerDetails()
		{
			OwnerServiceModel ownerExpectedResult = new OwnerServiceModel()
			{
				FullName = "Johny Test",
				PhoneNumber = "1234567890",
				LastAppointments = "This owner has no previous appointments",
				Animals = new List<AnimalServiceModel>()
			};

			OwnerServiceModel? ownerActualResult = await service.GetOwnerDetails(ownerId);

			Assert.That(ownerActualResult.FullName, Is.EqualTo(ownerExpectedResult.FullName));
			Assert.That(ownerActualResult.PhoneNumber, Is.EqualTo(ownerExpectedResult.PhoneNumber));
			Assert.That(ownerActualResult.LastAppointments, Is.EqualTo(ownerExpectedResult.LastAppointments));
			Assert.That(ownerActualResult.Animals, Is.EqualTo(ownerExpectedResult.Animals));
		}

		[Test]
		public async Task Test_EditAnimalOwner()
		{
			AnimalOwnerFormModel ownerTestForm = new AnimalOwnerFormModel()
			{
				FirstName = "Test",
				LastName = "Testov",
				PhoneNumber = "9876543210",
			};

			await service.EditAnimalOwner(ownerId, ownerTestForm);

			OwnerServiceModel? ownerActualResult = await service.GetOwnerDetails(ownerId);

			Assert.That(ownerId, Is.EqualTo(ownerActualResult.Id));
			Assert.That($"{ownerTestForm.FirstName} {ownerTestForm.LastName}", Is.EqualTo(ownerActualResult.FullName));
			Assert.That(ownerTestForm.PhoneNumber, Is.EqualTo(ownerActualResult.PhoneNumber));
		}

		[Test]
		public async Task Test_DeleteAnimalOwner()
		{
			await service.DeleteAnimalOwner(ownerId);
			bool result = await service.AnimalOwnerExists(ownerId);



			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AddAnimalOwner()
		{
			AnimalOwnerFormModel ownerTestForm = new AnimalOwnerFormModel()
			{
				FirstName = "Test",
				LastName = "Testov",
				PhoneNumber = "9876543210",
			};

			int newOwnerId = await service.AddAnimalOwner(ownerTestForm);
			bool result = await service.AnimalOwnerExists(newOwnerId);


			Assert.That(result, Is.EqualTo(true));
			Assert.That(newOwnerId, Is.EqualTo(ownerId + 1));
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

			await context.AnimalOwners.AddAsync(owner1);
			await context.SaveChangesAsync();

			ownerId = owner1.Id;
			owner = owner1;
		}
	}
}
