﻿using Common.Models;

namespace Prescriptions.Prescription
{
    public class PrescriptionFormModel
    {
        public string Number { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string StaffMemberId { get; set; } = string.Empty;
        public ICollection<StaffServiceModel> Staff { get; set; } = null!;
    }
}
