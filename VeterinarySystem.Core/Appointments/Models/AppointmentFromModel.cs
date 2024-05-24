using System.ComponentModel.DataAnnotations;
using Common.Models;
using VeterinarySystem.Common;

namespace Appointments.Appointment
{
    public class AppointmentFromModel
    {
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DateTime Date { get; set; }

        public string StaffMemberId { get; set; } = null!;

        public ICollection<StaffServiceModel> Staff = new List<StaffServiceModel>();
    }
}
