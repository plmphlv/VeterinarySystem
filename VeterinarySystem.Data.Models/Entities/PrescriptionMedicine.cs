using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinarySystem.Data.Domain.Entities
{
    [PrimaryKey(nameof(PrescriptionId), nameof(MedicineId))]
    public class PrescriptionMedicine
    {

        [Required]
        public int PrescriptionId { get; set; }

        [ForeignKey(nameof(PrescriptionId))]
        public Prescription Prescription { get; set; } = null!;

        [Required]
        public int MedicineId { get; set; }

        [ForeignKey(nameof(MedicineId))]
        public Medicine Medicine { get; set; } = null!;
    }
}

