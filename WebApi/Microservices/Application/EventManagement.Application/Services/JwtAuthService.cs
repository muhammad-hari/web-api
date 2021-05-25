using EventManagement.Application.Interfaces;
using EventManagement.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EventManagement.Application.Services
{
    /**
      * JwtAuthService class 
      * 
      * @author Hari
      * @license MIT
      * @version 1.0
  */
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtConfig jwtTokenConfig;
        private readonly byte[] secret;

        #region Constructor

        public JwtAuthService(JwtConfig jwtTokenConfig)
        {
            this.jwtTokenConfig = jwtTokenConfig;
            secret = Encoding.ASCII.GetBytes(jwtTokenConfig.SecretKey);
        }

        #endregion

        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
            };
        }
    }
}
