//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.Practices.Unity;
//using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
//using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
//using System.Transactions;
//using Es.Udc.DotNet.PracticaMaD.Model;
//using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
////using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
//using Es.Udc.DotNet.ModelUtil.Exceptions;
//using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;

//namespace Es.Udc.DotNet.PracticaMaD.Test
//{
//    /// <summary>
//    /// Descripción resumida de IUsersGroupServiceTest
//    /// </summary>
//    [TestClass]
//    public class IUsersGroupServiceTest
//    {
//        private static IUnityContainer container;
//        private static IUsersGroupService usersGroupService;
//        private static IUsersGroupDao usersGroupDao;
//        private static IUserProfileDao userProfileDao;

//        TransactionScope transaction;

//        private TestContext testContextInstance;

//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        [ClassInitialize()]
//        public static void MyClassInitialize(TestContext testContext)
//        {
//            container = TestManager.ConfigureUnityContainer("unity");
//            usersGroupService = container.Resolve<IUsersGroupService>();
//            usersGroupDao = container.Resolve<IUsersGroupDao>();
//            userProfileDao = container.Resolve<IUserProfileDao>();
//        }

//        [ClassCleanup()]
//        public static void MyClassCleanup()
//        {
//            TestManager.ClearUnityContainer(container);
//        }

//        [TestInitialize()]
//        public void MyTestInitialize()
//        {
//            transaction = new TransactionScope();
//        }

//        [TestCleanup()]
//        public void MyTestCleanup()
//        {
//            transaction.Dispose();
//        }

//        [TestMethod()]
//        public void CreateUsersGroup()
//        {
//            String name = "Grupo futbol";
//            String description = "Grupo para hablar de futbol";
//            long id = usersGroupService.Create(name, description);

//            UsersGroup ug = usersGroupDao.Find(id);

//            Assert.AreEqual(ug.id, id);
//            Assert.AreEqual(ug.name, name);
//            Assert.AreEqual(ug.description, description);
//        }

//        [TestMethod()]
//        public void RemoveUserFromGroup() 
//        {
//            String encryptedPassword = PasswordEncrypter.Crypt("pass");

//            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

//            userProfileDao.Create(userProfile);

//            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
//            usersGroupDao.Create(ug);

//            usersGroupService.AddUserToGroup(ug, userProfile);

//            usersGroupService.RemoveUserFromGroup(ug, userProfile);

//            Assert.AreEqual(ug.UserProfileUsersGroup.Count, 0);


//        }

//        [TestMethod()]
//        [ExpectedException(typeof(UserNotBelongGroupException))]
//        public void RemoveNonExistingUserFromGroup()
//        {
//            String encryptedPassword = PasswordEncrypter.Crypt("pass");

//            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

//            userProfileDao.Create(userProfile);

//            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
//            usersGroupDao.Create(ug);

//            usersGroupService.RemoveUserFromGroup(ug, userProfile);

//        }

//        [TestMethod()]
//        public void AddUserToGroup()
//        {
//            String encryptedPassword = PasswordEncrypter.Crypt("pass");

//            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

//            userProfileDao.Create(userProfile);
            
//            UsersGroup ug = UsersGroup.CreateUsersGroup(0,"Partidos de futbol","Grupo para partidos de futbol");
//            usersGroupDao.Create(ug);

//            usersGroupService.AddUserToGroup(ug, userProfile);

//            Assert.AreEqual(ug.UserProfileUsersGroup.ToList()[0].UserProfile, userProfile);
        
//        }

//        [TestMethod()]
//        [ExpectedException(typeof(DuplicateInstanceException))]
//        public void AddRepeatUserToGroup()
//        {
//            String encryptedPassword = PasswordEncrypter.Crypt("pass");

//            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

//            userProfileDao.Create(userProfile);

//            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
//            usersGroupDao.Create(ug);

//            usersGroupService.AddUserToGroup(ug, userProfile);

//            usersGroupService.AddUserToGroup(ug, userProfile);

//            Assert.Fail();

//        }
//    }
//}
