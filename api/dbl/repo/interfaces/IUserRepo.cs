using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface IUserRepo
    {
        void Add(User item);
        void Remove(int id);
        void Update(User item);
        User FindByID(int id);
        IEnumerable<User> FindAll();
        void AddNewUser(UserViewModel data);
        bool UserLogin(UserLogin login);
    }
}

