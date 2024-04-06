using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Unicode(true)]
        [MaxLength(EntityConstants.DescriptionMaxLength)]
        public string AppointmentDesctiption { get; set; } = string.Empty;

        [Required]
        public int AnimalOwnerId { get; set; }

        [ForeignKey(nameof(AnimalOwnerId))]
        public AnimalOwner AnimalOwner { get; set; } = null!;

        [Required]
        public string StaffMemberId { get; set; } = null!;

        [ForeignKey(nameof(StaffMemberId))]
        public StaffMember StaffMember { get; set; } = null!;
    }
}