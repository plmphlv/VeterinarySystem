namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class OwnerQueryModel
	{
		public int SearchResults { get; set; }
		public ICollection<AnimalOwnerMiniServiceModel> OwnersFound { get; set; } = new List<AnimalOwnerMiniServiceModel>();
	}
}