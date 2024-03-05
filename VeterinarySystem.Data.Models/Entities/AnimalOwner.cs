using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class AnimalOwner
    {
        public AnimalOwner()
        {
            Animals = new HashSet<Animal>();
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int AnimalOwnerId { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.HumanNameMaxLength)]
        public string OwnerFirstName { get; set; } = null!;

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.HumanNameMaxLength)]
        public string OwnerLastName { get; set; } = null!;

        [Required,
        MaxLength(EntityConstants.PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Unicode(true),
         MaxLength(EntityConstants.PetOwnerAddresMaxLenght)]
        public string? Address { get; set; }

        public ICollection<Animal> Animals { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}