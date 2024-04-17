using Microsoft.Extensions.DependencyInjection;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Services;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Test.Test
{
	public class AppointmentServiceTest
	{
		private InMemoryDbContext dbContext;
		private ServiceProvider serviceProvider;
		private IAppointmentService service;

		private int ownerId;
		private AnimalOwner owner;

		private string staffMemberId;
		private StaffMember staffMember;

		private int appointmentId;
		private Appointment appointment;

		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IAppointmentService, AppointmentService>()
				.BuildServiceProvider();

			service = serviceProvider.GetService<IAppointmentService>();

			VeterinarySystemDbContext context = serviceProvider
				.GetService<VeterinarySystemDbContext>();

			await SeedDb(context);
		}

		[Test]
		public async Task Test_AppointmenExists1()
		{
			bool result = await service.AppointmenExists(appointmentId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AppointmenExists2()
		{
			bool result = await service.AppointmenExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_AddAppointment()
		{
			AppointmentFromModel appointmentTestForm = new AppointmentFromModel()
			{
				Date = DateTime.Now,
				Description = "New test add",
				StaffMemberId = staffMemberId
			};

			int newId = await service.AddAppointment(appointmentTestForm, ownerId);

			AppointmentServiceModel? appointmentActualResult = await service.GetAppointmentDetails(newId);

			Assert.That(appointmentActualResult.Id, Is.EqualTo(appointmentId + 1));
			Assert.That(appointmentTestForm.Date.ToString(EntityConstants.DateFormat), Is.EqualTo(appointmentActualResult.AppointmentDate));
			Assert.That(appointmentTestForm.Description, Is.EqualTo(appointmentActualResult.Description));
			Assert.That(appointmentTestForm.StaffMemberId, Is.EqualTo(staffMemberId));
		}

		[Test]
		public async Task Test_GetAppointmentDetails()
		{
			AppointmentServiceModel? appointmenActualResult = await service.GetAppointmentDetails(appointmentId);

			Assert.That(appointmenActualResult.Id, Is.EqualTo(appointmentId));
			Assert.That(appointmenActualResult.Description, Is.EqualTo(appointment.AppointmentDesctiption));
			Assert.That(appointmenActualResult.FullName, Is.EqualTo($"{owner.FirstName} {owner.LastName}"));
			Assert.That(appointmenActualResult.StaffName, Is.EqualTo($"{staffMember.FirstName} {staffMember.LastName}"));
		}

		[Test]
		public async Task Test_EditAppointment()
		{
			AppointmentFromModel appointmentTestForm = new AppointmentFromModel()
			{
				Description = "Flu examination",
				StaffMemberId = staffMemberId
			};

			await service.EditAppointment(appointmentId, appointmentTestForm);

			AppointmentServiceModel? appointmentActualResult = await service.GetAppointmentDetails(appointmentId);

			Assert.That(appointmentTestForm.Description, Is.EqualTo(appointmentActualResult.Description));
			Assert.That(appointmentTestForm.StaffMemberId, Is.EqualTo(staffMemberId));
		}

		[Test]
		public async Task Test_DeleteAppointment()
		{
			int testOwnerId = await service.DeleteAppointment(appointmentId);
			bool result = await service.AppointmenExists(appointmentId);

			Assert.That(result, Is.EqualTo(false));
			Assert.That(testOwnerId, Is.EqualTo(ownerId));
		}

		[Test]
		public async Task Test_GetDeleteViewModel()
		{
			string controllerName = "TestController";

			DeleteViewModel? model = await service.GetDeleteViewModel(appointmentId, controllerName);

			Assert.That(model.Id, Is.EqualTo(appointmentId));
			Assert.That(model.Description, Is.EqualTo(appointment.AppointmentDate.ToString(EntityConstants.DateFormat)));
			Assert.That(model.Controller, Is.EqualTo(controllerName));
		}

		[Test]
		public async Task Test_GetFormForEditing()
		{
			AppointmentFromModel? model = await service.GetFormForEditing(appointmentId);

			Assert.That(model.Description, Is.EqualTo(appointment.AppointmentDesctiption));
			Assert.That(model.Date.ToString(EntityConstants.DateFormat), Is.EqualTo(appointment.AppointmentDate.ToString(EntityConstants.DateFormat)));
			Assert.That(model.StaffMemberId, Is.EqualTo(appointment.StaffMemberId));
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

			StaffMember staff = new StaffMember()
			{
				Email = "test@doc.com",
				FirstName = "Doctor",
				LastName = "Test"
			};

			context.Users.Add(staff);

			await context.SaveChangesAsync();

			Appointment _appointment = new Appointment()
			{
				AppointmentDate = DateTime.Now,
				AppointmentDesctiption = "General health examination",
				AnimalOwnerId = 2,
				StaffMemberId = staff.Id,
				IsUpcoming = true,
			};

			await context.AnimalOwners.AddAsync(owner1);
			await context.Appointments.AddAsync(_appointment);
			await context.SaveChangesAsync();

			ownerId = owner1.Id;
			owner = owner1;
			staffMemberId = staff.Id;
			staffMember = staff;
			appointment = _appointment;
			appointmentId = appointment.Id;
		}
	}
}
