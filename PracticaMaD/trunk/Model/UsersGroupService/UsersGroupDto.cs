using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService
{
    [Serializable()]
    public class UsersGroupDto
    {
        #region Properties Region

        public UsersGroup usersGroup{ get; private set; }

        public long numOfUsers { get; private set; }

        public long numOfRecommendations { get; private set; }

        #endregion

        public UsersGroupDto(UsersGroup usersGroup, long numOfUsers, long numOfRecommendations)
        {
            this.usersGroup = usersGroup;
            this.numOfUsers = numOfUsers;
            this.numOfRecommendations = numOfRecommendations;
        }
    

    }
}
