using EventManagement.Domain.Models;
using System;
using System.Security.Claims;

namespace EventManagement.Application.Interfaces
{
    /**
      * IJwtAuthService class 
      * 
      * @author Hari
      * @license MIT
      * @version 1.0
  */
    public interface IJwtAuthService
    {
        /// <summary>
        /// Generate new token for api access
        /// </summary>
        /// <param name="username">username for authentication</param>
        /// <param name="claims">claim information for specific user</param>
        /// <param name="now">current date & time</param>
        /// <returns></returns>
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);

        //void RemoveTokens(string username);
    }
}
