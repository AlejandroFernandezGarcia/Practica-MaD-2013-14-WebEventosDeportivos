using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileUsersGroupDao
{
    class UserProfileUsersGroupDaoEntityFramework : GenericDaoEntityFramework<UserProfileUsersGroup, Int64>, IUserProfileUsersGroupDao
    {
        public void AddUserToGroup(UserProfileUsersGroup userProfileUsersGroup)
        {
            
            this.Create(userProfileUsersGroup);
        }
        
        
        public void RemoveUserFromGroup(long usersGroupId, long userProfileId)
        {
            UserProfileUsersGroup upug = this.FindByUserIdAndGroupId(usersGroupId, userProfileId);

            this.Remove(upug.id);
        }

        public UserProfileUsersGroup FindByUserIdAndGroupId(long usersGroupId, long userProfileId)
        {
            UserProfileUsersGroup upug = null;
            
            String query = "SELECT VALUE e FROM PracticaMaDEntities.UserProfileUsersGroupDao AS e " +
                           "WHERE e.userId = @userProfileId " +
                           "AND e.groupID = @usersGroupId ";

            ObjectParameter param = new ObjectParameter("userProfileId", userProfileId);
            ObjectParameter param2 = new ObjectParameter("usersGroupId", usersGroupId);

            ObjectQuery<UserProfileUsersGroup> oQuery = this.Context.CreateQuery<UserProfileUsersGroup>(query, param,
                                                                                                        param2);
            // el test falla aquí, si descomentas esta excepción la verás:
            //throw new Exception("Antes del Execute");
            ObjectResult<UserProfileUsersGroup> result = oQuery.Execute(MergeOption.AppendOnly);
            // pero si solo descomentas esta no la verás:
            //throw new Exception("Después del Execute");

            try
            {
                upug = result.First<UserProfileUsersGroup>();
            }
            catch (Exception)
            {
                throw new InstanceNotFoundException("The user has not member of the group",
                    typeof(UserProfileUsersGroup).FullName);
            }
            
            return upug;
        }                                  
    }
}
