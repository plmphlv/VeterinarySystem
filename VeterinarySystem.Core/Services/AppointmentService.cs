using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Globalization;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
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
				AnimalOwnerId = ownerId,
				IsUpcoming = true,
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
					//AnimalOwnerId = appointment.AnimalOwnerId,
					IsUpcoming = appointment.IsUpcoming,
					OwnerFullName = $"{appointment.AnimalOwner.FirstName} {appointment.AnimalOwner.LastName}",
					StaffName = $"{appointment.StaffMember.FirstName} {appointment.StaffMember.LastName}"
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

			await data.SaveChangesAsync();
		}

		public async Task<bool> AppointmenExists(int appontmentId)
		{
			bool result = await data.Appointments.AnyAsync(appontment => appontment.Id == appontmentId);

			return result;
		}

		public async Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			ICollection<StaffServiceModel> staff = await data.Users
				.Where(u => u.Email != AdminUser.AdminEmail)
				.Select(u => new StaffServiceModel()
				{
					StaffId = u.Id,
					StaffName = $"{u.FirstName} {u.LastName}"
				})
				.ToListAsync();

			return staff;
		}

		public async Task<int> DeleteAppointment(int appontmentId)
		{
			Appointment appointment = await data.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == appontmentId);

			int ownerId = appointment.AnimalOwnerId;

			data.Appointments.Remove(appointment);
			await data.SaveChangesAsync();

			return ownerId;
		}

		public bool DateFormatIsValid(string dateString)
		{
			DateTime dateAndTime = DateTime.Now;

			if (!DateTime.TryParseExact(
				dateString,
				EntityConstants.DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out dateAndTime))
			{
				return false;
			}
			return true;
		}

		public DateTime GetDate()
		{
			string dateString = DateTime.Now.ToString(EntityConstants.DateFormat);
			DateTime dateAndTime = DateTime.Now;

			DateTime.TryParseExact(
				dateString,
				EntityConstants.DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out dateAndTime);

			return dateAndTime;
		}

		public bool CompareAppointmentDate(DateTime appointmentDate)
		{
			DateTime now = DateTime.Now;

			if (DateTime.Compare(appointmentDate, now) == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
