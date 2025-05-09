﻿using Journal.Domain;

namespace Journal.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByEmail(string email);
    }
}
