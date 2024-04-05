using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class StaffMember : IdentityUser
    {
        public StaffMember()
        {
            Appointments = new HashSet<Appointment>();
            Procedures = new HashSet<Procedure>();
        }

        [Required,
         Unicode(true), 
         MaxLength(EntityConstants.HumanNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.HumanNameMaxLength)]
        public string LasttName { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Procedure> Procedures { get; set; }
    }
}