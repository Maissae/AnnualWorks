﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCU.AnnualWorks.Authentication.JWT.Core;
using NCU.AnnualWorks.Authentication.JWT.Core.Constants;
using NCU.AnnualWorks.Authentication.JWT.Core.Enums;
using NCU.AnnualWorks.Authentication.JWT.Core.Models;
using NCU.AnnualWorks.Authentication.OAuth.Core;
using NCU.AnnualWorks.Integrations.Usos.Core;
using System;
using System.Threading.Tasks;

namespace NCU.AnnualWorks.Controllers.Auth
{
    [AllowAnonymous]
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUsosService _usosService;
        private readonly IOAuthService _oauthService;
        private readonly IJWTAuthenticationService _jwtService;
        public AuthenticateController(IUsosService usosService, IJWTAuthenticationService jwtService, IOAuthService oauthService)
        {
            _usosService = usosService;
            _oauthService = oauthService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var response = await _usosService.GetRequestTokenAsync(HttpContext);

            var claims = new AuthClaims
            {
                Id = -1,
                AccessType = AccessType.Unknown,
                Token = response.OAuthToken,
                TokenSecret = response.OAuthTokenSecret,
            };
            var claimsIdentity = _jwtService.GenerateClaimsIdentity(claims);
            var jwt = _jwtService.GenerateJWE(claimsIdentity);
            var cookieOptions = _jwtService.GetDefaultCookieOptions();
            cookieOptions.Expires = DateTimeOffset.UtcNow.AddMinutes(15);
            HttpContext.Response.Cookies.Append(AuthenticationCookies.SecureToken, jwt, cookieOptions);
            HttpContext.Response.Cookies.Delete(AuthenticationCookies.SecureAuth, cookieOptions);
            HttpContext.Response.Cookies.Delete(AuthenticationCookies.SecureUser, cookieOptions);

            return new OkObjectResult(_usosService.GetRedirectAddress(response.OAuthToken).ToString());
        }
    }
}
