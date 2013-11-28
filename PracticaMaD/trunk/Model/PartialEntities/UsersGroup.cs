using System;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    public partial class UsersGroup
    {
        public override bool Equals(object obj)
        {
            UsersGroup target = (UsersGroup)obj;

            return (this.id == target.id)
                && (this.name == target.name)
                && (this.description == target.description);
        }
    }
}
