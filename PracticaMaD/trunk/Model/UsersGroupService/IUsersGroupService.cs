﻿using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public interface IUsersGroupService
    {
        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        [Transactional]
        void Create(UsersGroup usersGroup);

        [Transactional]
        void RemoveUserFromGroup(UsersGroup usersGroup, UserProfile userP);

        [Transactional]
        void AddUserToGroup(UsersGroup usersGroup, UserProfile userP);
    }
}
