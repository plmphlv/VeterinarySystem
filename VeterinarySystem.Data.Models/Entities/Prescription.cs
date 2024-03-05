using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Prescription
    {
        public Prescription()
        {
            PrescriptionMedicines = new HashSet<PrescriptionMedicine>();
            Description = string.Empty;
            IssueDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Unicode(true),
         MaxLength(EntityConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public DateTime IssueDate { get; }

        [Required]
        public int ProcedureId { get; set; }
        [ForeignKey(nameof(ProcedureId))]
        public Procedure Procedure { get; set; } = null!;

        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }

    }
}