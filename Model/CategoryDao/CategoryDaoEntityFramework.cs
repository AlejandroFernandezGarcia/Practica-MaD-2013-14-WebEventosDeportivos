using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <summary>
    /// The implementation of DAO of the entity Category.
    /// </summary>
    internal class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> FindAllCategories()
        {
            String query = "SELECT VALUE a FROM PracticaMaDEntities.Category AS a";

            List<Category> result = this.Context.CreateQuery<Category>(query).ToList();

            return result;
        }
    }
}