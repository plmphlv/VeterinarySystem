using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinarySystem.Core.Contracts
{
	public interface IAppointmentService
	{
		Task AddAppointment();

		Task RemoveAppointment();

		Task EditAppointment();
	}
}
