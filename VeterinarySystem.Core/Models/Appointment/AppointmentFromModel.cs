﻿using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Models.Appointment
{
	public class AppointmentFromModel
	{
		[Required(ErrorMessage = ErrorMessages.RequiredError)]
		public DateTime Date { get; set; }

		public string Desctiption { get; set; } = string.Empty;

		public string StaffMemberId { get; set; } = null!;

		public ICollection<StaffServiceModel> Staff = new List<StaffServiceModel>();
	}
}
