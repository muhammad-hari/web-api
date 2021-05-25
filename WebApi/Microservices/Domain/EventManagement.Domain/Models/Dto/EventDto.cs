using EventManagement.Domain.Models;
using Newtonsoft.Json;
using System;

namespace EventManagement.Api.Models.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public VenueDto Venue { get; set; }
    }
}
