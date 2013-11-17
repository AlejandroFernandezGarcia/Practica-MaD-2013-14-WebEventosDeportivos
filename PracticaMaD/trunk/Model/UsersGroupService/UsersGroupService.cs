using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileUsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public class UsersGroupService : IUsersGroupService
    {
        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }

        [Dependency]
        public IUserProfileUsersGroupDao UserProfileUsersGroupDao { private get; set; }

        public long Create(String name, String description)
        {
            UsersGroup ug = UsersGroup.CreateUsersGroup(0, name, description);

            UsersGroupDao.Create(ug);

            return ug.id;
        }

        public void RemoveUserFromGroup(UsersGroup usersGroup, UserProfile userP)
        {
            try
            {
                UserProfileUsersGroupDao.RemoveUserFromGroup(usersGroup.id, userP.id);
            }catch(InstanceNotFoundException){
                throw new UserNotBelongGroupException(usersGroup.id, userP.id);
            }
        }

        public void AddUserToGroup(UsersGroup usersGroup, UserProfile userP)
        {
            try
            {
                UserProfileUsersGroupDao.FindByUserIdAndGroupId(usersGroup.id, userP.id);

                throw new DuplicateInstanceException("GroupId: " + usersGroup.id + "UserId: " + userP.id,
                    typeof(UsersGroupService).FullName);
            }
            catch (InstanceNotFoundException)
            {
                UserProfileUsersGroup userProfileUsersGroupNew =
                    UserProfileUsersGroup.CreateUserProfileUsersGroup(0, userP.id, usersGroup.id);

                UserProfileUsersGroupDao.AddUserToGroup(userProfileUsersGroupNew);

            }

            
        }
    }
}
