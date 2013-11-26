﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    class RecommendationService : IRecommendationService
    {
        [Dependency]
        public IRecommendationDao RecommendationDao { private get; set; }

        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }

        [Dependency]
        public IEventDao EventDao { private get; set; }

        public void Create(long eventId, List<long> usersGroupIds, string text, long userProfileId)
        {
            List<UsersGroup> list = UsersGroupDao.FindAllGroupsOfUser(userProfileId);

            List<long> listOfIds = new List<long>;

            foreach(UsersGroup j in list)
            {
                listOfIds.Add(j.id);
            }

            foreach (long i in usersGroupIds)
            {
                if (listOfIds.Contains(i) == false){
                    throw new UserNotBelongGroupException(i,userProfileId);
                }
            }

            
            foreach(long i in usersGroupIds)
            {
                Recommendation recommendation = Recommendation.CreateRecommendation(0, text, eventId, i);

                RecommendationDao.Create(recommendation);
            }   
        }

        public List<Recommendation> FindRecommendationsForEvent(long eventId)
        {
            return RecommendationDao.FindRecommendationsForEvent(EventDao.Find(eventId));
        }

    }
}
