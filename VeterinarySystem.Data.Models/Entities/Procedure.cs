using System.Collections.Generic;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Procedure
    {
        public int ProcedureId { get; set; }
        public string Name { get; set; } = null!;
        public string? ProcedureDescription { get; set; }
        public bool IsMedical { get; set; }


        public string StaffMemberId { get; set; } = null!;
        public StaffMember StaffMember { get; set; } = null!;

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public Prescription? Prescription { get; set; }
    }
}
