using Common.Common;
using Microsoft.Extensions.DependencyInjection;
using Procedures;
using Procedures.Contracts;
using Procedures.Procedure;
using VeterinarySystem.Common;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Test.Test
{
	public class ProcedureServiceTest
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IProcedureService service;

		private int animalId;
		private Data.Domain.Entities.Animal animal;

		private int ownerId;
		private Data.Domain.Entities.AnimalOwner owner;

		private Procedure _procedure1;
		private int procedure1Id;

		private string staffMemberId;
		private StaffMember staffMember;


		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IProcedureService, ProcedureService>()
				.BuildServiceProvider();

			service = serviceProvider.GetService<IProcedureService>();

			VeterinarySystemDbContext context = serviceProvider
				.GetService<VeterinarySystemDbContext>();

			await SeedDb(context);
		}

		[Test]
		public async Task Test_AnimalExists1()
		{
			bool result1 = await service.ProcedureExists(procedure1Id);

			Assert.That(result1, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalExists2()
		{
			bool result = await service.ProcedureExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_GetProcetudeDetails()
		{
			ProcedureServiceModel? procedurelResult = await service.GetProcedudeDetails(procedure1Id);

			Assert.That(procedurelResult.Id, Is.EqualTo(procedure1Id));
			Assert.That(procedurelResult.Name, Is.EqualTo(_procedure1.Name));
			Assert.That(procedurelResult.Description, Is.EqualTo(_procedure1.Description));
			Assert.That(procedurelResult.AnimalName, Is.EqualTo(animal.Name));
			Assert.That(procedurelResult.StaffMemberFullName, Is.EqualTo($"{staffMember.FirstName} {staffMember.LastName}"));
		}

		[Test]
		public async Task Test_EditProcedude()
		{
			ProcedureFormModel procetudeForm = new ProcedureFormModel()
			{
				Name = "Test",
				Description = "Castration",
				Date = DateTime.Today.AddDays(1),
				StaffMemberId = staffMemberId
			};

			await service.EditProcedude(procetudeForm, procedure1Id);

			ProcedureServiceModel? procedureActualResult = await service.GetProcedudeDetails(procedure1Id);

			Assert.That(procedureActualResult.Id, Is.EqualTo(procedure1Id));
			Assert.That(procedureActualResult.Name, Is.EqualTo(procetudeForm.Name));
			Assert.That(procedureActualResult.Description, Is.EqualTo(procetudeForm.Description));
		}

		[Test]
		public async Task Test_DeleteProcedure()
		{
			await service.DeleteProcedude(procedure1Id);
			bool result = await service.ProcedureExists(procedure1Id);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_CreateNewProcedude()
		{
			ProcedureFormModel form = new ProcedureFormModel()
			{
				Name = "Blood Test",
				Description = "A blood tets for diabeties",
				Date = DateTime.Today.AddDays(1),
				StaffMemberId = staffMemberId
			};

			int newId = await service.CreateNewProcedude(animalId, form);
			bool result = await service.ProcedureExists(newId);

			Assert.That(result, Is.EqualTo(true));
			Assert.That(newId, Is.EqualTo(procedure1Id + 1));
		}

		[Test]
		public async Task Test_GetEditingForm()
		{
			ProcedureFormModel? model = await service.GetEditingForm(procedure1Id);

			Assert.That(model.Name, Is.EqualTo(_procedure1.Name));
			Assert.That(model.Description, Is.EqualTo(_procedure1.Description));
			Assert.That(model.Date.ToString(EntityConstants.DateFormat), Is.EqualTo(_procedure1.Date.ToString(EntityConstants.DateFormat)));
			Assert.That(model.StaffMemberId, Is.EqualTo(_procedure1.StaffMemberId));
		}

		[Test]
		public async Task Test_GetDeleteViewModel()
		{
			string controllerName = "TestController";

			DeleteViewModel? model = await service.GetDeleteViewModel(ownerId, controllerName);

			Assert.That(model.Id, Is.EqualTo(ownerId));
			Assert.That(model.Description, Is.EqualTo(_procedure1.Name));
			Assert.That(model.Controller, Is.EqualTo(controllerName));
		}

		[TearDown]
		public async Task TearDown()
		{
			serviceProvider.Dispose();
			await dbContext.Dispose();
		}

		private async Task SeedDb(VeterinarySystemDbContext context)
		{
			Data.Domain.Entities.AnimalOwner owner1 = new Data.Domain.Entities.AnimalOwner()
			{
				FirstName = "Johny",
				LastName = "Test",
				PhoneNumber = "1234567890"
			};

			await context.AnimalOwners.AddAsync(owner1);

			Data.Domain.Entities.Animal pet = new Data.Domain.Entities.Animal()
			{
				Name = "Test",
				Age = 2,
				AnimalTypeId = 1,
				AnimalOwnerId = 2,
			};

			await context.Animals.AddAsync(pet);

			StaffMember staff = new StaffMember()
			{
				Email = "test@doc.com",
				FirstName = "Doctor",
				LastName = "Test"
			};

			context.Users.Add(staff);

			await context.SaveChangesAsync();

			Procedure procedures = new Procedure()
			{
				Name = "Test Extraction",
				Description = "A Test extraction of a model tooth",
				Date = DateTime.Today,
				AnimalId = pet.Id,
				StaffMemberId = staff.Id
			};

			await context.Procedures.AddRangeAsync(procedures);

			await context.SaveChangesAsync();

			ownerId = owner1.Id;
			owner = owner1;
			animalId = pet.Id;
			animal = pet;
			staffMember = staff;
			staffMemberId = staff.Id;
			_procedure1 = procedures;
			procedure1Id = procedures.Id;
		}
	}
}
