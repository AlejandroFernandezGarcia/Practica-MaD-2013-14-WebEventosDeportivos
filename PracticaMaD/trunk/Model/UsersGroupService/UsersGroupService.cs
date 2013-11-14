using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileUsersGroupDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public class UsersGroupService : IUsersGroupService
    {
        [Dependency]
        IUserProfileDao UserProfileDao { private get;  set; }

        [Dependency]
        IUsersGroupDao UsersGroupDao { private get; set; }

        [Dependency]
        IUserProfileUsersGroupDao UserProfileUsersGroupDao { private get; set; }

        public void Create(UsersGroup usersGroup)
        {
            UsersGroupDao.Create(usersGroup);
        }

        public void RemoveUserFromGroup(UsersGroup usersGroup, UserProfile userP)
        {
            try
            {
                UserProfileUsersGroupDao.RemoveUserFromGroup(usersGroup.id, userP.id);
            }catch(InstanceNotFoundException){
                throw new UsersBelongGroupException(usersGroup.id, userP.id);
            }
        }

        public void AddUserToGroup(UsersGroup usersGroup, UserProfile userP)
        {
            try
            {
                UserProfileUsersGroupDao.FindByUserIdAndGroupId(usersGroup.id, userP.id);
            }
            catch (InstanceNotFoundException)
            {
                UserProfileUsersGroup userProfileUsersGroupNew =
                    UserProfileUsersGroup.CreateUserProfileUsersGroup(0, userP.id, usersGroup.id);

                UserProfileUsersGroupDao.AddUserToGroup(userProfileUsersGroupNew);

                return;//puede que falle
            }

            throw new DuplicateInstanceException("GroupId: "+usersGroup.id+"UserId: "+userP.id,
                    typeof(UsersGroupService).FullName);
        }
    }
}
