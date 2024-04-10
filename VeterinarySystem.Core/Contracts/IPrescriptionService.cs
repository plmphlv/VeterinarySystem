﻿using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Models.StaffMember;

namespace VeterinarySystem.Core.Contracts
{
	public interface IPrescriptionService
	{
		Task<int> AddPrescription(PrescriptionFormModel form, int ownerId);//C

		Task<PrescriptionServiceModel> GetPrescriptionDetails(int prescriptionId);

		Task EditPrescription(int appontmentId, AppointmentFromModel form);

		Task<int> DeletePrescription(int prescriptionId);

		Task<bool> PrescriptionExists(int prescriptionId);

		Task<ICollection<StaffServiceModel>> GetStaffMembers();

		Task<PrescriptionFromModel> GetFormForEditing(int prescriptionId);

		Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

		Task<int> GetNewPrescriptionNumber();

		Task<int> GetCurremtPrescriptionNumber();
	}
}
