using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
	public class Procedure
	{
		[Key]
		public int Id { get; set; }

		[Required,
		 Unicode(true),
		 MaxLength(EntityConstants.ProcedureNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		[Unicode(true),
		 Required,
		 MaxLength(EntityConstants.DescriptionMaxLength)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public DateTime Date { get; set; }

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
