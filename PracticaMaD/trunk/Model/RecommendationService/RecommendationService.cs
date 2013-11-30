using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    /// <summary>
    /// The implementation of Recommentadion service.
    /// </summary>
    internal class RecommendationService : IRecommendationService
    {
        /// <summary>
        /// Gets or sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        [Dependency]
        public IRecommendationDao RecommendationDao { private get; set; }

        /// <summary>
        /// Gets or sets the users group DAO.
        /// </summary>
        /// <value>
        /// The users group DAO.
        /// </value>
        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }

        /// <summary>
        /// Gets or sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Dependency]
        public IEventDao EventDao { private get; set; }

        /// <summary>
        /// Gets or sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        /// <summary>
        /// Creates the specified event identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="usersGroupIds">The users group ids.</param>
        /// <param name="text">The text.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <exception cref="UserNotBelongGroupException"></exception>
        public void Create(long eventId, List<long> usersGroupIds, string text, long userProfileId)
        {
            List<UsersGroup> list = UsersGroupDao.FindByUserId(UserProfileDao.Find(userProfileId));

            List<long> listOfIds = new List<long>();

            foreach (UsersGroup j in list)
            {
                listOfIds.Add(j.id);
            }

            foreach (long i in usersGroupIds)
            {
                if (listOfIds.Contains(i) == false)
                {
                    throw new UserNotBelongGroupException(i, userProfileId);
                }
            }


            foreach (long i in usersGroupIds)
            {
                DateTime date = new DateTime();
                byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

                Recommendation recommendation = Recommendation.CreateRecommendation(0, text, eventId, i, dateBytes);

                RecommendationDao.Create(recommendation);
            }
        }

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsForEvent(long eventId)
        {
            return RecommendationDao.FindRecommendationsForEvent(EventDao.Find(eventId));
        }

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsForEvent(long eventId, int startIndex, int count)
        {
            return RecommendationDao.FindRecommendationsForEvent(EventDao.Find(eventId), startIndex, count);
        }

        /// <summary>
        /// Finds the recommendations received by user group of user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsReceivedByUserGroupOfUser(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            return RecommendationDao.FindRecommendationForAnUserUsersGroup(userProfile);
        }

        /// <summary>
        /// Finds the recommendations received by user group of user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsReceivedByUserGroupOfUser(long userProfileId, int startIndex,
                                                                                 int count)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            return RecommendationDao.FindRecommendationForAnUserUsersGroup(userProfile, startIndex, count);
        }
    }
}