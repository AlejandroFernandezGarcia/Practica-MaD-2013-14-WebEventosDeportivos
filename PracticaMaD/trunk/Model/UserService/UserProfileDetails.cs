using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    /// <summary>
    /// Class which contains the user details
    /// </summary>
    [Serializable()]
    public class UserProfileDetails
    {
        #region Properties Region

        public String FirstName { get; private set; }

        public String Surname { get; private set; }

        public String Email { get; private set; }

        public string Language { get; private set; }

        public string Country { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="Surname">The surname.</param>
        /// <param name="email">The email.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public UserProfileDetails(String firstName, String Surname,
                                  String email, String language, String country)
        {
            this.FirstName = firstName;
            this.Surname = Surname;
            this.Email = email;
            this.Language = language;
            this.Country = country;
        }

        public override bool Equals(object obj)
        {
            UserProfileDetails target = (UserProfileDetails) obj;

            return (this.FirstName == target.FirstName)
                   && (this.Surname == target.Surname)
                   && (this.Email == target.Email)
                   && (this.Language == target.Language)
                   && (this.Country == target.Country);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode();
        }


        /// <summary>
        /// Devuelve una clase <see cref="T:System.String" /> que representa la clase <see cref="T:System.Object" /> actual.
        /// </summary>
        /// <returns>
        /// Una clase <see cref="T:System.String" /> que representa la clase <see cref="T:System.Object" /> actual.
        /// </returns>
        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + FirstName + " | " +
                "Surname = " + Surname + " | " +
                "email = " + Email + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";


            return strUserProfileDetails;
        }
    }
}