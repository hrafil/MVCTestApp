using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MVCTestApp.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private string AuthTokenHeader { get; } = "Authorization";
        private string AuthTokenVal { get; } = "ahojsvete";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var info = $"Use {AuthTokenHeader} header and {AuthTokenVal} value";
            string authorizedToken = string.Empty;
            try
            {
                var headerToken = actionContext.Request.Headers.SingleOrDefault(x => x.Key == AuthTokenHeader);
                if (headerToken.Key != null)
                {
                    authorizedToken = Convert.ToString(headerToken.Value.SingleOrDefault());
                    if (authorizedToken != AuthTokenVal)
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent(info)
                        };
                        return;
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(info)
                    };
                    return;
                }
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(info)
                };
                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}