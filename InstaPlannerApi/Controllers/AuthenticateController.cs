using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using InstaPlannerApi.Models;
using InstaPlannerApi.Interfaces;

namespace InstaplannerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthorizationManager _authorizationManager;
        
        public AuthenticateController(IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager;
        }

        [HttpGet]
        public RedirectResult AuthorizeAsync()
        {
            return Redirect(_authorizationManager.GetOAuthUrl());
        }

        [HttpGet]
        [Route("redirect")]
        public async Task<ActionResult> Redirected(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                AccessTokenResult accessTokenResult = await _authorizationManager.GetAccessToken(code);
                return Ok(accessTokenResult.access_token);
            }
            else
            {
                return Ok("No Code In paramter");
            }
        }
        [HttpGet]
        [Route("amountofdays")]
        public ActionResult Redirected()
        {
            var aangemaakt = new DateTime(2020, 05, 01);
            var today = DateTime.Now;

            var NrOfDays = (today - aangemaakt).Days;

            return Ok(NrOfDays);
        }
    }
}