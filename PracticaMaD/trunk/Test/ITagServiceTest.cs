using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

//TODO Hacer test.
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass()]
    public class ITagServiceTest
    {
        private static IUnityContainer container;
        private static IEventService eventService;
        private static IEventDao eventDao;
        private static ITagDao tagDao;
        private static ITagService tagService;
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
            tagDao = container.Resolve<ITagDao>();
            tagService = container.Resolve<ITagService>();
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
        public void TestAddTagsToComment()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 0);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0],tagDao.FindByTagName("Tag1"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[1],tagDao.FindByTagName("Tag2"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[2],tagDao.FindByTagName("Tag3"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count,3);

        }

        [TestMethod()]
        public void TestRemoveTagsToComment()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();
            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            
            listOfTags.Remove("Tag2");

            tagService.RemoveTagsFromComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag2"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 1);

            tagService.RemoveTagsFromComment(new List<string>(), e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag2"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 1);
        }
    
        [TestMethod()]
        public void TestAddTagsRepeatedToComment()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 0);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag1"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[1], tagDao.FindByTagName("Tag2"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[2], tagDao.FindByTagName("Tag3"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 3);

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag1"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[1], tagDao.FindByTagName("Tag2"));
            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[2], tagDao.FindByTagName("Tag3"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 3);
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestRemoveInexistentTags()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();
            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);

            List<string> listOfTags2 = new List<string>();
            listOfTags2.Add("Tag-2");

            tagService.RemoveTagsFromComment(listOfTags2, e.Comment.ToList()[0].id);

            Assert.Fail();
        }

        [TestMethod()]
        public void TestRemoveTagThatNotBeInComment()
        {

            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();
            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);

            listOfTags.Remove("Tag2");

            tagService.RemoveTagsFromComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag2"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 1);

            tagService.RemoveTagsFromComment(listOfTags, e.Comment.ToList()[0].id);

            Asserto.AreEqual(e.Comment.ToList()[0].Tag.ToList()[0], tagDao.FindByTagName("Tag2"));
            Assert.AreEqual(e.Comment.ToList()[0].Tag.Count, 1);
        }

        [TestMethod()]
        public void TestFindTagsPercent()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba2", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba3", userProfile.id, listOfTags);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            listOfTags.Remove("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[1].id);

            listOfTags.Remove("Tag2");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[2].id);

            List<TagDto> listCloud = tagService.FindTagsPercent();

            Asserto.AreEqual(listCloud[0].tag,tagDao.FindByTagName("Tag1"));
            Asserto.AreEqual(listCloud[1].tag, tagDao.FindByTagName("Tag2"));
            Asserto.AreEqual(listCloud[2].tag, tagDao.FindByTagName("Tag3"));

            Assert.AreEqual(listCloud[0].percent, 50);
            Assert.AreEqual(listCloud[1].percent, 33,3333320617676);
            Assert.AreEqual(listCloud[2].percent, 16,66667);
        }

        [TestMethod()]
        public void TestFindCommentsByTag()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba2", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba3", userProfile.id, listOfTags);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            listOfTags.Remove("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[1].id);

            listOfTags.Remove("Tag2");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[2].id);

            List<Comment> listComment = tagService.FindCommentsByTag(tagDao.FindByTagName("Tag1").id);
            List<Comment> listComment2 = tagService.FindCommentsByTag(tagDao.FindByTagName("Tag2").id);
            List<Comment> listComment3 = tagService.FindCommentsByTag(tagDao.FindByTagName("Tag3").id);

            Assert.AreEqual(listComment.Count, 3);
            Assert.AreEqual(listComment2.Count, 2);
            Assert.AreEqual(listComment3.Count, 1);
        }

        [TestMethod()]
        public void TestFindCommentsByTagPaginated()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba2", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba3", userProfile.id, listOfTags);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            listOfTags.Remove("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[1].id);

            listOfTags.Remove("Tag2");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[2].id);

            List<Comment> listComment = tagService.FindCommentsByTag(tagDao.FindByTagName("Tag1").id, 0, 1);

            Assert.AreEqual(listComment.Count,1);

            listComment = tagService.FindCommentsByTag(tagDao.FindByTagName("Tag1").id, 1, 2);

            Assert.AreEqual(listComment.Count, 2);
        }

        [TestMethod()]
        public void TestFindTagsOfComment()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba2", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba3", userProfile.id, listOfTags);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            listOfTags.Remove("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[1].id);

            listOfTags.Remove("Tag2");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[2].id);

            List<Tag> listTag = tagService.FindTagsOfComment(e.Comment.ToList()[0].id);
            List<Tag> listTag2 = tagService.FindTagsOfComment(e.Comment.ToList()[1].id);
            List<Tag> listTag3 = tagService.FindTagsOfComment(e.Comment.ToList()[2].id);

            Assert.AreEqual(listTag.Count, 3);
            Assert.AreEqual(listTag2.Count, 2);
            Assert.AreEqual(listTag3.Count, 1);
        }

        [TestMethod()]
        public void TestFindTagsOfCommentPaginated()
        {
            String encryptedPassword = PasswordEncrypter.Crypt("pass");
            UserProfile userProfile = UserProfile.CreateUserProfile(0, "Pepe.com", encryptedPassword, "Pepe", "Garcia", "pepe@udc.es", "es", "ES");
            userProfileDao.Create(userProfile);

            Event e = Event.CreateEvent(0, "Evento aaa", DateTime.Now, "Evento para testing", 1);
            eventDao.Create(e);

            List<string> listOfTags = new List<string>();

            eventService.AddComment(e.id, "Comentario de prueba", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba2", userProfile.id, listOfTags);
            eventService.AddComment(e.id, "Comentario de prueba3", userProfile.id, listOfTags);

            listOfTags.Add("Tag1");
            listOfTags.Add("Tag2");
            listOfTags.Add("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[0].id);

            listOfTags.Remove("Tag3");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[1].id);

            listOfTags.Remove("Tag2");

            tagService.AddTagsToComment(listOfTags, e.Comment.ToList()[2].id);

            List<Tag> listTag = tagService.FindTagsOfComment(e.Comment.ToList()[0].id,0,1);
            List<Tag> listTag2 = tagService.FindTagsOfComment(e.Comment.ToList()[0].id,1,2);

            Assert.AreEqual(listTag.Count, 1);
            Assert.AreEqual(listTag2.Count, 2);
        }
    }
}
