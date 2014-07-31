using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;


namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    /// <summary>
    /// The DAO interface of the Tag entity.
    /// </summary>
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        /// <summary>
        /// Finds all tags.
        /// </summary>
        /// <returns></returns>
        List<Tag> FindAllTags();

        /// <summary>
        /// Finds the name of the by tag.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Tag FindByTagName(String name);
    }
}
