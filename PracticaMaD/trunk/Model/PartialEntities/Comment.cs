using System;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    /// <summary>
    /// Partial class to override the method equals.
    /// </summary>
    public partial class Comment
    {
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" }, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Comment target = (Comment) obj;

            return (this.id == target.id)
                   && (this.date.CompareTo(target.date) == 0)
                   && (this.text == target.text)
                   && (this.eventId == target.eventId)
                   && (this.userProfileId == target.userProfileId);
        }
    }
}