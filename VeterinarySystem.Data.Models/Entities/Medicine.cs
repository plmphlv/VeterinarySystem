using System;
using VeterinarySystem.Data.Domain.Enums;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public MedicineCategory Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Producer { get; set; } = null!;


        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; } = null!;
    }
}