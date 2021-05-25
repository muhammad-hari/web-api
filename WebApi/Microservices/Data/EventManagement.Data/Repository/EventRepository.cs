using EventManagement.Data.Contexts;
using EventManagement.Domain.Interfaces;
using EventManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using EventManagement.Api.Models.Dto;
using System.Threading.Tasks;

namespace EventManagement.Data.Repository
{
    /**
   * EventRepository class 
   * 
   * @author Hari
   * @license MIT
   * @version 1.0
  */
    public class EventRepository : IEventRepository
    {
        #region Private Members

        private readonly EventDbContext context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventRepository(EventDbContext context)
        {
            this.context = context;
        }

     

        #endregion

        public List<EventDto> GetEvents()
        {
            var query = (from @event in context.Event
                         join venue in context.Venue on @event.Id equals venue.Id
                         where @event.Flag == false && venue.Flag == false
                         select new EventDto()
                         {
                             Id = @event.Id,
                             Title = @event.Title,
                             StartDate = @event.StartDate.ToString("yyyy-MM-ddThh:mm:ss"),
                             EndDate = @event.EndDate.ToString("yyyy-MM-ddThh:mm:ss"),
                             Venue = new VenueDto()
                             {
                                Name = venue.Name,
                                Address = venue.Address
                             }
                         }).ToList<EventDto>();

            return query;


        }

        public Event AddEvent(Event @event)
        {
            context.Event.Add(@event);
            context.SaveChanges();
            return @event;
        }

        public bool DeleteEvent(Event @event)
        {
            context.Event.Update(@event);
            context.SaveChanges();
            return true;
        }

        public Event UpdateEvent(Event @event)
        {
            context.Event.Update(@event);
            context.SaveChanges();
            return @event;
        }
    }
}
