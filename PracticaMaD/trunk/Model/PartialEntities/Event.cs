using System;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    /// <summary>
    /// Partial class to override the method equals.
    /// </summary>
    public partial class Event
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
            Event target = (Event) obj;

            return (this.id == target.id)
                   && (this.name == target.name)
                   && (this.date.SequenceEqual(target.date))
                   && (this.description == target.description)
                   && (this.categoryId == target.categoryId);
        }
    }
}