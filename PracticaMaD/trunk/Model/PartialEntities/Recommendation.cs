using System;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    /// <summary>
    /// Partial class to override the method equals.
    /// </summary>
    public partial class Recommendation
    {
        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(Recommendation other)
        {
            return _id == other._id && string.Equals(_text, other._text) && _eventId == other._eventId && _usersGroupId == other._usersGroupId && _date.Equals(other._date);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _id.GetHashCode();
                hashCode = (hashCode*397) ^ (_text != null ? _text.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _eventId.GetHashCode();
                hashCode = (hashCode*397) ^ _usersGroupId.GetHashCode();
                hashCode = (hashCode*397) ^ _date.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Recommendation) obj);
        }
    }
}