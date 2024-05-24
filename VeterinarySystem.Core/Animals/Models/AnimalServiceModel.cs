using Animal.Contracts;

namespace Animal.Animal
{
    public class AnimalServiceModel : IAnimal
    {
        public int Id { get; set; }

        public string AnimalName { get; set; } = string.Empty;

        public int Age { get; set; }

        public double Weight { get; set; }

        public string AnimalTypeName { get; set; } = null!;

        public int OwnerId { get; set; }

        public string OwnerFullName { get; set; } = null!;
    }
}