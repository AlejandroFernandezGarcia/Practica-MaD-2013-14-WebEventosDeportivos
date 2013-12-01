using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de IRecommendationServiceTest
    /// </summary>
    [TestClass]
    public class IRecommendationServiceTest
    {
        private static IUnityContainer container;
        private static IRecommendationService recommendationService;
        private static IRecommendationDao recommendationDao;
        private static IEventDao eventDao;
        private static IUsersGroupDao usersGroupDao;
        private static ICategoryDao categoryDao;
        private static IUserProfileDao userProfileDao;
        private static IUsersGroupService usersGroupService;

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
            recommendationService = container.Resolve<IRecommendationService>();
            recommendationDao = container.Resolve<IRecommendationDao>();
            eventDao = container.Resolve<IEventDao>();
            usersGroupDao = container.Resolve<IUsersGroupDao>();
            categoryDao = container.Resolve<ICategoryDao>();
            userProfileDao = container.Resolve<IUserProfileDao>();
            usersGroupService = container.Resolve<IUsersGroupService>();
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
        public void CreateRecommendation()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);

            usersGroupService.AddUserToGroup(ids, userProfile.id);

            recommendationService.Create(e1.id, ids, reText,userProfile.id);

            List<Recommendation> list = ug.Recommendation.ToList();

            Assert.AreEqual(1,list.Count);


        }

        [TestMethod()]
        [ExpectedException(typeof(UserNotBelongGroupException))]
        public void CreateRecommendationException()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);

            List<Recommendation> list = ug.Recommendation.ToList();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod()]
        public void FindRecommendationsForEvent()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            Event e2 = Event.CreateEvent(0, "Evento 2", dateBytes, "Evento de prueba 2", category1.id);
            eventDao.Create(e2);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);

            usersGroupService.AddUserToGroup(ids, userProfile.id);

            recommendationService.Create(e1.id, ids, reText, userProfile.id);

            List<Recommendation> list = recommendationService.FindRecommendationsForEvent(e1.id);
            List<Recommendation> list2 = recommendationService.FindRecommendationsForEvent(e2.id);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(0, list2.Count);
        }

        [TestMethod()]
        public void FindRecommendationsForEventPaging()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);

            usersGroupService.AddUserToGroup(ids, userProfile.id);

            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);

            List<Recommendation> list = recommendationService.FindRecommendationsForEvent(e1.id,0,3);
            List<Recommendation> list2 = recommendationService.FindRecommendationsForEvent(e1.id,2,1);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list2.Count);
        }

        [TestMethod()]
        public void FindRecommendationsReceivedByUserGroupOfUser()
        {
            
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            UsersGroup ug2 = UsersGroup.CreateUsersGroup(0, "Partidos de futbol2", "Grupo para partidos de futbol2");
            usersGroupDao.Create(ug2);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            UserProfile userProfile2 = UserProfile.CreateUserProfile(0, "Pepe2.com", encryptedPassword, "Pepe2", "Garcia2", "pepe2@udc.es", "es", "ES");

            userProfileDao.Create(userProfile2);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);

            List<long> ids2 = new List<long>();
            ids2.Add(ug2.id);
            ids2.Add(ug.id);

            usersGroupService.AddUserToGroup(ids, userProfile.id);

            usersGroupService.AddUserToGroup(ids2, userProfile2.id);

            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids2, reText, userProfile2.id);

            List<Recommendation> list = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile.id);
            List<Recommendation> list2 = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile2.id);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(4, list2.Count);
        }

        [TestMethod()]
        public void FindRecommendationsReceivedByUserGroupOfUserPaging()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0, "Partidos de futbol", "Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            UsersGroup ug2 = UsersGroup.CreateUsersGroup(0, "Partidos de futbol2", "Grupo para partidos de futbol2");
            usersGroupDao.Create(ug2);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");

            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");

            userProfileDao.Create(userProfile);

            UserProfile userProfile2 = UserProfile.CreateUserProfile(0, "Pepe2.com", encryptedPassword, "Pepe2", "Garcia2", "pepe2@udc.es", "es", "ES");

            userProfileDao.Create(userProfile2);

            String reText = "Este evento es la polla";
            List<long> ids = new List<long>();
            ids.Add(ug.id);

            List<long> ids2 = new List<long>();
            ids2.Add(ug2.id);
            ids2.Add(ug.id);

            usersGroupService.AddUserToGroup(ids, userProfile.id);

            usersGroupService.AddUserToGroup(ids2, userProfile2.id);

            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids, reText, userProfile.id);
            recommendationService.Create(e1.id, ids2, reText, userProfile2.id);

            List<Recommendation> list = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile.id,0,1);
            List<Recommendation> list2 = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile.id,1,2);
            List<Recommendation> list3 = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile2.id, 0, 1);
            List<Recommendation> list4 = recommendationService.FindRecommendationsReceivedByUserGroupOfUser(userProfile2.id, 1, 3);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(2, list2.Count);
            Assert.AreEqual(1, list3.Count);
            Assert.AreEqual(3, list4.Count);

            Assert.IsTrue(!list2.Contains(list[0]));
            Assert.IsTrue(!list4.Contains(list3[0]));
        }
    }
}

