namespace AnimalOwner.AnimalOwner
{
    public class OwnerQueryModel
    {
        public int TotalOwnersFound { get; set; }
        public int TotalPages { get; set; }
        public ICollection<AnimalOwnerMiniServiceModel> OwnersFound { get; set; } = new List<AnimalOwnerMiniServiceModel>();
    }
}