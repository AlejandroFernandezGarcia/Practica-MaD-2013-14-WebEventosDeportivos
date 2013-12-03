using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    internal class TagDaoEntityFramework : GenericDaoEntityFramework<Tag,Int64>, ITagDao
    {
    }
}
