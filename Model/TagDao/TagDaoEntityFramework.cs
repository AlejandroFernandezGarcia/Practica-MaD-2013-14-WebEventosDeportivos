using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    /// <summary>
    /// The DAO implementation of the Tag entity.
    /// </summary>
    internal class TagDaoEntityFramework : GenericDaoEntityFramework<Tag,Int64>, ITagDao
    {

        /// <summary>
        /// Finds all tags.
        /// </summary>
        /// <returns></returns>
        public List<Tag> FindAllTags()
        {
            String query = "SELECT VALUE t FROM PracticaMaDEntities.Tag AS t";

            List<Tag> result = this.Context.CreateQuery<Tag>(query).ToList();

            return result;
        }

        /// <summary>
        /// Finds the name of the by tag.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public Tag FindByTagName(string name)
        {
            Tag tag = null;
            
            String query = "SELECT VALUE t FROM PracticaMaDEntities.Tag AS t " +
                           "WHERE t.tagName LIKE @name";

            ObjectParameter param = new ObjectParameter("name", name);

            ObjectResult<Tag> result =
                this.Context.CreateQuery<Tag>(query, param).Execute(MergeOption.AppendOnly);

            try
            {
                tag = result.First<Tag>();
            }
            catch (Exception)
            {
                tag = null;
            }

            if (tag == null)
                throw new InstanceNotFoundException(name, typeof(Tag).FullName);
            return tag;
        }
    }
}
