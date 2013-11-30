﻿using System;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    /// <summary>
    /// Partial class to override the method equals.
    /// </summary>
    public partial class UsersGroup
    {
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" }, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            UsersGroup target = (UsersGroup) obj;

            return (this.id == target.id)
                   && (this.name == target.name)
                   && (this.description == target.description);
        }
    }
}