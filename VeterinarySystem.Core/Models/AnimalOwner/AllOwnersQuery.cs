using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Core.Infrastructure;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class AllOwnersQuery
	{
		[Display(Name = "Search")]
		public string SearchTerm { get; init; } = null!;

		public SearchParameter Parameter { get; init; }

		public int TotalOwnersCount { get; set; }

		public IEnumerable<string> Categories { get; set; } = null!;

		public IEnumerable<AnimalOwnerMiniServiceModel> Owners { get; set; } = new List<AnimalOwnerMiniServiceModel>();
	}
}
