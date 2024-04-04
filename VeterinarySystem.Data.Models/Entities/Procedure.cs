using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Procedure
    {
        public Procedure()
        {
            ProcedureDescription = string.Empty;
        }

        [Key]
        public int Id { get; set; }

        [Required, 
         Unicode(true), 
         MaxLength(EntityConstants.ProcedureNameMaxLength)]
        public string Name { get; set; } = string.Empty;

		[Unicode(true), 
         MaxLength(EntityConstants.DescriptionMaxLength)]
        public string ProcedureDescription { get; set; } = string.Empty;

		[Required]
        public bool IsMedical { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; } = null!;

		[Required]
		public string StaffMemberId { get; set; } = null!;

		[ForeignKey(nameof(StaffMemberId))]
		public StaffMember StaffMember { get; set; } = null!;

        public Prescription? Prescription { get; set; }
	}
}
