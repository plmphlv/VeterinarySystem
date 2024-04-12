using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Core.Infrastructure;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class AllOwnersQuery
	{
		public const int OwnersPerPAge = 8;

		[Display(Name = "Search")]
		public string SearchTerm { get; init; } = null!;

		public SearchParameter Parameter { get; init; }

		public int CurrentPage { get; init; } = 1;

		public int TotalOwnersCount { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<AnimalOwnerMiniServiceModel> Owners { get; set; } = new List<AnimalOwnerMiniServiceModel>();
	}
}
