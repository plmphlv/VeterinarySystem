﻿using static System.Runtime.InteropServices.JavaScript.JSType;
using VeterinarySystem.Data.DataSeeding.Admin;
using Prescriptions.Prescription;
using Common.Common;
using Common.Models;
using Common.Infrastructure;

namespace Prescriptions.Contracts
{
    public interface IPrescriptionService
    {
        Task<int> AddPrescription(PrescriptionFormModel form, int animalId);//C

        Task<PrescriptionServiceModel> GetPrescriptionDetails(int prescriptionId);

        Task EditPrescription(int prescriptionId, PrescriptionFormModel form);

        Task<int> DeletePrescription(int prescriptionId);

        Task<bool> PrescriptionExists(int prescriptionId);

        Task<ICollection<StaffServiceModel>> GetStaffMembers();

        Task<PrescriptionFormModel> GetFormForEditing(int prescriptionId);

        Task<PrescriptionFormModel> GetNewPrescriptionForm();

        Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName);

        Task<string> GetPrescriptionNumber();

        Task<string> CheckPrescriptionNumber(int id);

        Task<DateTime> CheckPrescriptionDate(int id);

        Task<PrescriptionsQueryServiceModel> GetPrescriptionHistory(int animalId,
            MedicalHistoryOrder PrescriptionsOrder = MedicalHistoryOrder.Newest,
            int currentPage = 1,
            int prescriptionsPerPage = 1);
    }
}