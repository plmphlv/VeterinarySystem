using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Animal
    {
        public Animal()
        {
            Procedures = new HashSet<Procedure>();
			Prescriptions = new HashSet<Prescription>();
		}

        [Key]
        public int Id { get; set; }

        [MaxLength(EntityConstants.AmnimalNameMaxLength)]
        [Unicode(true)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public int AnimalTypeId { get; set; }

        [ForeignKey(nameof(AnimalTypeId))]
        public AnimalType AnimalType { get; set; }

        [Required]
        public int AnimalOwnerId { get; set; }

        [ForeignKey(nameof(AnimalOwnerId))]
        public AnimalOwner AnimalOwner { get; set; } = null!;

        public ICollection<Procedure> Procedures { get; set; }

		public ICollection<Prescription> Prescriptions { get; set; }
	}
}
