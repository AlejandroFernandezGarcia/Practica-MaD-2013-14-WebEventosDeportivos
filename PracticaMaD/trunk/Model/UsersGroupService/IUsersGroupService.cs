using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    public interface IUsersGroupService
    {
        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        [Dependency]
        IRecommendationDao RecommendationDao { set; }


        [Transactional]
        long Create(String name, String description, long userProfileId);

        [Transactional]
        void RemoveUserFromGroup(long usersGroupId, long userProfileId);

        [Transactional]
        void RemoveUserFromGroup(List<long> usersGroupId, long userProfileId);

        [Transactional]
        void AddUserToGroup(long usersGroupId, long userProfileId);

        [Transactional]
        void AddUserToGroup(List<long> usersGroupIds, long userProfileId);

        List<UsersGroupDto> FindAllGroups();

        List<UsersGroupDto> FindByUserId(long userProfileId);

        bool UserBelongGroup(long userProfileId, long usersGroupId);
    }
}
