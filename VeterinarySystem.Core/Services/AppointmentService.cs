using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

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
				StaffMemberId = form.StaffMemberId,
				AnimalOwnerId = ownerId
			};

			data.Appointments.Add(appointment);
			await data.SaveChangesAsync();

			return appointment.Id;
		}


		public async Task<AppointmentServiceModel> GetAppointmentDetails(int appontmentId)
		{
			AppointmentServiceModel? appointment = await data.Appointments
				.Select(appointment => new AppointmentServiceModel()
				{
					Id = appointment.Id,
					AppointmentDate = appointment.AppointmentDate.ToString(EntityConstants.DateFormat),
					Description = appointment.AppointmentDesctiption,
					AnimalOwnerId = appointment.AnimalOwnerId,
					IsUpcoming = appointment.IsUpcoming,
					StaffMemberId = appointment.StaffMemberId
				})
				.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			return appointment;
		}

		public Task EditAppointment(AppointmentFromModel form)
		{
			throw new NotImplementedException();
		}

		public async Task RemoveAppointment(int appontmentId)
		{
			Appointment appointment = await data.Appointments
				.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			data.Appointments.Remove(appointment);
			await data.SaveChangesAsync();
		}

		public async Task ChangeAppointmentUpcomingStatus(int appointmentId)
		{
			Appointment appointment = await data.Appointments.
				FirstOrDefaultAsync(ap => ap.Id == appointmentId);

			if (appointment.IsUpcoming is true)
			{
				appointment.IsUpcoming = false;
			}
			else
			{
				appointment.IsUpcoming = true;
			}
		}

		public async Task<bool> AppointmenExists(int appontmentId)
		{
			bool result = await data.Appointments.AnyAsync(appontment => appontment.Id == appontmentId);

			return result;
		}
	}
}
