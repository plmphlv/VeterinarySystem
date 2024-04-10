﻿using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class ProcedureService : IProcedureService
	{
		private readonly VeterinarySystemDbContext data;

		public ProcedureService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public async Task<int> CreateNewProcetude(int id, ProcedureFormModel model)
		{
			Procedure procedure = new Procedure()
			{
				Name = model.Name,
				Description = model.Description,
				Date = model.Date,
				AnimalId = id,
				StaffMemberId = model.StaffMemberId,
			};

			data.Procedures.Add(procedure);
			await data.SaveChangesAsync();

			return procedure.Id;
		}

		public async Task<int> DeleteProcetude(int id)
		{
			Procedure procedure = await data.Procedures
				.FirstOrDefaultAsync(x => x.Id == id);

			int entityId = procedure.AnimalId;

			data.Procedures.Remove(procedure);
			await data.SaveChangesAsync();

			return entityId;
		}

		public async Task EditProcetude(ProcedureFormModel model, int id)
		{
			Procedure procedure = await data.Procedures
				.FirstOrDefaultAsync(p => p.Id == id);

			procedure.Name = model.Name;
			procedure.Description = model.Description;
			procedure.Date = model.Date;
			procedure.StaffMemberId = model.StaffMemberId;

			await data.SaveChangesAsync();
		}

		public async Task<ProcedureServiceModel> GetProcetudeDetails(int id)
		{
			ProcedureServiceModel? model = await data.Procedures
				.AsNoTracking()
				.Where(p => p.Id == id)
				.Select(p => new ProcedureServiceModel()
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Date = p.Date.ToString(EntityConstants.DateFormat),
					AnimalName = p.Animal.Name,
					StaffMemberFullName = $"{p.StaffMember.FirstName} {p.StaffMember.LastName}"
				})
				.FirstOrDefaultAsync();

			return model;
		}

		public async Task<bool> ProcedureExists(int id)
		{
			bool result = await data.Procedures
				.AsNoTracking()
				.AnyAsync(x => x.Id == id);

			return result;
		}

		public async Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			ICollection<StaffServiceModel> staff = await data.Users
				.AsNoTracking()
				.Where(u => u.Email != AdminUser.AdminEmail)
				.Select(u => new StaffServiceModel()
				{
					StaffId = u.Id,
					StaffName = $"{u.FirstName} {u.LastName}"
				})
				.ToListAsync();

			return staff;
		}

		public async Task<ProcedureFormModel> GetEditingForm(int id)
		{
			ProcedureFormModel? form = await data.Procedures
				.AsNoTracking()
				.Where(procedure => procedure.Id == id)
				.Select(procedure => new ProcedureFormModel()
				{
					Name = procedure.Name,
					Description = procedure.Description,
					Date = procedure.Date,
					StaffMemberId = procedure.StaffMemberId
				}).FirstOrDefaultAsync();

			return form;
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.Procedures
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Name = e.Name,
					Controller = controllerName
				}
			).FirstOrDefaultAsync();

			return model;
		}
	}
}
