using EventManagement.Api.Models.Dto;
using EventManagement.Api.RestModels.Request;
using EventManagement.Api.RestModels.Response;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Interfaces;
using EventManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EventManagement.Api.Controllers
{
    /**
       * EventManagementController class 
       * 
       * @author Hari
       * @license MIT
       * @version 1.0
   */

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class EventManagementController : ControllerBase
    {
        #region Private Members

        private readonly ILogger<EventManagementController> logger;
        private readonly IEventService eventService;
        private readonly IEventRepository eventRepository;
        private readonly IJwtAuthService jwtAuthService;


        #endregion


        #region Constructor

        public EventManagementController(ILogger<EventManagementController> logger, IEventService eventService, 
            IEventRepository eventRepository, IJwtAuthService jwtAuthService)
        {
            this.logger = logger;
            this.eventService = eventService;
            this.eventRepository = eventRepository;
            this.jwtAuthService = jwtAuthService;
        }



        #endregion


        [HttpPost, Route("Authentication")]
        [AllowAnonymous]
        public ActionResult Authentication([FromBody] Login login)
        {
            if(login.Username == "Test" && login.Password == "Test")
            {
                Claim[] claims() { List<Claim> claims = new List<Claim>()
                {
                    new Claim("user", $"{login.Username}"),
                };

                    return claims.ToArray();
                }

                var jwtResult = jwtAuthService.GenerateTokens(login.Username, claims(), DateTime.Now);

                return Ok(jwtResult.AccessToken);
            }

            return Ok("Unable to authenticate account");
        }

        [HttpGet]
        public ActionResult GetEvents()
        {
            var result = eventRepository.GetEvents().ToList();
            var response = new Response<EventDto>()
            {
                Data = result
            };

            return Ok(response);

        }

        #region CRUD

        // it's just for sample, we need to change update model to Dto model
        // for production

        //[HttpPost]
        //public ActionResult AddEvent([FromBody] Event @event)
        //{
        //    eventRepository.AddEvent(@event);

        //    return Ok(@event);

        //}


        //[HttpPut]
        //public ActionResult UpdateEvent([FromBody] Event @event)
        //{
        //    eventRepository.UpdateEvent(@event);
        //    return Ok(@event);

        //}


        ////[HttpDelete]
        ////public ActionResult DeleteEvent(int id)
        ////{
        ////    eventRepository.DeleteEventById(id);
        ////    return Ok("Deleted");

        ////}


        //[HttpPost, Route("Delete")]
        //public ActionResult DeleteEvent([FromBody] Event @event)
        //{
        //    eventRepository.DeleteEvent(@event);
        //    return Ok(@event);

        //}

        #endregion



    }
}
