using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using System.Collections.Concurrent;

namespace AngularJSAuthentication.API.Providers
{
    public class MyRefreshTokenProvider : IAuthenticationTokenProvider
    {
        ConcurrentDictionary<string, AuthenticationTicket> RefreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            await Task.Run(() =>
            {
                int expire = 1 * 60;
                context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddSeconds(expire));
                context.SetToken(context.SerializeTicket());
            });
           
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            await Task.Run(() =>
            {
                context.DeserializeTicket(context.Token);
            });

            
        }
    }
}