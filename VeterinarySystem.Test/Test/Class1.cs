using Microsoft.Extensions.DependencyInjection;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Test.Test
{
	public class Class1
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IAnimalOwnerService service;
		private int ownerId;

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
		public async Task GetAllOwners()
		{
			bool result = await service.AnimalOwnerExists(ownerId);

			Assert.That(result, Is.EqualTo(true));
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
		}
	}
}
