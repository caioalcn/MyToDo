using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Configs.Jwt;
using MyToDo.Api.Models;
using MyToDo.Domain.Crypto;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _service;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IMapper _mapper;

        public LoginController(IServiceLogin service, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, IMapper mapper)
        {
            _service = service;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(Login login)
        {
            var user = await _service.FindUser(login);

            bool isValidLogin = false;
            if (user != null)
            {
                var hash = Security.Validate(login.Password, user.Password);
                isValidLogin = (user != null && login.Username == user.LoginName && hash);
            }

            if (isValidLogin)
            {
                var token = GenerateToken(_signingConfigurations, _tokenConfigurations, user);
                var newUser = _mapper.Map<UserDto>(user);
                return Ok(new
                {
                    user = newUser,
                    authenticated = true,
                    created = token.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = token.DateExpiration.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token.Value,
                });
            }
            else 
            {
                return Unauthorized(new
                {

                    authenticated = false,
                    message = "Username or password invalid!"
                });
            }
        }

        private TokenDto GenerateToken(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, User user)
        {
            var token = new TokenDto();

            ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.Id.ToString(), "UserId"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
                        }
                    );

            token.SetExpiration(TimeSpan.FromSeconds(tokenConfigurations.Seconds));

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = token.DateCreated,
                Expires = token.DateExpiration,
                IssuedAt = token.DateCreated
            });

            token.Value = handler.WriteToken(securityToken);
            return token;
        }
    }
}
