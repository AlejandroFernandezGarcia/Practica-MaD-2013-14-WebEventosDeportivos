using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
            Category category1 = Category.CreateCategory(0,"Futbol");
            categoryDao.Create(category1);

            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Event e1 = Event.CreateEvent(0,"Evento 1", dateBytes, "Evento de prueba 1", category1.id);
            eventDao.Create(e1);

            UsersGroup ug = UsersGroup.CreateUsersGroup(0,"Partidos de futbol","Grupo para partidos de futbol");
            usersGroupDao.Create(ug);

            recommendationService.Create(e1, ug, "Metete en este grupo");

            Recommendation re = recommendationDao.Find(1);

            Recommendation re1 = new Recommendation();
            re1.id = 1;
            re1.eventId = e1.id;
            re1.usersGroupId = ug.id;
            re1.text = "Metete en este grupo";

            Assert.AreEqual(re, re1);
        }
    }
}
