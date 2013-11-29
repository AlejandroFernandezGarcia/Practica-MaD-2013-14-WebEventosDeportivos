using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//COCHADA
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    public class Asserto
    {
        public static void AreEqual(object obj, object obj2)
        {
            if(obj == null) throw new AssertFailedException("El primero es null");
            if (obj2 == null) throw new AssertFailedException("El segundo es null");

            if (!obj.Equals(obj2))
            {
                throw new AssertFailedException(obj.ToString()+" no es igual a "+obj2.ToString());
            }
        }
    }

}
