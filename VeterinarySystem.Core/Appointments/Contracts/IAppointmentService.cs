using Appointments.Appointment;
using Common.Common;
using Common.Models;

namespace Appointments.Contracts
{
    public interface IAppointmentService
    {
        Task<int> AddAppointment(AppointmentFromModel form, int ownerId);//C

        Task<AppointmentServiceModel> GetAppointmentDetails(int appontmentId);

        Task EditAppointment(int appontmentId, AppointmentFromModel form);

        Task ChangeAppointmentUpcomingStatus(int appointmentId);

        Task<bool> AppointmenExists(int appontmentId);

        Task<int> DeleteAppointment(int appontmentId);

        Task<ICollection<StaffServiceModel>> GetStaffMembers();

        Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

        Task<AppointmentFromModel> GetFormForEditing(int appontmentId);

        Task<ICollection<AppointmentServiceModel>> TodaysSchedule();

        Task<AppointmenQueryServiceModel> GetAppointmensForPeriod(
            DateTime? startDate,
            DateTime? endDate,
            int currentPage = 1,
            int appointmentsPerPage = 1
            );
    }
}
