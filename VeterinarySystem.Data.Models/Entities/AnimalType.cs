using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class AnimalType
    {
        public AnimalType()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        public int Id { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.AnomalTypeNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Animal> Animals { get; set; }
    }
}
