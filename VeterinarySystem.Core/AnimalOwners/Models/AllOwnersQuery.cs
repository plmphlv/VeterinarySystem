using System.ComponentModel.DataAnnotations;
using Common.Infrastructure;

namespace AnimalOwner.AnimalOwner
{
    public class AllOwnersQuery
    {
        public const int OwnersPerPAge = 8;

        [Display(Name = "Search")]
        public string SearchTerm { get; init; } = null!;

        public OwnerSearchParameters Parameter { get; init; }

        public int TotalOwnersCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; init; } = 1;

        public IEnumerable<AnimalOwnerMiniServiceModel> Owners { get; set; } = new List<AnimalOwnerMiniServiceModel>();
    }
}
