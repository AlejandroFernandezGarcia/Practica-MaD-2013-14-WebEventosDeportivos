using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    /// <summary>
    /// The interface of the UsersGroup service.
    /// </summary>
    public interface IUsersGroupService
    {
        /// <summary>
        /// Sets the users group DAO.
        /// </summary>
        /// <value>
        /// The users group DAO.
        /// </value>
        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        [Dependency]
        IRecommendationDao RecommendationDao { set; }


        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        long Create(String name, String description, long userProfileId);

        /// <summary>
        /// Removes the user from group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        [Transactional]
        void RemoveUserFromGroup(long usersGroupId, long userProfileId);

        /// <summary>
        /// Removes the user from group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        [Transactional]
        void RemoveUserFromGroup(List<long> usersGroupId, long userProfileId);

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        [Transactional]
        void AddUserToGroup(long usersGroupId, long userProfileId);

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="usersGroupIds">The users group ids.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        [Transactional]
        void AddUserToGroup(List<long> usersGroupIds, long userProfileId);

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        List<UsersGroupDto> FindAllGroups();

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        List<UsersGroupDto> FindByUserId(long userProfileId);

        /// <summary>
        /// Users the belong group.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        bool UserBelongGroup(long userProfileId, long usersGroupId);
    }
}