using EventManagement.Api.Models.Dto;
using EventManagement.Domain.Models;
using System.Collections.Generic;

namespace EventManagement.Domain.Interfaces
{
    /**
    * IEventRepository class 
    * 
    * @author Hari
    * @license MIT
    * @version 1.0
   */

    public interface IEventRepository
    {
        #region Events

        List<EventDto> GetEvents();
        Event AddEvent(Event @event);
        Event UpdateEvent(Event @event);
        bool DeleteEvent(Event @event);


        #endregion
    }
}
