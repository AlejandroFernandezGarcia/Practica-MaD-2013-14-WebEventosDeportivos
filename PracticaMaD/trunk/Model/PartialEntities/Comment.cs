using System;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    public partial class Comment
    {
        public override bool Equals(object obj)
        {
            Comment target = (Comment)obj;

            return (this.id == target.id)
                && (this.date.SequenceEqual(target.date))
                && (this.text == target.text)
                && (this.eventId == target.eventId)
                && (this.userProfileId == target.userProfileId);
        }
    }
}
