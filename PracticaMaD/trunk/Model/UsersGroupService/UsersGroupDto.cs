using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    /// <summary>
    /// A DTO that can allow to return in the same object, an usersGroup 
    /// the number of users in this group and the number of recommendation for this group.
    /// </summary>
    [Serializable()]
    public class UsersGroupDto
    {
        #region Properties Region

        /// <summary>
        /// Gets the users group.
        /// </summary>
        /// <value>
        /// The users group.
        /// </value>
        public UsersGroup usersGroup { get; private set; }

        /// <summary>
        /// Gets the number of users.
        /// </summary>
        /// <value>
        /// The number of users.
        /// </value>
        public long numOfUsers { get; private set; }

        /// <summary>
        /// Gets the number of recommendations.
        /// </summary>
        /// <value>
        /// The number of recommendations.
        /// </value>
        public long numOfRecommendations { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersGroupDto"/> class.
        /// </summary>
        /// <param name="usersGroup">The users group.</param>
        /// <param name="numOfUsers">The number of users.</param>
        /// <param name="numOfRecommendations">The number of recommendations.</param>
        public UsersGroupDto(UsersGroup usersGroup, long numOfUsers, long numOfRecommendations)
        {
            this.usersGroup = usersGroup;
            this.numOfUsers = numOfUsers;
            this.numOfRecommendations = numOfRecommendations;
        }
    }
}