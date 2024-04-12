namespace VeterinarySystem.Core.Models.Appointment
{
	public class AllAppointmentsQueryModel
	{
		public const int AppointmensPerPage = 3;

		public DateTime? StartDate { get; set; } = null;

		public DateTime? EndDate { get; set; } = null;

        public int AppointmensCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; } = 1;

        public ICollection<AppointmentServiceModel> Appointments { get; set; } = new List<AppointmentServiceModel>();
	}
}
