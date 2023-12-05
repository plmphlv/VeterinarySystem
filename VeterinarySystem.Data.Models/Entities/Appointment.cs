using System;

namespace VeterinarySystem.Data.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentDesctiption { get; set; }


        public int AnimalOwnerId { get; set; }
        public AnimalOwner AnimalOwner { get; set; } = null!;

        public int StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; } = null!;
    }
}