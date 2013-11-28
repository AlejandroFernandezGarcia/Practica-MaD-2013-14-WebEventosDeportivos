using System;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    public partial class Event
    {
        public override bool Equals(object obj)
        {
            Event target = (Event) obj;

            return (this.id == target.id)
                && (this.name == target.name)
                && (this.date == target.date)
                && (this.description == target.description)
                && (this.categoryId == target.categoryId);
        }
    }
}
