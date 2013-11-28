using System;

namespace Es.Udc.DotNet.PracticaMaD.Model
{ 
    public partial class Recommendation
    {
        public override bool  Equals(object obj)
        {
 	        Recommendation target = (Recommendation) obj;

            return (this.id == target.id)
                && (this.text == target.text)
                && (this.eventId == target.eventId)
                && (this.usersGroupId == target.usersGroupId)
                && (this.date == target.date);
        }
    }
}
