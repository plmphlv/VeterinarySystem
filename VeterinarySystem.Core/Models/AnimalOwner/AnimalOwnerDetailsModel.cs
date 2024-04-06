using System.Collections;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class AnimalOwnerDetailsModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public ICollection<AnimalDetailsModel> Animals { get; set; } = new List<AnimalDetailsModel>();

		public int TotalAnimalsCount { get; set; }

		public int TotalVisits { get; set; }
	}
}