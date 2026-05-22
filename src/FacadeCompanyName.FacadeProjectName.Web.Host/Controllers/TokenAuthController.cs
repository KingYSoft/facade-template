using Abp.Runtime.Security;
using Facade.AspNetCore.Mvc.Authorization;
using Facade.Core.Web;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.Web.Core.Authentication.JwtBearer;
using FacadeCompanyName.FacadeProjectName.Web.Core.Controllers;
using FacadeCompanyName.FacadeProjectName.Web.Host.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Controllers
{
    [Route("api/tokenauth")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TokenAuthController : FacadeProjectNameControllerBase
    {
        private readonly TokenAuthConfiguration _configuration;
        public TokenAuthController(TokenAuthConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [NoToken]
        [Route("authenticate")]
        public async Task<JsonResponse<AuthenticateOutput>> Authenticate([FromBody] AuthenticateInput input)
        {
            if (input.UserNameOrEmailAddress == "admin" && input.Password == "admin")
            {
                var identity = CreateClaimsIdentity("10000", "admin", "1");

                var accessToken = GetEncrpyedAccessToken(CreateAccessToken(CreateJwtClaims(identity)));

                return new JsonResponse<AuthenticateOutput>
                {
                    Data = new AuthenticateOutput
                    {
                        access_token = accessToken
                    }
                };
            }
            else
            {
                return new JsonResponse<AuthenticateOutput>(false, "登入失败");
            }
        }
        private ClaimsIdentity CreateClaimsIdentity(string userId, string userName, string tenantId)
        {
            var claims = new List<Claim>
            {
                new Claim(AbpClaimTypes.UserId, userId),
                new Claim(AbpClaimTypes.UserName, string.IsNullOrWhiteSpace(userName) ? string.Empty : userName),
                new Claim(AbpClaimTypes.TenantId, tenantId),
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(claimsIdentity);

            var identity = principal.Identity as ClaimsIdentity;
            return identity;
        }
        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}
