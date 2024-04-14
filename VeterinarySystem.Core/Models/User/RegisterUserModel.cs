﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinarySystem.Core.Models.User
{
	public class RegisterUserModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
	}
}
