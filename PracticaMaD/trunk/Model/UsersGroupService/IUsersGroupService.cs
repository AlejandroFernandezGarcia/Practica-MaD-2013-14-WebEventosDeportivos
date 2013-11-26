using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public interface IUsersGroupService
    {
        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        [Transactional]
        long Create(String name, String description);

        [Transactional]
        void RemoveUserFromGroup(long usersGroupId, long userProfileId);

        [Transactional]
        void AddUserToGroup(long usersGroupId, long userProfileId);
    }
}
