using VeterinarySystem.Data.Domain.Entities;

namespace Animal.Animal
{
    public class AnimalFormModel
    {
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public int Age { get; set; }
        public int AnimalTypeId { get; set; }
        public int OwnerId { get; set; }

        public ICollection<AnimalTypesServiceModels> AnimalTypes { get; set; } = new List<AnimalTypesServiceModels>();
    }
}
