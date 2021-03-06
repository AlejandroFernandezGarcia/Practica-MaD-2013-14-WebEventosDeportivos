﻿using System;
using System.Runtime.Remoting.Contexts;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    /// <summary>
    /// The implementation of the UsersGroup service.
    /// </summary>
    public class UsersGroupService : IUsersGroupService
    {
        /// <summary>
        /// Gets or sets the users group DAO.
        /// </summary>
        /// <value>
        /// The users group DAO.
        /// </value>
        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }

        /// <summary>
        /// Gets or sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>
        /// Gets or sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        [Dependency]
        public IRecommendationDao RecommendationDao { private get; set; }

        /// <summary>
        /// Removes the user from group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <exception cref="Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions.UserNotBelongGroupException"></exception>
        public void RemoveUserFromGroup(long usersGroupId, long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".RemoveUserFromGroup(usersGroupId=" + usersGroupId + ",userProfileId=" + userProfileId + ") used.", MessageType.Info);

            UsersGroup ug = UsersGroupDao.Find(usersGroupId);
            UserProfile up = UserProfileDao.Find(userProfileId);

            if (!UsersGroupDao.IsUsersBelongGroup(ug, up))
            {
                LogManager.RecordMessage("new UserNotBelongGroupException(usersGroupId=" + usersGroupId + ",userProfileId=" + userProfileId + ") thrown.", MessageType.Info);
                throw new UserNotBelongGroupException(usersGroupId, userProfileId);
            }

            ug.UserProfile.Load();
            ug.UserProfile.Remove(up);
            up.UsersGroup.Load();
            up.UsersGroup.Remove(ug);

            UsersGroupDao.Update(ug);
            UserProfileDao.Update(up);
        }

        /// <summary>
        /// Removes the user from group.
        /// </summary>
        /// <param name="usersGroupIds">The users group ids.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        public void RemoveUserFromGroup(List<long> usersGroupIds, long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".RemoveUserFromGroup(usersGroupIds=" + usersGroupIds + ",userProfileId=" + userProfileId + ") used.", MessageType.Info);

            foreach (long i in usersGroupIds)
            {
                RemoveUserFromGroup(i, userProfileId);
            }
        }

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException">UsersGroupSevice</exception>
        public void AddUserToGroup(long usersGroupId, long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".AddUserToGroup(long usersGroupId=" + usersGroupId + ",userProfileId=" + userProfileId + ") used.", MessageType.Info);

            UsersGroup ug = UsersGroupDao.Find(usersGroupId);

            List<UserProfile> listOfUsers = ug.UserProfile.ToList();

            UserProfile up = UserProfileDao.Find(userProfileId);

            if (listOfUsers.Contains(up))
            {
                LogManager.RecordMessage("new DuplicateInstanceException(userProfile=" + up + ",UsersGroupSevice) thrown.", MessageType.Info);
                throw new DuplicateInstanceException(up, "UsersGroupSevice");
            }

            ug.UserProfile.Add(up);
            up.UsersGroup.Add(ug);

            UsersGroupDao.Update(ug);
            UserProfileDao.Update(up);
        }

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="usersGroupIds">The users group ids.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        public void AddUserToGroup(List<long> usersGroupIds, long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".AddUserToGroup(long usersGroupIds=" + usersGroupIds + ",userProfileId=" + userProfileId + ") used.", MessageType.Info);

            foreach (long i in usersGroupIds)
            {
                AddUserToGroup(i, userProfileId);
            }
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException"></exception>
        public long Create(string name, string description, long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".Create(name="+name+",description=" + description + ",userProfileId=" + userProfileId + ") used.", MessageType.Info);

            try
            {
                UsersGroupDao.FindByName(name);

                LogManager.RecordMessage("new DuplicateInstanceException(name=" + name + ",XXX) thrown.", MessageType.Info);
                throw new DuplicateInstanceException(name,
                    typeof(UsersGroup).FullName);
            }
            catch (InstanceNotFoundException)
            {
                UsersGroup ug = UsersGroup.CreateUsersGroup(0, name, description);

                UsersGroupDao.Create(ug);

                AddUserToGroup(ug.id, userProfileId);

                return ug.id;
            }
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <returns></returns>
        public List<UsersGroupDto> FindAllGroups()
        {
            LogManager.RecordMessage(this.GetType().Name + ".FindAllGroups() used.", MessageType.Info);

            List<UsersGroup> listOfGroups = UsersGroupDao.FindAllGroups();

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups)
            {
                result.Add(new UsersGroupDto(i, UsersGroupDao.GetNumberOfUsersForGroup(i.id),
                                             UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        /// <summary>
        /// Finds all groups.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<UsersGroupDto> FindAllGroups(int startIndex, int count)
        {
            LogManager.RecordMessage(this.GetType().Name + ".FindAllGroups(startIndex=" + startIndex + ",count=" + count + ") used.", MessageType.Info);

            List<UsersGroup> listOfGroups = UsersGroupDao.FindAllGroups(startIndex, count);

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups)
            {
                result.Add(new UsersGroupDto(i, UsersGroupDao.GetNumberOfUsersForGroup(i.id),
                                             UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        public List<UsersGroupDto> FindByUserId(long userProfileId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".FindByUserId(userProfileId=" + userProfileId + ") used.", MessageType.Info);

            List<UsersGroup> listOfGroups = UsersGroupDao.FindByUserId(UserProfileDao.Find(userProfileId));

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups)
            {
                result.Add(new UsersGroupDto(i, UsersGroupDao.GetNumberOfUsersForGroup(i.id),
                                             UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<UsersGroupDto> FindByUserId(long userProfileId, int startIndex, int count)
        {
            LogManager.RecordMessage(this.GetType().Name + ".FindByUserId(userProfileId=" + userProfileId + ",startIndex=" + startIndex + ",count=" + count + ") used.", MessageType.Info);

            List<UsersGroup> listOfGroups = UsersGroupDao.FindByUserId(
                UserProfileDao.Find(userProfileId), startIndex, count);

            List<UsersGroupDto> result = new List<UsersGroupDto>();

            foreach (UsersGroup i in listOfGroups)
            {
                result.Add(new UsersGroupDto(i, UsersGroupDao.GetNumberOfUsersForGroup(i.id),
                                             UsersGroupDao.GetNumberOfRecommendationsForGroup(i.id)));
            }

            return result;
        }

        /// <summary>
        /// Users the belong group.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public bool UserBelongGroup(long userProfileId, long usersGroupId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".UserBelongGroup(userProfileId=" + userProfileId + ",usersGroupId=" + usersGroupId + ") used.", MessageType.Info);

            return UsersGroupDao.IsUsersBelongGroup(UsersGroupDao.Find(usersGroupId), UserProfileDao.Find(userProfileId));
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="usersGroupId">The users group identifier.</param>
        /// <returns></returns>
        public UsersGroup FindById(long usersGroupId)
        {
            LogManager.RecordMessage(this.GetType().Name + ".FindById(usersGroupId="+usersGroupId+") used.", MessageType.Info);

            return UsersGroupDao.Find(usersGroupId);
        }
    }
}