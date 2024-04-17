using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.AnimalOwner;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VeterinarySystem.Test.Test
{
	public class PrescriptionServiceTest
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IPrescriptionService service;

		private int ownerId;
		private AnimalOwner owner;

		private string staffMemberId;
		private StaffMember staffMember;

		private int prescriptionId;
		private int animalId;

		private Animal animal;
		private Prescription prescription;

		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IPrescriptionService, PrescriptionService>()
				.BuildServiceProvider();

			service = serviceProvider.GetService<IPrescriptionService>();

			VeterinarySystemDbContext context = serviceProvider
				.GetService<VeterinarySystemDbContext>();

			await SeedDb(context);
		}

		[Test]
		public async Task Test_PrescriptionExists1()
		{
			bool result = await service.PrescriptionExists(prescriptionId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_PrescriptionExists2()
		{
			bool result = await service.PrescriptionExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_GetPrescriptionDetails()
		{

			PrescriptionServiceModel? prescriptionActualResult = await service.GetPrescriptionDetails(prescriptionId);

			Assert.That(prescriptionActualResult.Number, Is.EqualTo(prescription.Number));
			Assert.That(prescriptionActualResult.Description, Is.EqualTo(prescription.Description));
			Assert.That(prescriptionActualResult.AnimalId, Is.EqualTo(prescription.AnimalId));
		}

		[Test]
		public async Task Test_EditPrescription()
		{
			PrescriptionFormModel prescriptionTestForm = new PrescriptionFormModel()
			{
				Description = "Something",
				StaffMemberId = staffMemberId
			};

			await service.EditPrescription(ownerId, prescriptionTestForm);

			PrescriptionServiceModel? prescriptionResult = await service.GetPrescriptionDetails(prescriptionId);

			Assert.That(prescriptionResult.Number, Is.EqualTo(prescription.Number));
			Assert.That(prescriptionResult.IssueDate, Is.EqualTo(prescription.IssueDate.ToString(EntityConstants.DateOnlyFormat)));
			Assert.That(prescriptionResult.Description, Is.EqualTo(prescriptionTestForm.Description));
		}

		[Test]
		public async Task Test_DeletePrescription()
		{
			await service.DeletePrescription(prescriptionId);
			bool result = await service.PrescriptionExists(prescriptionId);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AddPrescription()
		{
			PrescriptionFormModel newForm = new PrescriptionFormModel()
			{
				Number = await service.GetPrescriptionNumber(),
				Description = "I'm out of ideas at this point",
				IssueDate = DateTime.Today,
				StaffMemberId = staffMemberId
			};


			int newId = await service.AddPrescription(newForm, animalId);
			bool result = await service.PrescriptionExists(newId);


			Assert.That(result, Is.EqualTo(true));
			Assert.That(newId, Is.EqualTo(prescriptionId + 1));
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

			Animal pet = new Animal()
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

			Prescription _prescription = new Prescription()
			{
				Number = await service.GetPrescriptionNumber(),
				Description = "Antibiotics for gum infection",
				AnimalId = pet.Id,
				IssueDate = DateTime.Today,
				StaffMemberId = staff.Id
			};

			var counter = await context.PrescriptionCounters.FirstOrDefaultAsync(c => c.Id == 1);
			counter.CurrentNumber++;

			await context.Prescriptions.AddAsync(_prescription);
			await context.SaveChangesAsync();

			ownerId = owner1.Id;
			owner = owner1;
			staffMemberId = staff.Id;
			staffMember = staff;
			prescriptionId = _prescription.Id;
			prescription = _prescription;
		}
	}
}
