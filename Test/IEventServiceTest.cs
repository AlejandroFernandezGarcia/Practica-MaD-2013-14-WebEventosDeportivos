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
        public void SearchByKeywords1()
        {
            Event e = eventDao.Find(1);
            Event e1 = eventDao.Find(15);

            List<EventCategoryDto> list1 = eventService.FindByKeywords("Petanca", 0, 1);
            List<EventCategoryDto> list12 = eventService.FindByKeywords("Petanca", 1, 1);
            List<EventCategoryDto> list2 = eventService.FindByKeywords("Gilberto", 0, 1);
            List<EventCategoryDto> list3 = eventService.FindByKeywords("Final petanca", 0, 2);

            Assert.AreEqual(list1.Count, 1);
            Assert.AreEqual(list12.Count, 1);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 2);
            

            Assert.AreEqual(list1[0].evento, e);

            Assert.AreEqual(list12[0].evento, e1);

            Assert.AreEqual(list2[0].evento, e);

            Assert.AreEqual(list3[0].evento, e);
            Assert.AreEqual(list3[1].evento, e1);

        }

        [TestMethod()]
        public void SearchByKeywordsAndCategory1()
        {
            Event e1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            Event e2 = Event.CreateEvent(0, "Evento manzana", DateTime.Now, "Evento de prueba 2", 2);
            Event e3 = Event.CreateEvent(0, "Evento test", DateTime.Now, "Evento de prueba 3", 1);

            eventDao.Create(e1);
            eventDao.Create(e2);
            eventDao.Create(e3);

            List<EventCategoryDto> list1 = eventService.FindByKeywords("Evento", 1, 0, 3);
            List<EventCategoryDto> list2 = eventService.FindByKeywords("ven", 2, 0, 1);
            List<EventCategoryDto> list3 = eventService.FindByKeywords("prueba", 2, 2, 2);
            List<EventCategoryDto> list4 = eventService.FindByKeywords("Even", 3, 0, 2);

            Assert.AreEqual(list1.Count, 2);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 0);
            Assert.AreEqual(list4.Count, 0);

            Assert.AreEqual(list1[0].evento, e1);
            Assert.AreEqual(list1[1].evento, e3);
            Assert.AreEqual(list1[0].evento.name, e1.name);
            Assert.AreEqual(list2[0].evento, e2);
        }

        [TestMethod()]
        public void SearchByKeywords2()
        {
            Event e = eventDao.Find(1);
            Event e1 = eventDao.Find(15);

            List<EventCategoryDto> list1 = eventService.FindByKeywords("Petanca");
            List<EventCategoryDto> list2 = eventService.FindByKeywords("Gilberto");
            List<EventCategoryDto> list3 = eventService.FindByKeywords("Final petanca");

            Assert.AreEqual(list1.Count, 2);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 2);


            Assert.AreEqual(list1[0].evento, e);

            Assert.AreEqual(list2[0].evento, e);

            Assert.AreEqual(list3[0].evento, e);
            Assert.AreEqual(list3[1].evento, e1);
        }

        [TestMethod()]
        public void SearchByKeywordsAndCategory2()
        {
            Event e1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            Event e2 = Event.CreateEvent(0, "Evento manzana", DateTime.Now, "Evento de prueba 1", 2);

            eventDao.Create(e1);
            eventDao.Create(e2);

            List<EventCategoryDto> list1 = eventService.FindByKeywords("Evento", 1);
            List<EventCategoryDto> list2 = eventService.FindByKeywords("ven", 2);
            List<EventCategoryDto> list3 = eventService.FindByKeywords("prueba", 2);
            List<EventCategoryDto> list4 = eventService.FindByKeywords("Evento", 3);

            Assert.AreEqual(list1.Count, 1);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 0);
            Assert.AreEqual(list4.Count, 0);

            Assert.AreEqual(list1[0].evento, e1);
            Assert.AreEqual(list1[0].evento.name, e1.name);
            Assert.AreEqual(list2[0].evento, e2);

        }

        [TestMethod()]
        public void AddComment()
        {
            Event event1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            eventDao.Create(event1);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            String expectedText = "Prueba de comentarios";
            eventService.AddComment(event1.id, expectedText, userProfile.id,new List<string>());
            Comment comment1 = event1.Comment.ToList()[0];

            Assert.AreEqual(comment1.text, expectedText);
            Assert.AreEqual(comment1.userProfileId, userProfile.id);
            Assert.AreEqual(comment1.eventId, event1.id);
        }

        [TestMethod()]
        public void FindCategories()
        {
            List<Category> list = eventService.FindAllCategories();

            Assert.AreEqual(list.Count, 9);
            Assert.AreEqual(categoryDao.Find(1),list[1]);
            Assert.AreEqual(categoryDao.Find(8),list[8]);
        }

        [TestMethod()]
        public void SearchEventComments1()
        {
            Event event1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            eventDao.Create(event1);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            UserProfile userProfile2 = UserProfile.CreateUserProfile(0, "Manolo.com", encryptedPassword, "Manolo", "Garcia", "manolo@udc.es", "es", "ES");
            userProfileDao.Create(userProfile2);

            String commentText1 = "Prueba de comentarios 1";
            eventService.AddComment(event1.id, commentText1, userProfile.id, new List<string>());
            String commentText2 = "Prueba de comentarios 2";
            eventService.AddComment(event1.id, commentText2, userProfile.id, new List<string>());
            String commentText3 = "Prueba de comentarios 3";
            eventService.AddComment(event1.id, commentText3, userProfile.id, new List<string>());
            String commentText4 = "Prueba de comentarios 4";
            eventService.AddComment(event1.id, commentText4, userProfile.id, new List<string>());

            List<Comment> list = eventService.FindCommentsForEvent(event1.id);

            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0].UserProfile, userProfile);
            
        }

        [TestMethod()]
        public void SearchEventComments2()
        {
            Event event1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            eventDao.Create(event1);

            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            UserProfile userProfile2 = UserProfile.CreateUserProfile(0, "Manolo.com", encryptedPassword, "Manolo", "Garcia", "manolo@udc.es", "es", "ES");
            userProfileDao.Create(userProfile2);

            String commentText1 = "Prueba de comentarios 1";
            eventService.AddComment(event1.id, commentText1, userProfile.id, new List<string>());
            String commentText2 = "Prueba de comentarios 2";
            eventService.AddComment(event1.id, commentText2, userProfile.id, new List<string>());
            String commentText3 = "Prueba de comentarios 3";
            eventService.AddComment(event1.id, commentText3, userProfile.id, new List<string>());
            String commentText4 = "Prueba de comentarios 4";
            eventService.AddComment(event1.id, commentText4, userProfile2.id, new List<string>());

            List<Comment> list1 = eventService.FindCommentsForEvent(event1.id, 0, 5);
            List<Comment> list2 = eventService.FindCommentsForEvent(event1.id, 3, 5);
            List<Comment> list3 = eventService.FindCommentsForEvent(event1.id, 4, 5);

            Assert.AreEqual(list1.Count, 4);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 0);

            Assert.AreEqual(list1[0], eventService.FindCommentById(list1[0].id));
        }

        [TestMethod()]
        public void FindEventById()
        {
            Event event1 = Event.CreateEvent(0, "Evento prueba", DateTime.Now, "Evento de prueba 1", 1);
            eventDao.Create(event1);

            Assert.AreEqual(eventService.FindEventById(event1.id),event1);
        }
    }
}
