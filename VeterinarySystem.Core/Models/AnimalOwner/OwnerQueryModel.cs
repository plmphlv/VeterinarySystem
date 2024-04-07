namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class OwnerQueryModel
	{
		public int SearchResults { get; set; }
		public ICollection<OwnerServiceModel> OwnersFound { get; set; } = new List<OwnerServiceModel>();
	}
}