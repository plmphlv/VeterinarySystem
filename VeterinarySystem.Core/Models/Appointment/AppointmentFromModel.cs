using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Core.Models.Appointment
{
	public class AppointmentFromModel
	{
		[Required(ErrorMessage = ErrorMessages.RequiredError)]
		public string AppointmentDate { get; set; } = null!;


		public string AppointmentDesctiption { get; set; } = string.Empty;

		public int AnimalOwnerId { get; set; }

		public string StaffMemberId { get; set; } = null!;
	}
}
