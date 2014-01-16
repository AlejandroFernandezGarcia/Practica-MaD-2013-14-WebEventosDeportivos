using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    /// <summary>
    /// DTO that can allow return an Event an his Category in the same object.
    /// </summary>
    [Serializable()]
    public class EventCategoryDto
    {
        #region Properties Region

        /// <summary>
        /// Gets the evento.
        /// </summary>
        /// <value>
        /// The evento.
        /// </value>
        public Event evento { get; private set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public Category category { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCategoryDto" /> class.
        /// </summary>
        /// <param name="evento">The evento.</param>
        /// <param name="category">The category.</param>
        public EventCategoryDto(Event evento, Category category)
        {
            this.evento = evento;
            this.category = category;
        }
    }
}