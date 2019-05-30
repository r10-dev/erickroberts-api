using System.Collections.Generic;
using api.models;

namespace api.dbl.repo
{
public interface IAuthorRepo
    {
        void Add(Author item);
        void Remove(int id);
        void Update(Author item);
        Author FindByID(int id);
        IEnumerable<Author> FindAll();
    }
}

