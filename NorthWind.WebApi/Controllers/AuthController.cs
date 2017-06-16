using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NorthWind.Domain.Commands.Inputs.Account;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.UnitOfWork;
using NorthWind.Shared.Notifications;
using NorthWind.WebApi.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NorthWind.WebApi.Controllers
{
    [Route("v1/account")]
    public class AuthController : BaseController
    {
        readonly IUserRepository _userRepository;
        readonly TokenOptions _tokenOptions;
        static readonly JsonSerializerSettings _serializerSettings;


        static AuthController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public AuthController(IUow uow, IUserRepository userRepository, IOptions<TokenOptions> jwtOptions) : base(uow)
        {
            _userRepository = userRepository;
            _tokenOptions = jwtOptions.Value;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginCommand command)
        {
            if (command == null)
                return await CreateResponse(null, new[] { new Notification("User", "Invalid user name or password.") });

            var user = _userRepository.GetByEmail(command.Username);

            if (user == null || !user.Authenticate(command.Username, command.Password))
                return await CreateResponse(null, new[] { new Notification("User", "Invalid user name or password.") });

            var identity = await GetClaims(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("NorthWind")
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = user.Id,
                    name = $"{user.FirstName} {user.LastName}",
                    email = user.Email,
                    username = user.Email
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);

            return new OkObjectResult(json);
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaims(User user)
        {
            return Task.FromResult(new ClaimsIdentity(
                 new GenericIdentity(user.Email, "Token"),
                 new[] { new Claim("NorthWind", "User") }));
        }
    }
}
