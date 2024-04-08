using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Models.Appointment
{
	public class AppointmentFromModel
	{
		public string Desctiption { get; set; } = string.Empty;

		[Required(ErrorMessage = ErrorMessages.RequiredError)]
		public DateTime Date { get; set; }

		public string StaffMemberId { get; set; } = null!;

		public ICollection<StaffServiceModel> Staff = new List<StaffServiceModel>();
	}
}
