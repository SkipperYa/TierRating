﻿using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRegistrationService
	{
		Task<User> Registration(string email, string userName, string password, CancellationToken cancellationToken);
	}
}
