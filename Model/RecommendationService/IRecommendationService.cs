using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    /// <summary>
    /// The interface of Recommendation service.
    /// </summary>
    public interface IRecommendationService
    {
        /// <summary>
        /// Sets the recommendation DAO.
        /// </summary>
        /// <value>
        /// The recommendation DAO.
        /// </value>
        [Dependency]
        IRecommendationDao RecommendationDao { set; }

        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Sets the users group DAO.
        /// </summary>
        /// <value>
        /// The users group DAO.
        /// </value>
        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        /// <summary>
        /// Creates the specified event identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="usersGroupIds">The users group ids.</param>
        /// <param name="text">The text.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        void Create(long eventId, List<long> usersGroupIds, String text, long userProfileId);

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsForEvent(long eventId);

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsForEvent(long eventId, int startIndex, int count);

        /// <summary>
        /// Finds the recommendations received by user group of user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsReceivedByUserGroupOfUser(long userProfileId);

        /// <summary>
        /// Finds the recommendations received by user group of user.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsReceivedByUserGroupOfUser(long userProfileId, int startIndex, int count);
    }
}