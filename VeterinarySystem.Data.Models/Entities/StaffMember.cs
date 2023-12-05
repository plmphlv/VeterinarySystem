using System.Collections.Generic;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class StaffMember
    {
        public StaffMember()
        {
            Appointments = new HashSet<Appointment>();
            Procedures = new HashSet<Procedure>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int StaffMemberId { get; set; }
        public string StaffMemberFirstName { get; set; } = null!;
        public string StaffMemberLasttName { get; set; } = null!;
        public string PhoneNumter { get; set; } = null!;
        public StaffType StaffType { get; set; }
        public string StaffPositionName { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Procedure> Procedures { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}