﻿using Projeto.Domain.Models;
using Projeto.Service;

namespace Projeto.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<Usuario>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
