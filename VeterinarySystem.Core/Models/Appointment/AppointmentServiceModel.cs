namespace VeterinarySystem.Core.Models.Appointment
{
    public class AppointmentServiceModel
    {
        public int Id { get; set; }

        public string AppointmentDate { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsUpcoming { get; set; }

        public string OwnerFullName { get; set; } = string.Empty;

        public string StaffName { get; set; } = string.Empty;
    }
}
