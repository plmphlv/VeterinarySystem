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
        public string Barcode { get; set; } = null!;
        //A barcode number based on the European Article Number (EAN)
        //standard describing barcode symbology and numbering system used in global trade to identify a specific retail product type


        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; } = null!;
    }
}