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

        }

        [TestMethod()]
        public void CreateRecommendationException()
        {
            
        }
    }
}

