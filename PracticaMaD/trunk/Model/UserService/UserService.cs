using System;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        #region IUserService Members

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException"></exception>
        /// <exception cref="DuplicateInstanceException"/>
        public long RegisterUser(string loginName, string clearPassword, UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);

            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile =
                    UserProfile.CreateUserProfile(0, loginName, encryptedPassword,
                        userProfileDetails.FirstName, userProfileDetails.Surname,
                        userProfileDetails.Email, userProfileDetails.Language,
                        userProfileDetails.Country);

                UserProfileDao.Create(userProfile);

                return userProfile.id;

            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile =
                UserProfileDao.FindByLoginName(loginName);
            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.id, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.surname, userProfile.email,
                    userProfile.language, userProfile.country);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);
            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.surname = userProfileDetails.Surname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void ChangePassword(long userProfileId, string oldClearPassword, string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
                PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }

        #endregion

    }
}
