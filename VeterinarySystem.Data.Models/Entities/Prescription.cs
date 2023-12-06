using System;
using System.Collections.Generic;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Prescription
    {
        public Prescription()
        {
            Medicines = new HashSet<Medicine>();
        }

        public int PrescriptionId { get; set; }
        public string? Description { get; set; }
        public DateTime IssueDate { get; set; }

        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; } = null!;

        public ICollection<Medicine> Medicines { get; set; }

    }
}