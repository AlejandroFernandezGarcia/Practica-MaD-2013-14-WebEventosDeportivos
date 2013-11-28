using System;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    public partial class Category
    {
        public override bool Equals(object obj)
        {
            Category target = (Category) obj;
            return (this.id == target.id)
                && (this.name == target.name);
        }
    }
}
