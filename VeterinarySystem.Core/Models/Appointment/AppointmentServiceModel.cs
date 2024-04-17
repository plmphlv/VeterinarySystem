using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Core.Models.Appointment
{
    public class AppointmentServiceModel : IDescription, IOwner
	{
		public int Id { get; set; }

		public string AppointmentDate { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public bool IsUpcoming { get; set; }

		public string FullName { get; set; } = string.Empty;

		public int OwnerId { get; set; }

		public string StaffName { get; set; } = string.Empty;
	}
}
