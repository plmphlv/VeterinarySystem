using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Data;

namespace VeterinarySystem.Core.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly VeterinarySystemDbContext data;

		public AppointmentService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public Task AddAppointment()
		{
			throw new NotImplementedException();
		}

		public Task EditAppointment()
		{
			throw new NotImplementedException();
		}

		public Task RemoveAppointment()
		{
			throw new NotImplementedException();
		}
	}
}
