using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

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
            //return userProfile.UsersGroup.Skip(startIndex).Take(count).ToList();

            ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            var result = (from g in usersGroups
                          where (from u in g.UserProfile select u.id).Contains(userProfile.id)
                          select g).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <summary>
        /// Finds a group searching by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public UsersGroup FindByName(string name)
        {
            UsersGroup usersGroup = null;

            String query =
                "SELECT VALUE g FROM PracticaMaDEntities.UsersGroup AS g WHERE g.name=@name";

            ObjectParameter param = new ObjectParameter("name", name);

            ObjectResult<UsersGroup> result =
                this.Context.CreateQuery<UsersGroup>(query, param).Execute(MergeOption.AppendOnly);

            try
            {
                usersGroup = result.First<UsersGroup>();
            }
            catch (Exception)
            {
                usersGroup = null;
            }

            if (usersGroup == null)
                throw new InstanceNotFoundException(name, typeof(UsersGroup).FullName);
            return usersGroup;
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
            ////Context.LoadProperty(userProfile, "UsersGroup");
            //return userProfile.UsersGroup.Count();

            ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            var result =
                (from g in usersGroups
                 select g).Count();

            return result;
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        public List<UsersGroup> FindAllGroups()
        {
            ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            var result =
                (from g in usersGroups
                 orderby g.id
                 select g).ToList();

            return result;

            //String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";
            //return this.Context.CreateQuery<UsersGroup>(query).ToList();
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<UsersGroup> FindAllGroups(int startIndex, int count)
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";

            return this.Context.CreateQuery<UsersGroup>(query).Skip(startIndex).Take(count).ToList();
        }

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public List<UsersGroup> FindByUserId(UserProfile userProfile)
        {
            ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            var result = (from g in usersGroups
                          where (from u in g.UserProfile select u.id).Contains(userProfile.id)
                          orderby g.id
                          select g).ToList();

            return result;

            ////Context.LoadProperty(userProfile, "UsersGroup");
            //return userProfile.UsersGroup.ToList();
        }


        /// <summary>
        /// Gets the number of users for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public int GetNumberOfUsersForGroup(long usersGroupId)
        {
            ObjectSet<UserProfile> userProfiles = Context.CreateObjectSet<UserProfile>();

            var result = (from u in userProfiles
                          where (from g in u.UsersGroup select g.id).Contains(usersGroupId)
                          select u).Count();

            return result;
        }

        /// <summary>
        /// Gets the number of recommendations for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public int GetNumberOfRecommendationsForGroup(long usersGroupId)
        {
            ObjectSet<Recommendation> recommendations = Context.CreateObjectSet<Recommendation>();

            var result = (from r in recommendations
                          where r.usersGroupId == usersGroupId
                          select r).Count();

            return result;
        }

        /// <summary>
        /// Determines whether [is users belong group] [the specified users groups].
        /// </summary>
        /// <param name="usersGroup">The users groups.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public bool IsUsersBelongGroup(UsersGroup usersGroup, UserProfile userProfile)
        {
            ObjectSet<UserProfile> userProfiles = Context.CreateObjectSet<UserProfile>();

            var result = (from u in userProfiles
                          where u.id == userProfile.id
                                && (from g in u.UsersGroup select g.id).Contains(usersGroup.id)
                          select u).Count();

            return (result>0);

            ////Context.LoadProperty(usersGroups, "UserProfile");
            //return usersGroups.UserProfile.ToList().Contains(userProfile);
        }
    }
}