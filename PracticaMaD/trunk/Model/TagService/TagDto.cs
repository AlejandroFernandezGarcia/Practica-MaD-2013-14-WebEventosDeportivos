using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    /// <summary>
    /// A DTO that can allow to return in the same object, a tag and 
    /// the number of times of this tag is used.
    /// </summary>
    [Serializable()]
    public class TagDto
    {
        #region Properties Region

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public Tag tag { get; private set; }

        /// <summary>
        /// Gets the percent.
        /// </summary>
        /// <value>
        /// The percent.
        /// </value>
        public double percent { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TagDto"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="percent">The percent.</param>
        public TagDto(Tag tag, double percent)
        {
            this.tag = tag;
            this.percent = percent;
        }
    }
}
