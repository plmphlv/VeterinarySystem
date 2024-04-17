using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
    public class AnimalOwnerMiniServiceModel : IOwner
	{
		public int Id { get; set; }

		public string FullName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;
	}
}
