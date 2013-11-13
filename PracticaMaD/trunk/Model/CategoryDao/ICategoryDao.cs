using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        List<Category> FindAllCategories();
    }
}
