using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindByEventId(long id, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
    }
}
