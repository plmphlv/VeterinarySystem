using VeterinarySystem.Core.Models.Animal;
using VeterinarySystem.Core.Models.Appointment;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class OwnerServiceModel : AnimalOwnerMiniServiceModel
	{

		public ICollection<AnimalServiceModel> Animals { get; set; } = new List<AnimalServiceModel>();

		public string LastAppointments { get; set; } = string.Empty;

		public int TotalAnimalsCount { get; set; }

		public int TotalVisits { get; set; }
	}
}