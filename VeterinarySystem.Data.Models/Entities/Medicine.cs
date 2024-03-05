using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Medicine
    {
        public Medicine()
        {
            PrescriptionMedicines =new HashSet<PrescriptionMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.MedicineNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int MedicineCategoryId { get; set; }

        [ForeignKey(nameof(MedicineCategoryId))]
        public MedicineCategory Category { get; set; } = null!;

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.MedicineProducerNameMaxLength)]
        public string Producer { get; set; } = null!;

        public string Barcode { get; set; } = null!;
        //A barcode number based on the European Article Number (EAN)
        //standard describing barcode symbology and numbering system used in global trade to identify a specific retail product type

        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }
}