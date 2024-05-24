namespace Appointments.Appointment
{
    public class AppointmenQueryServiceModel
    {
        public int TotalAppointmens { get; set; }

        public int TotalPages { get; set; }

        public ICollection<AppointmentServiceModel> Appointmens { get; set; } = new List<AppointmentServiceModel>();
    }
}
