namespace Appointments.Appointment
{
    public class AllAppointmentsQueryModel
    {
        public const int AppointmensPerPage = 8;

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;

        public int TotalAppointmensCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; } = 1;

        public ICollection<AppointmentServiceModel> Appointments { get; set; } = new List<AppointmentServiceModel>();
    }
}
