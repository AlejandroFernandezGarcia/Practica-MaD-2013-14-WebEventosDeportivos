using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao
{
    /// <summary>
    /// The DAO interface of UsersGroup entity.
    /// </summary>
    public interface IUsersGroupDao : IGenericDao<UsersGroup, Int64>
    {
        /// <summary>
        /// Returns a list of groups which the user is member of. If the user
        /// is not member of any group, an empty list is returned.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="startIndex">the index of the first account to return (starting in 0)</param>
        /// <param name="count">the maximum number of accounts to return</param>
        /// <returns>
        /// the list of accounts
        /// </returns>
        List<UsersGroup> FindByUserId(UserProfile userProfile, int startIndex, int count);

        /// <summary>
        /// Finds a group searching by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        UsersGroup FindByName(string name);

        /// <summary>
        /// Returns the number of groups which the user is member of.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns>
        /// the number of groups
        /// </returns>
        int GetNumberOfUserGroups(UserProfile userProfile);

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        List<UsersGroup> FindAllGroups();

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<UsersGroup> FindAllGroups(int startIndex, int count);

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        List<UsersGroup> FindByUserId(UserProfile userProfile);

        /// <summary>
        /// Gets the number of users for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        int GetNumberOfUsersForGroup(long usersGroupId);

        /// <summary>
        /// Gets the number of recommendations for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        int GetNumberOfRecommendationsForGroup(long usersGroupId);

        /// <summary>
        /// Determines whether [is users belong group] [the specified users groups].
        /// </summary>
        /// <param name="usersGroups">The users groups.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        bool IsUsersBelongGroup(UsersGroup usersGroups, UserProfile userProfile);
    }
}