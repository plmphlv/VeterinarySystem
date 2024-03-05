using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class MedicineCategory
    {
        [Key]
        public int Id { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.MedicineNameMaxLength)]
        public string MedicineCategoryName { get; set; } = null!;
    }
}
