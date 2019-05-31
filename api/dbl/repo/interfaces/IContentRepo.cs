using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface IContentRepo
    {
        void Add(Content item);
        void Remove(int id);
        void Update(Content item);
        Content FindByID(int id);
        IEnumerable<Content> FindAll();
    }
}

