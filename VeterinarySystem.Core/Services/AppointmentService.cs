using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Core.Tools.ExtenshionMethods;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
using VeterinarySystem.Data.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VeterinarySystem.Core.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly VeterinarySystemDbContext data;

		public AppointmentService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public async Task<int> AddAppointment(AppointmentFromModel form, int ownerId)
		{
			Appointment appointment = new Appointment()
			{
				AppointmentDate = form.Date,
				AppointmentDesctiption = form.Desctiption,
				AnimalOwnerId = ownerId,
				StaffMemberId = form.StaffMemberId,
				IsUpcoming = true,
			};

			data.Appointments.Add(appointment);
			await data.SaveChangesAsync();

			return appointment.Id;
		}

		public async Task<AppointmentServiceModel> GetAppointmentDetails(int appontmentId)
		{
			AppointmentServiceModel? appointment = await data.Appointments
				.AsNoTracking()
				.Where(appointment => appointment.Id == appontmentId)
				.Select(appointment => new AppointmentServiceModel()
				{
					Id = appointment.Id,
					AppointmentDate = appointment.AppointmentDate.ToString(EntityConstants.DateFormat),
					Description = appointment.AppointmentDesctiption,
					IsUpcoming = appointment.IsUpcoming,
					OwnerId = appointment.AnimalOwnerId,
					OwnerFullName = $"{appointment.AnimalOwner.FirstName} {appointment.AnimalOwner.LastName}",
					StaffName = $"{appointment.StaffMember.FirstName} {appointment.StaffMember.LastName}"
				})
				.FirstOrDefaultAsync();

			return appointment;
		}

		public async Task EditAppointment(int appontmentId, AppointmentFromModel form)
		{
			Appointment? appointment = await data.Appointments
				.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			appointment.AppointmentDesctiption = form.Desctiption;
			appointment.AppointmentDate = form.Date;
			appointment.StaffMemberId = form.StaffMemberId;

			await data.SaveChangesAsync();
		}

		public async Task RemoveAppointment(int appontmentId)
		{
			Appointment? appointment = await data.Appointments
				.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			data.Appointments.Remove(appointment);
			await data.SaveChangesAsync();
		}

		public async Task ChangeAppointmentUpcomingStatus(int appointmentId)
		{
			Appointment? appointment = await data.Appointments.
				FirstOrDefaultAsync(ap => ap.Id == appointmentId);

			if (appointment.IsUpcoming is true)
			{
				appointment.IsUpcoming = false;
			}
			else
			{
				appointment.IsUpcoming = true;
			}

			await data.SaveChangesAsync();
		}

		public async Task<bool> AppointmenExists(int appontmentId)
		{
			bool result = await data.Appointments.AsNoTracking()
				.AnyAsync(appontment => appontment.Id == appontmentId);

			return result;
		}

		public async Task<int> DeleteAppointment(int appontmentId)
		{
			Appointment? appointment = await data.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			int ownerId = appointment.AnimalOwnerId;

			data.Appointments.Remove(appointment);
			await data.SaveChangesAsync();

			return ownerId;
		}

		public async Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			ICollection<StaffServiceModel> staff = await data.Users
				.AsNoTracking()
				.Where(u => u.Email != AdminUser.AdminEmail)
				.Select(u => new StaffServiceModel()
				{
					StaffId = u.Id,
					StaffName = $"{u.FirstName} {u.LastName}"
				})
				.ToListAsync();

			return staff;
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.Appointments
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Name = e.AppointmentDate.ToString(EntityConstants.DateFormat),
					Controller = controllerName
				}
			).FirstOrDefaultAsync();

			return model;
		}

		public async Task<AppointmentFromModel> GetFormForEditing(int appontmentId)
		{
			AppointmentFromModel? form = await data.Appointments
				.AsNoTracking()
				.Where(appontment => appontment.Id == appontmentId)
				.Select(appontment => new AppointmentFromModel()
				{
					Desctiption = appontment.AppointmentDesctiption,
					Date = appontment.AppointmentDate,
					StaffMemberId = appontment.StaffMemberId,
				})
				.FirstOrDefaultAsync();

			return form;
		}

		public async Task<ICollection<AppointmentServiceModel>> TodaysSchedule()
		{
			DateTime today = (DateTimeQuickTools.GetDateAndTime()).Date;

			ICollection<AppointmentServiceModel> schedule = await data.Appointments
			.AsNoTracking()
				.Where(appointmen => appointmen.AppointmentDate.Date == today &&
				appointmen.IsUpcoming == true)
				.OrderBy(appointment => appointment.AppointmentDate)
				.Select(appointment => new AppointmentServiceModel()
				{
					Id = appointment.Id,
					AppointmentDate = appointment.AppointmentDate.ToString(EntityConstants.DateFormat),
					Description = appointment.AppointmentDesctiption,
					OwnerFullName = $"{appointment.AnimalOwner.FirstName} {appointment.AnimalOwner.LastName}"
				})
				.ToListAsync();

			return schedule;
		}
	}
}
