using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class StaffType
    {
        public StaffType()
        {
            StaffMembers = new HashSet<StaffMember>();
        }

        [Key]
        public int Id { get; set; }

        [Required,
         Unicode(true),
         MaxLength(EntityConstants.StaffPositionNameMaxLength)]
        public string StaffPositionName { get; set; } = null!;

        public ICollection<StaffMember> StaffMembers { get; set; }
    }
}
