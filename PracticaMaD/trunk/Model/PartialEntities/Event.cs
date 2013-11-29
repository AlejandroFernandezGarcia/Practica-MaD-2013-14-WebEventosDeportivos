using System;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    public partial class Event
    {
        public override bool Equals(object obj)
        {
            Event target = (Event) obj;

            return (this.id == target.id)
                && (this.name == target.name)
                && (this.date.SequenceEqual(target.date))
                && (this.description == target.description)
                && (this.categoryId == target.categoryId);

        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override string ToString()
        {
            return "[ Id = " + id + " | " +
                "Name = " + name + " | " +
                "Date = " + date + " | " +
                "Description = " + description + " | " +
                "CategoryId = " + categoryId + " ]";
        }
    }
}
