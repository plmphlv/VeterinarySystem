using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAppointmentService
	{
		Task<int> AddAppointment(AppointmentFromModel form, int ownerId);//C

		Task<AppointmentServiceModel> GetAppointmentDetails(int appontmentId);

		Task EditAppointment(AppointmentFromModel form);

		Task RemoveAppointment(int appontmentId);

		Task ChangeAppointmentUpcomingStatus(int appointmentId);

		Task<bool> AppointmenExists(int appontmentId);
	}
}
