using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;


namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    /// <summary>
    /// The DAO implementation of the Recommendation entity.
    /// </summary>
    internal class RecommendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, Int64>,
                                                      IRecommendationDao
    {
        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="evento">The evento.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsForEvent(Event evento)
        {
            return evento.Recommendation.ToList();
        }

        /// <summary>
        /// Finds the recommendations for event.
        /// </summary>
        /// <param name="evento">The evento.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationsForEvent(Event evento, int startIndex, int count)
        {
            return evento.Recommendation.Skip(startIndex).Take(count).ToList();
        }

        /// <summary>
        /// Finds the recommendation for an user users group.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationForAnUserUsersGroup(UserProfile userProfile)
        {
            //UNDONE Fortes: Este metodo falla cuando no existen grupos de usuarios en la aplicación.
            userProfile.UsersGroup.Load();

            String query = "SELECT VALUE r " +
                           "FROM PracticaMaDEntities.Recommendation AS r " +
                           "WHERE r.usersGroupId =";
                           

            List<long> listOfGroups = new List<long>();
            bool first = true;
            foreach (UsersGroup usersGroup in userProfile.UsersGroup.ToList())
            {
                if (first)
                {
                    first = false;
                    query += usersGroup.id + " ";
                }
                else
                {
                    query += "OR r.usersGroupId = " + usersGroup.id + " ";
                }
            }

            query+= "ORDER BY r.date DESC";

            return this.Context.CreateQuery<Recommendation>(query).ToList();
        }

        /// <summary>
        /// Finds the recommendation for an user users group.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Recommendation> FindRecommendationForAnUserUsersGroup(UserProfile userProfile, int startIndex,
                                                                          int count)
        {
            //UNDONE Fortes: Este metodo falla cuando no existen grupos de usuarios en la aplicación.
            userProfile.UsersGroup.Load();

            String query = "SELECT VALUE r " +
                           "FROM PracticaMaDEntities.Recommendation AS r " +
                           "WHERE r.usersGroupId =";


            List<long> listOfGroups = new List<long>();
            bool first = true;
            foreach (UsersGroup usersGroup in userProfile.UsersGroup.ToList())
            {
                if (first)
                {
                    first = false;
                    query += usersGroup.id + " ";
                }
                else
                {
                    query += "OR r.usersGroupId = " + usersGroup.id + " ";
                }
            }

            query += "ORDER BY r.date DESC";

            return this.Context.CreateQuery<Recommendation>(query).Skip(startIndex).Take(count).ToList();
        }
    }
}