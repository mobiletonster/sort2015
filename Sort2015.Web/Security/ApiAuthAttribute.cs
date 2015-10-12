using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sort2015.Web.Security
{
    // This is a sample auth filter using a static API_TOKEN. You can implement your own scheme as you wish.
    public class ApiAuthAttribute : AuthorizationFilterAttribute
    {
        private const string SCHEME = "sort_api_token";
        private const string API_TOKEN = "U29ydEFwaUtleTpTb3J0U2VjdXJpdHk=";
        private const string API_KEY = "SortApiKey";
        private const string API_KEY_VALUE = "SortSecurity";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme == SCHEME)
                {
                    var rawCredentials = authHeader.Parameter;
                    if (CheckCredentials(rawCredentials))
                    {
                        return;
                    }
                }
            }

            if (actionContext.Request.Headers.Contains(SCHEME))
            {
                var header = actionContext.Request.Headers.Where(m => m.Key == SCHEME).FirstOrDefault();
                var rawCredentials = header.Value.FirstOrDefault();
                if (CheckCredentials(rawCredentials))
                {
                    return;
                }
            }

            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            var req = actionContext.Request;
            actionContext.Response = req.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "token Scheme='" + SCHEME + "' ");
        }

        private static bool CheckCredentials(string rawCredentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials)); //ApiKey|Security|SomeCODE|2  // mobiletony@msn.com|1|1854|7/25/2014 11:06:50 PM
                var split = credentials.Split(':');
                var apiKey = split[0];
                var keyValue = split[1];

                if (apiKey == API_KEY || keyValue == API_KEY_VALUE)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                // failed.
            }

            return false;
        }

        private static string GetCredentials()
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var credentials = API_KEY + ":" + API_KEY_VALUE;
            var credentialsBytes = encoding.GetBytes(credentials);
            var tokenized = Convert.ToBase64String(credentialsBytes);
            return tokenized;
        }

    }
}
