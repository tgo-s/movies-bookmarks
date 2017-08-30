using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movies.bookmarks.infra.moviedb;
using Microsoft.Extensions.Options;
using movies.bookmarks.domain.util;
using movies.bookmarks.domain.aggregates.authentication;

namespace movies.bookmarks.webapi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly Authentication auth;
        public AuthenticationController(IOptions<AppSettings> settings)
        {
            this._settings = settings;
            auth = new Authentication(this._settings);
        }

        /*
         * The methods are organized by the order which have to be called 
         * 1. Create a request token
         * 2. Ask the user for permission
         * 3. Create a session ID
         * 4. Guest Sessions (Not necessary a TMDb account)
         */


        // GET api/values
        [HttpGet("RequestToken")]
        public async Task<JsonResult> RequestToken()
        {
            domain.aggregates.authentication.Token token = await auth.RequestTokenAsync();
            //return the Json to front-end
            return Json(token);
        }

        //In doubt about this method... maybe could get this info directly from front-end
        //I'll keep it here until start front-end development
        [HttpGet("GetUserPermissionUrl")]
        public JsonResult GetUserPermissionUrl(string token, string redirectTo)
        {
            string url = string.Empty;
            string errorMsg = string.Empty;
            bool success = false;
            if (!string.IsNullOrEmpty(token))
            {
                url = auth.RequestUserPermissionUrl(token, redirectTo);
                success = true;
            }
            else
            {
                errorMsg = "The parameter token cannot be null or empty";
            }
            
            return Json(new { Success = success, UserPermissionUrl = url, ErrorMessage = errorMsg });
        }

        [HttpGet("CreateSession")]
        public async Task<JsonResult> CreateSession()
        {
            ApiSession session = await auth.CreateSession();
            return Json(session);
        }

        //Not necessary a TMDb account
        [HttpGet("CreateGuestSession")]
        public async Task<JsonResult> CreateGuestSession()
        {
            ApiGuestSession session = await auth.CreateGuestSession();
            return Json(session);
        }
    }
}
