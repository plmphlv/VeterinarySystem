using System.Collections.Generic;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class AnimalOwner
    {
        public AnimalOwner()
        {
            Animals = new HashSet<Animal>();
            Appointments = new HashSet<Appointment>();
        }

        public int AnimalOwnerId { get; set; }
        public string OwnerFirstName { get; set; } = null!;
        public string OwnerLastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }


        public ICollection<Animal> Animals { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}