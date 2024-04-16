using Microsoft.Extensions.DependencyInjection;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
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
		public async Task Test_AnimalOwnerExists1()
		{
			bool result = await service.AppointmenExists(appointmentId);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task Test_AnimalOwnerExists2()
		{
			bool result = await service.AppointmenExists(-2077);

			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task Test_GetAppointmentDetails()
		{
			AppointmentServiceModel? appointmenActualResult = await service.GetAppointmentDetails(appointmentId);

			Assert.That(appointmenActualResult.Id, Is.EqualTo(appointmentId));
			Assert.That(appointmenActualResult.Description, Is.EqualTo(appointment.AppointmentDesctiption));
			Assert.That(appointmenActualResult.OwnerFullName, Is.EqualTo($"{owner.FirstName} {owner.LastName}"));
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
		public async Task Test_RemoveAppointment()
		{
			await service.RemoveAppointment(appointmentId);
			bool result = await service.AppointmenExists(appointmentId);

			Assert.That(result, Is.EqualTo(false));
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
