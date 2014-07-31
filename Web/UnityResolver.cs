using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public static class UnityResolver
    {
        public static T Resolve<T>()
        {
            var container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            return container.Resolve<T>();
        }
    }
}