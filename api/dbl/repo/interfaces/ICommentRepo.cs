using System.Collections.Generic;
using api.models;

namespace api.dbl.repo.interfaces
{
public interface ICommentRepo
    {
        void Add(Comment item);
        void Remove(int id);
        void Update(Comment item);
        Comment FindByID(int id);
        IEnumerable<Comment> FindAll();
    }
}

