using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public class Services
    {
        public static IUserService UserService { private set; get; }
        public static IEventService EventService { private set; get; }
        public static IRecommendationService RecommendationService { private set; get; }
        public static IUsersGroupService UsersGroupService { private set; get; }
        public static ITagService TagService { private set; get; }

        static Services()
        {
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.Application["unityContainer"];

            container.AddNewExtension<Interception>();
            container.Configure<Interception>()
                .SetInterceptorFor<IUserService>(new InterfaceInterceptor());

            UserService = container.Resolve<IUserService>();
            EventService = container.Resolve<IEventService>();
            RecommendationService = container.Resolve<IRecommendationService>();
            UsersGroupService = container.Resolve<IUsersGroupService>();
            TagService = container.Resolve<ITagService>();
        }
    }
}