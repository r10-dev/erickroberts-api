using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface IUserRoleRepo
    {
        void Add(UserRole item);
        void Remove(int id);
        void Update(UserRole item);
        UserRole FindByID(int id);
        IEnumerable<UserRole> FindAll();
    }
}

