using System.Collections.Generic;
using VeterinarySystem.Data.Domain.Enums;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Animal
    {
        public Animal()
        {
            Procedures = new HashSet<Procedure>();
        }

        public int AnimalId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public double Weight { get; set; }
        public AnimalType AnimalType { get; set; }
        //public AnimalCategory AnimalCategory { get; set; }


        public ICollection<Procedure> Procedures { get; set; }

        public int AnimalOwnerId { get; set; }
        public AnimalOwner AnimalOwner { get; set; } = null!;
    }
}
