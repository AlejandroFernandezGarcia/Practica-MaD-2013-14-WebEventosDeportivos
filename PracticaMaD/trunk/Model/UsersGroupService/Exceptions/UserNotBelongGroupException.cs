using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions
{
    /// <summary>
    /// This exception will be throw when user with userId not belong to the group with groupId.
    /// </summary>
    public class UserNotBelongGroupException : Exception
    {
        /// <summary>
        /// Gets the group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public long GroupId { get; private set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotBelongGroupException"/> class.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public UserNotBelongGroupException(long groupId, long userId)
            : base("The user with the id:" + userId + "not belong to the group with id:" + groupId)
        {
            this.GroupId = groupId;
            this.UserId = userId;
        }
    }
}