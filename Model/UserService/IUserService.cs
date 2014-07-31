using System;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService
    {
        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <returns></returns>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
                          UserProfileDetails userProfileDetails);

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is encrypted].</param>
        /// <returns></returns>
        [Transactional]
        LoginResult Login(String loginName, String password,
                          Boolean passwordIsEncrypted);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <returns></returns>
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
                                      UserProfileDetails userProfileDetails);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="oldClearPassword">The old clear password.</param>
        /// <param name="newClearPassword">The new clear password.</param>
        [Transactional]
        void ChangePassword(long userProfileId, String oldClearPassword,
                            String newClearPassword);
    }
}