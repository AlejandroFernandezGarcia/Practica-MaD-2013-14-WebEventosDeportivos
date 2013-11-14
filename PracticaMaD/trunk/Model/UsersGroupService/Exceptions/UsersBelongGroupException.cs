using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions
{
    public class UsersBelongGroupException : Exception
    {
        public long GroupId { get; private set; }
        public long UserId { get; private set; }

        public UsersBelongGroupException(long groupId, long userId)
            : base("The user with the id:"+userId + "not belong to the group with id:"+groupId)
        {
            this.GroupId = groupId;
            this.UserId = userId;
        }
    }
}
