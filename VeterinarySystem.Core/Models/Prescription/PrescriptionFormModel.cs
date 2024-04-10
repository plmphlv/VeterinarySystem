﻿namespace VeterinarySystem.Core.Models.Prescription
{
	public class PrescriptionFormModel
	{
		public string Number { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime IssueDate { get; set; }
		public string StaffMemberId { get; set; } = null!;
	}
}
