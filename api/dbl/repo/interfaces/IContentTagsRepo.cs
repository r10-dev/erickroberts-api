using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface IContentTagsRepo
    {
        void Add(ContentTags item);
        void Remove(int id);
        void Update(ContentTags item);
        ContentTags FindByID(int id);
        IEnumerable<ContentTags> FindAll();
    }
}

