using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using System.Data.Objects.DataClasses;

namespace Es.Udc.DotNet.PracticaMaD.Test
{

    [TestClass()]
    public class IEventServiceTest
    {
        private static IUnityContainer container;
        private static IEventService eventService;
        private static IEventDao eventDao;
        private static ICategoryDao categoryDao;
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
            eventService = container.Resolve<IEventService>();
            eventDao = container.Resolve<IEventDao>();
            categoryDao = container.Resolve<ICategoryDao>();
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
        public void EmptyResultSearch()
        {
            Category category1 = Category.CreateCategory(0,"Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0,"Evento 1", dateBytes, "Evento de prueba 1", category1.id);

            eventDao.Create(e1);

            List<Event> list = eventService.FindByKeywords("prueba", null);
            Assert.AreEqual(list.Count, 0);
            
        }

        [TestMethod()]
        public void SearchByKeywordsAndCategory1()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);
            Category category2 = Category.CreateCategory(0, "Baloncesto");
            categoryDao.Create(category2);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0,"Evento prueba", dateBytes, "Evento de prueba 1", category1.id);
            Event e2 = Event.CreateEvent(0, "Evento manzana", dateBytes, "Evento de prueba 1", category2.id);

            eventDao.Create(e1);
            eventDao.Create(e2);

            List<Event> list = eventService.FindByKeywords("Evento", category1);
            Assert.AreEqual(list[0], e1);

        }

        [TestMethod()]
        public void SearchByKeywordsAndCategory2()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);
            Category category2 = Category.CreateCategory(0, "Baloncesto");
            categoryDao.Create(category2);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento prueba", dateBytes, "Evento de prueba 1", category1.id);
            Event e2 = Event.CreateEvent(0, "Evento manzana", dateBytes, "Evento de prueba 1", category2.id);

            eventDao.Create(e1);
            eventDao.Create(e2);


            List<Event> list = eventService.FindByKeywords("manzana", category1);
            Assert.AreEqual(list.Count, 0);

        }

        [TestMethod()]
        public void SearchByKeywordsAndCategory3()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);
            Category category2 = Category.CreateCategory(0, "Baloncesto");
            categoryDao.Create(category2);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0, "Evento prueba", dateBytes, "Evento de prueba 1", category1.id);
            Event e2 = Event.CreateEvent(0, "Evento manzana", dateBytes, "Evento de prueba 1", category2.id);

            eventDao.Create(e1);
            eventDao.Create(e2);

            List<Event> list = eventService.FindByKeywords("Evento", null);
            Assert.AreEqual(list.Count, 2);

        }

        [TestMethod()]
        public void AddComment()
        {
            Category category1 = Category.CreateCategory(0, "Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event event1 = Event.CreateEvent(0, "Evento prueba", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(event1);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia","pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            String expectedText = "Prueba de comentarios";
            eventService.AddComment(event1, expectedText, userProfile);
            Comment comment1 = event1.Comment.ToList()[0];

            Assert.AreEqual(comment1.text, expectedText);
            Assert.AreEqual(comment1.userProfileId, userProfile.id);
        }
    }
}
