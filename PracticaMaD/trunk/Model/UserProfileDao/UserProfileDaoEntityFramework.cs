using System;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    class UserProfileDaoEntityFramework : GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {

        public UserProfileDaoEntityFramework() { }

        #region IUserProfileDao Members

        /// <summary>
        /// Finds the name of the by login.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            String query =
                "SELECT VALUE u FROM PracticaMaDEntities.UserProfile AS u WHERE u.loginName=@loginName";

            ObjectParameter param = new ObjectParameter("loginName", loginName);

            ObjectResult<UserProfile> result =
                this.Context.CreateQuery<UserProfile>(query, param).Execute(MergeOption.AppendOnly);

            try
            {
                userProfile = result.First<UserProfile>();
            }
            catch (Exception)
            {
                
                userProfile = null;
            }

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName, typeof(UserProfile).FullName);
            return userProfile;
        }

        #endregion
    }
}
