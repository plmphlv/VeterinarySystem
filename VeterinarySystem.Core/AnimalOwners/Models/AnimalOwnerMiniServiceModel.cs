using AnimalOwner.Contracts;

namespace AnimalOwner.AnimalOwner
{
    public class AnimalOwnerMiniServiceModel : IOwner
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
