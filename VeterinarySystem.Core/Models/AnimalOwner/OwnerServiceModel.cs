using System.Collections;
using VeterinarySystem.Core.Models.Animal;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
    public class OwnerServiceModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public ICollection<AnimalServiceModel> Animals { get; set; } = new List<AnimalServiceModel>();

		public int TotalAnimalsCount { get; set; }

		public int TotalVisits { get; set; }
	}
}