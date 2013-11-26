using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao
{
    public interface IUsersGroupDao : IGenericDao<UsersGroup, Int64>
    {
        /// <summary>
        /// Returns a list of groups which the user is member of. If the user
        /// is not member of any group, an empty list is returned.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="startIndex">the index of the first account to return (starting in 0)</param>
        /// <param name="count">the maximum number of accounts to return</param>
        /// <returns>the list of accounts</returns>
        List<UsersGroup> FindByUserId(long userId, int startIndex, int count);

        /// <summary>
        /// Returns the number of groups which the user is member of.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <returns>the number of groups</returns>
        int GetNumberOfUserGroups(long userId);

        void RemoveUserFromGroup(List<long> usersGroupIds, long userProfileId);

        void AddUserToGroup(List<long> usersGroupIds, long userProfileId);

        List<UsersGroup> FindAllGroups();

        List<UsersGroup> FindAllGroupsOfUser(long userProfileId);
    }
}
