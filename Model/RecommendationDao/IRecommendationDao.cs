using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    /// <summary>
    /// The DAO interface of the Recommendation entity.
    /// </summary>
    public interface IRecommendationDao : IGenericDao<Recommendation, Int64>
    {
        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="evento">The evento.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsForEvent(Event evento);

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="evento">The evento.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationsForEvent(Event evento, int startIndex, int count);

        /// <summary>
        /// Finds the recommendation for an user users group.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationForAnUserUsersGroup(UserProfile userProfile);

        /// <summary>
        /// Finds the recommendation for an user users group.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Recommendation> FindRecommendationForAnUserUsersGroup(UserProfile userProfile, int startIndex, int count);
    }
}