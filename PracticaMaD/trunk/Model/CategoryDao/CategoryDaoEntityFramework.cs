using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>,ICategoryDao
    {
        public List<Category> FindAllCategories()
        {
            String query = "SELECT VALUE a FROM PracticaMaDEntities.Category AS a";
            
            List<Category> result = this.Context.CreateQuery<Category>(query).ToList();

            return result;
        }
    }
}
