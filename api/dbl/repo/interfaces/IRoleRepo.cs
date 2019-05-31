using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface IRoleRepo
    {
        void Add(Role item);
        void Remove(int id);
        void Update(Role item);
        Role FindByID(int id);
        IEnumerable<Role> FindAll();
    }
}

