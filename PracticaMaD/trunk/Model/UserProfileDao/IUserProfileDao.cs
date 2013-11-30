using System;
using Es.Udc.DotNet.ModelUtil.Dao;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    /// <summary>
    /// The DAO interface of the UserProfile entity.
    /// </summary>
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds the name of the by login.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns></returns>
        UserProfile FindByLoginName(String loginName);
    }
}