using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileUsersGroupDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public interface IUsersGroupService
    {
        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        [Dependency]
        IUserProfileUsersGroupDao UserProfileUsersGroupDao { set; }

        [Transactional]
        long Create(String name, String description);

        [Transactional]
        void RemoveUserFromGroup(UsersGroup usersGroup, UserProfile userP);

        [Transactional]
        void AddUserToGroup(UsersGroup usersGroup, UserProfile userP);
    }
}
