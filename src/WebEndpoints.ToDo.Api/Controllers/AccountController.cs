using Application.ApplicationUsers.Command.CreateUser;
using Application.Common.Interfaces;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ToDo.Domain.Common;
using ToDo.Domain.CustomerAggregate.Dto;

namespace WebEndpoints.ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/Account/")]
    [SwaggerTag("Authenticate")]
    public class AccountController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationUserRepository _repository;
        public AccountController(IConfiguration configuration, IApplicationUserRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation("Create User")]
        public async Task<IActionResult> Register(CreateUserCommand model)
        {
             await Mediator.Send(model);
            return Ok();
        }


        [HttpPost("Login")]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _repository.FindByUserName(userName);
            if (user == null) { return BadRequest("user not found"); }
            var checkPassword = await _repository.PasswordSignIn(userName, password, false);
            if (checkPassword.Success)
            {
                var userRoles = await _repository.GetUserRoles(user.Id);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }

    
}
