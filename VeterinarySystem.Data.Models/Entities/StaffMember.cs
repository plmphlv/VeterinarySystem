using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class StaffMember : IdentityUser
    {
        public StaffMember()
        {
            Appointments = new HashSet<Appointment>();
            Procedures = new HashSet<Procedure>();
        }

        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string PhoneNumter { get; set; } = null!;
        public StaffType StaffType { get; set; }
        public string StaffPositionName { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Procedure> Procedures { get; set; }
    }
}