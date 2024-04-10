using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
	public class Prescription
	{
		public Prescription()
		{
			Description = string.Empty;
			IssueDate = DateTime.Now;
		}

		[Key]
		public int Id { get; set; }

		[Required,
		 MaxLength(EntityConstants.PrescriptionNumberMaxLenght)]
		public string Number { get; set; } = string.Empty;

		[Unicode(true),
		 MaxLength(EntityConstants.DescriptionMaxLength)]
		public string Description { get; set; }

		[Required]
		public DateTime IssueDate { get; set; }

		[Required]
		public int AnimalId { get; set; }

		[ForeignKey(nameof(AnimalId))]
		public Animal Animal { get; set; } = null!;

		[Required]
		public string StaffMemberId { get; set; }

		[ForeignKey(nameof(StaffMemberId))]
		public StaffMember StaffMember { get; set; } = null!;
	}
}