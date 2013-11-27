using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public class UsersGroupService : IUsersGroupService
    {
        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }

        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Dependency]
        public IRecommendationDao RecommendationDao { private get; set; }

        public void RemoveUserFromGroup(long usersGroupId, long userProfileId)
        {
            UsersGroup ug = UsersGroupDao.Find(usersGroupId);

            List<UserProfile> listOfUsers = ug.UserProfile.ToList();

            UserProfile up = UserProfileDao.Find(userProfileId);

            if (!listOfUsers.Contains(up))
            {
                throw new UserNotBelongGroupException(usersGroupId, userProfileId);
            }

            ug.UserProfile.Remove(up);
            up.UsersGroup.Remove(ug);

            UsersGroupDao.Update(ug);
            UserProfileDao.Update(up);

        }

        public void RemoveUserFromGroup(List<long> usersGroupIds, long userProfileId)
        {
            foreach (long i in usersGroupIds)
            {
                RemoveUserFromGroup(i, userProfileId);
            }
        }

        public void AddUserToGroup(long usersGroupId, long userProfileId)
        {
            UsersGroup ug = UsersGroupDao.Find(usersGroupId);

            List<UserProfile> listOfUsers = ug.UserProfile.ToList();

            UserProfile up = UserProfileDao.Find(userProfileId);

            if (listOfUsers.Contains(up))
            {
                throw new DuplicateInstanceException(up, "UsersGroupSevice");
            }

            ug.UserProfile.Add(up);
            up.UsersGroup.Add(ug);

            UsersGroupDao.Update(ug);
            UserProfileDao.Update(up);
        }

        public void AddUserToGroup(List<long> usersGroupIds, long userProfileId)
        {
            foreach (long i in usersGroupIds)
            {
                AddUserToGroup(i, userProfileId);
            }
        }

        public long Create(string name, string description, long userProfileId)
        {
            UsersGroup ug = UsersGroup.CreateUsersGroup(0, name, description);

            UsersGroupDao.Create(ug);

            AddUserToGroup(ug.id, userProfileId);

            return ug.id;
        }

        public List<UsersGroupDto> FindAllGroups()
        {
            List<UsersGroup> listOfGroups = UsersGroupDao.FindAllGroups();

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups) 
            { 
                result.Add(new UsersGroupDto(i,UsersGroupDao.GetNumberOfUsersForGroup(i.id),UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        public List<UsersGroupDto> FindByUserId(long userProfileId)
        {
            List<UsersGroup> listOfGroups = UsersGroupDao.FindByUserId(UserProfileDao.Find(userProfileId));

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups)
            {
                result.Add(new UsersGroupDto(i, UsersGroupDao.GetNumberOfUsersForGroup(i.id), UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        public bool UserBelongGroup(long userProfileId, long usersGroupId)
        {
            return UsersGroupDao.IsUsersBelongGroup(UsersGroupDao.Find(usersGroupId), UserProfileDao.Find(userProfileId));
        }


        
    }
}
