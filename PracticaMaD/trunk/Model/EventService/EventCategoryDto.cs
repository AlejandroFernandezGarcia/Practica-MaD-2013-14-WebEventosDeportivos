using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    [Serializable()]
    public class EventCategoryDto
    {
        #region Properties Region

        public Event evento { get; private set; }

        public Category category { get; private set; }

        #endregion

        public EventCategoryDto(Event evento, Category category)
        {
            this.evento = evento;
            this.category = category;
        }

    }
}
