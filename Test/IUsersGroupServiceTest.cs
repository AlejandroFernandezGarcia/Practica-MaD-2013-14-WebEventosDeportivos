﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de IUsersGroupServiceTest
    /// </summary>
    [TestClass]
    public class IUsersGroupServiceTest
    {
        private static IUnityContainer container;
        private static IUsersGroupService usersGroupService;
        private static IUsersGroupDao usersGroupDao;
        private static IUserProfileDao userProfileDao;

        TransactionScope transaction;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            container = TestManager.ConfigureUnityContainer("unity");
            usersGroupService = container.Resolve<IUsersGroupService>();
            usersGroupDao = container.Resolve<IUsersGroupDao>();
            userProfileDao = container.Resolve<IUserProfileDao>();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        [TestMethod()]
        public void CreateFindGroupAndAddRemoveUser()
        {
            // create & find
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            String name = "Grupo futbol";
            String description = "Grupo para hablar de futbol";
            UserProfile up1 = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(up1);
            long id = usersGroupService.Create(name, description, up1.id);
            UsersGroup ug = usersGroupDao.Find(id);

            Assert.AreEqual(ug.id, id);
            Assert.AreEqual(ug.name, name);
            Assert.AreEqual(ug.description, description);
            Assert.AreEqual(usersGroupDao.GetNumberOfUsersForGroup(ug.id), 1);
            Assert.IsTrue(usersGroupDao.IsUsersBelongGroup(ug, up1));

            // add
            UserProfile up2 = UserProfile.CreateUserProfile(0, "Paco.com", encryptedPassword, "Paco", "Pérez", "paco@udc.es", "es", "ES");
            userProfileDao.Create(up2);
            usersGroupService.AddUserToGroup(ug.id, up2.id);

            Assert.AreEqual(usersGroupDao.GetNumberOfUsersForGroup(ug.id), 2);
            Assert.IsTrue(usersGroupDao.IsUsersBelongGroup(ug, up1));
            Assert.IsTrue(usersGroupDao.IsUsersBelongGroup(ug, up2));

            // remove
            usersGroupService.RemoveUserFromGroup(ug.id, up1.id);

            Assert.AreEqual(usersGroupDao.GetNumberOfUsersForGroup(ug.id), 1);
            Assert.IsFalse(usersGroupDao.IsUsersBelongGroup(ug, up1));
            Assert.IsTrue(usersGroupDao.IsUsersBelongGroup(ug, up2));
        }

        [TestMethod()]
        [ExpectedException(typeof(UserNotBelongGroupException))]
        public void RemoveNonExistingUserFromGroup()
        {
            // create & find group
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            String name = "Grupo futbol";
            String description = "Grupo para hablar de futbol";
            UserProfile up1 = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(up1);
            long id = usersGroupService.Create(name, description, up1.id);
            UsersGroup ug = usersGroupDao.Find(id);

            // remove an user that not belongs to the group
            UserProfile up2 = UserProfile.CreateUserProfile(0, "Paco.com", encryptedPassword, "Paco", "Pérez", "paco@udc.es", "es", "ES");
            userProfileDao.Create(up2);
            usersGroupService.RemoveUserFromGroup(ug.id, up2.id);
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void AddRepeatUserToGroup()
        {
            // create & find group
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            String name = "Grupo futbol";
            String description = "Grupo para hablar de futbol";
            UserProfile up1 = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(up1);
            long id = usersGroupService.Create(name, description, up1.id);
            UsersGroup ug = usersGroupDao.Find(id);

            // add an user that is already in the group
            usersGroupService.AddUserToGroup(ug.id, up1.id);
        }

        [TestMethod()]
        [ExpectedException(typeof (DuplicateInstanceException))]
        public void CreateRepeatNameGroup()
        {
            const string name = "Grupo futbol";
            String description = null;
            UserProfile up2 = null;
            try
            {
                // create & find
                String encryptedPassword = PasswordEncrypter.Crypt("pass");
                description = "Grupo para hablar de futbol";
                UserProfile up1 = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
                userProfileDao.Create(up1);
                long id1 = usersGroupService.Create(name, description, up1.id);
                UsersGroup ug1 = usersGroupDao.Find(id1);

                // create other user
                up2 = UserProfile.CreateUserProfile(0, "Pepe2.com", encryptedPassword, "Pepe2", "Garcia2", "pepe2@udc.es", "es", "ES");
                userProfileDao.Create(up2);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            // create other group with the same name
            long id2 = usersGroupService.Create(name, description + "2", up2.id);
            UsersGroup ug2 = usersGroupDao.Find(id2);
        }
    }
}
