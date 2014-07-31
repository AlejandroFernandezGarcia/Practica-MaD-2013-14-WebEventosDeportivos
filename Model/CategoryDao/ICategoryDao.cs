using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <summary>
    /// The interface of DAO of the entity Category.
    /// </summary>
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        List<Category> FindAllCategories();
    }
}