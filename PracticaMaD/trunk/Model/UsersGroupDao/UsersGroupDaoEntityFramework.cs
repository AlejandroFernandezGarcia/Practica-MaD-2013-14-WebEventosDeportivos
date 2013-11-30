using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao
{
    /// <summary>
    /// The DAO implementation of UsersGroup entity.
    /// </summary>
    internal class UsersGroupDaoEntityFramework : GenericDaoEntityFramework<UsersGroup, Int64>, IUsersGroupDao
    {
        /// <summary>
        /// Returns a list of groups which the user is member of. If the user
        /// is not member of any group, an empty list is returned.
        /// </summary>
        /// <param name="userProfile"></param>
        /// <param name="startIndex">the index of the first account to return (starting in 0)</param>
        /// <param name="count">the maximum number of accounts to return</param>
        /// <returns>
        /// the list of accounts
        /// </returns>
        public List<UsersGroup> FindByUserId(UserProfile userProfile, int startIndex, int count)
        {
            return userProfile.UsersGroup.Skip(startIndex).Take(count).ToList();
        }

        /// <summary>
        /// Returns the number of groups which the user is member of.
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>
        /// the number of groups
        /// </returns>
        public int GetNumberOfUserGroups(UserProfile userProfile)
        {
            return userProfile.UsersGroup.Count();
        }


        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        public List<UsersGroup> FindAllGroups()
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";

            return this.Context.CreateQuery<UsersGroup>(query).ToList();
        }

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public List<UsersGroup> FindByUserId(UserProfile userProfile)
        {
            return userProfile.UsersGroup.ToList();
        }


        /// <summary>
        /// Gets the number of users for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public int GetNumberOfUsersForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);

            return usersGroup.UserProfile.Count();
        }

        /// <summary>
        /// Gets the number of recommendations for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public int GetNumberOfRecommendationsForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);

            return usersGroup.Recommendation.Count();
        }

        /// <summary>
        /// Determines whether [is users belong group] [the specified users groups].
        /// </summary>
        /// <param name="usersGroups">The users groups.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public bool IsUsersBelongGroup(UsersGroup usersGroups, UserProfile userProfile)
        {
            return usersGroups.UserProfile.ToList().Contains(userProfile);
        }
    }
}