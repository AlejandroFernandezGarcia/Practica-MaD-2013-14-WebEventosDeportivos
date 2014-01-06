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
            userProfile.UsersGroup.Load();
            return userProfile.UsersGroup.OrderBy(g => g.id).Skip(startIndex).Take(count).ToList();

            #region Using Entity SQL
            //// Query no tested
            //String query =
            //    "SELECT VALUE g FROM PracticaMaDEntities.UsersGroup AS g " +
            //    "WHERE @userProfileId IN (SELECT VALUE u.id FROM g.UserProfile) " +
            //    "ORDER BY g.id SKIP @skip LIMIT @limit";

            //ObjectParameter[] param = new ObjectParameter[3];
            //param[0] = new ObjectParameter("userProfileId", userProfile.id);
            //param[1] = new ObjectParameter("skip", startIndex);
            //param[2] = new ObjectParameter("limit", count);

            //ObjectResult<UsersGroup> result =
            //    this.Context.CreateQuery<UsersGroup>(query, param).Execute(MergeOption.AppendOnly);

            //return result.ToList();
            #endregion

            #region Using LINQ
            //ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            //var result = (from g in usersGroups
            //              where (from u in g.UserProfile select u.id).Contains(userProfile.id)
            //              select g).Skip(startIndex).Take(count).ToList();

            //return result;
            #endregion
        }

        /// <summary>
        /// Finds a group searching by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public UsersGroup FindByName(string name)
        {
            String query =
                "SELECT VALUE g FROM PracticaMaDEntities.UsersGroup AS g WHERE g.name=@name";

            ObjectParameter param = new ObjectParameter("name", name);

            ObjectResult<UsersGroup> result =
                this.Context.CreateQuery<UsersGroup>(query, param).Include("UserProfile")
                .Include("Recommendation").Execute(MergeOption.AppendOnly);

            UsersGroup usersGroup = null;
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
            userProfile.UsersGroup.Load();
            return userProfile.UsersGroup.Count();

            #region Using LINQ
            //ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            //var result =
            //    (from g in usersGroups
            //     select g).Count();

            //return result;
            #endregion
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        public List<UsersGroup> FindAllGroups()
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";
            return this.Context.CreateQuery<UsersGroup>(query).ToList();

            #region Using LINQ
            //ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            //var result =
            //    (from g in usersGroups
            //     orderby g.id
            //     select g).ToList();

            //return result;
            #endregion
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<UsersGroup> FindAllGroups(int startIndex, int count)
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v ORDER BY v.id SKIP " +
                           startIndex.ToString() + " LIMIT " + count.ToString();

            return this.Context.CreateQuery<UsersGroup>(query).ToList();
        }

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public List<UsersGroup> FindByUserId(UserProfile userProfile)
        {
            userProfile.UsersGroup.Load();
            return userProfile.UsersGroup.ToList();

            #region Using LINQ
            //ObjectSet<UsersGroup> usersGroups = Context.CreateObjectSet<UsersGroup>();

            //var result = (from g in usersGroups
            //              where (from u in g.UserProfile select u.id).Contains(userProfile.id)
            //              orderby g.id
            //              select g).ToList();

            //return result;
            # endregion
        }


        /// <summary>
        /// Gets the number of users for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public int GetNumberOfUsersForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);
            usersGroup.UserProfile.Load();
            return usersGroup.UserProfile.Count;

            # region Using Entity SQL
            //String query =
            //    "SELECT VALUE g FROM PracticaMaDEntities.UsersGroup AS g WHERE g.id=@id";

            //ObjectParameter param = new ObjectParameter("id", usersGroupId);

            //ObjectResult<UsersGroup> result =
            //    this.Context.CreateQuery<UsersGroup>(query, param).Include("UserProfile")
            //    .Execute(MergeOption.AppendOnly);

            //UsersGroup usersGroup = null;
            //try
            //{
            //    usersGroup = result.First<UsersGroup>();
            //}
            //catch (Exception)
            //{
            //    usersGroup = null;
            //}

            //if (usersGroup == null)
            //    throw new InstanceNotFoundException(usersGroupId, typeof(UsersGroup).FullName);
            //return usersGroup.UserProfile.Count;
            # endregion

            #region Using LINQ
            //ObjectSet<UserProfile> userProfiles = Context.CreateObjectSet<UserProfile>();

            //var result = (from u in userProfiles
            //              where (from g in u.UsersGroup select g.id).Contains(usersGroupId)
            //              select u).Count();

            //return result;
            # endregion
        }

        /// <summary>
        /// Gets the number of recommendations for group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public int GetNumberOfRecommendationsForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);
            usersGroup.Recommendation.Load();
            return usersGroup.Recommendation.Count;

            # region Using Entity SQL
            //String query =
            //    "SELECT VALUE g FROM PracticaMaDEntities.UsersGroup AS g WHERE g.id=@id";

            //ObjectParameter param = new ObjectParameter("id", usersGroupId);

            //ObjectResult<UsersGroup> result =
            //    this.Context.CreateQuery<UsersGroup>(query, param).Include("Recommendation")
            //    .Execute(MergeOption.AppendOnly);

            //UsersGroup usersGroup = null;
            //try
            //{
            //    usersGroup = result.First<UsersGroup>();
            //}
            //catch (Exception)
            //{
            //    usersGroup = null;
            //}

            //if (usersGroup == null)
            //    throw new InstanceNotFoundException(usersGroupId, typeof(UsersGroup).FullName);
            //return usersGroup.Recommendation.Count;
            # endregion

            #region Using LINQ
            //ObjectSet<Recommendation> recommendations = Context.CreateObjectSet<Recommendation>();

            //var result = (from r in recommendations
            //              where r.usersGroupId == usersGroupId
            //              select r).Count();

            //return result;
            #endregion
        }

        /// <summary>
        /// Determines whether [is users belong group] [the specified users groups].
        /// </summary>
        /// <param name="usersGroup">The users groups.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public bool IsUsersBelongGroup(UsersGroup usersGroup, UserProfile userProfile)
        {
            usersGroup.UserProfile.Load();
            return usersGroup.UserProfile.ToList().Contains(userProfile);

            #region Using LINQ
            //ObjectSet<UserProfile> userProfiles = Context.CreateObjectSet<UserProfile>();

            //var result = (from u in userProfiles
            //              where u.id == userProfile.id
            //                    && (from g in u.UsersGroup select g.id).Contains(usersGroup.id)
            //              select u).Count();

            //return (result>0);
            # endregion
        }
    }
}