using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//COCHADA

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Class that "overrides" the AreEqual method of the class Assert,
    /// because the Assert.AreEquals not work.
    /// </summary>
    public class Asserto
    {
        /// <summary>
        /// Ares the equal.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="obj2">The obj2.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// El primero es null
        /// or
        /// El segundo es null
        /// or
        /// </exception>
        public static void AreEqual(object obj, object obj2)
        {
            if (obj == null) throw new AssertFailedException("El primero es null");
            if (obj2 == null) throw new AssertFailedException("El segundo es null");

            if (!obj.Equals(obj2))
            {
                throw new AssertFailedException(obj.ToString() + " no es igual a " + obj2.ToString());
            }
        }
    }
}