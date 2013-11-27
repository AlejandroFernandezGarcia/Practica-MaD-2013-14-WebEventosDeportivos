using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindByEventId(long id, int startIndex, int count)
        {
            String query =
                "SELECT VALUE c FROM PracticaMaDEntities.Comment AS c WHERE c.eventId = @id ORDER BY c.date DESC";

            ObjectParameter param = new ObjectParameter("id", id);

            List<Comment> result = this.Context.CreateQuery<Comment>(query, param).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Comment> FindByEventId(long id)
        {
            String query =
                "SELECT VALUE c FROM PracticaMaDEntities.Comment AS c WHERE c.eventId = @id ORDER BY c.date DESC";

            ObjectParameter param = new ObjectParameter("id", id);

            List<Comment> result = this.Context.CreateQuery<Comment>(query, param).ToList();

            return result;
        }
    }
}
