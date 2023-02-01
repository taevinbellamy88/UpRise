﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using UpRise.Models.Domain;
using UpRise.Models.Interfaces;
using UpRise.Services.Interfaces.Security;

namespace UpRise.Web.Core.Services
{
    public class WebAuthenticationService : IAuthenticationService<int>
    {
        private static string? _title = null;
        private IHttpContextAccessor _contextAccessor;

        static WebAuthenticationService()
        {
            _title = GetApplicationName();
        }

        public WebAuthenticationService(IHttpContextAccessor httpContext)
        {
            this._contextAccessor = httpContext;
        }

        public async Task LogInAsync(IUserAuthData user, params Claim[] extraClaims)
        {
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme
                                                            , ClaimsIdentity.DefaultNameClaimType
                                                            , ClaimsIdentity.DefaultRoleClaimType);

            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider"
                                , _title
                                , ClaimValueTypes.String));

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));

            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));

            if (user.Roles != null && user.Roles.Any())
            {
                foreach (string singleRole in user.Roles)
                {
                    identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, singleRole, ClaimValueTypes.String));
                }
            }

            //identity.AddTenantId(user.TenantId);

            identity.AddClaims(extraClaims);

            AuthenticationProperties props = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(60),
                AllowRefresh = true
            };

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await _contextAccessor.HttpContext
                .SignInAsync(AuthenticationDefaults.AuthenticationScheme, principal, props);
        }

        public async Task LogOutAsync()
        {
            await _contextAccessor.HttpContext.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsLoggedIn()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Only call this when the user IsLoggedIn
        /// </summary>
        /// <returns></returns>
        public int GetCurrentUserId()
        {
            return GetId(_contextAccessor.HttpContext.User.Identity).Value;
        }

        public IUserAuthData GetCurrentUser()
        {
            UserBase baseUser = null;

            if (IsLoggedIn())
            {
                ClaimsIdentity claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                if (claimsIdentity != null)
                {
                    baseUser = ExtractUser(claimsIdentity);
                }
            }

            return baseUser;
        }

        private static UserBase ExtractUser(ClaimsIdentity identity)
        {
            UserBase baseUser = new UserBase();
            List<string> roles = null;

            foreach (var claim in identity.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.NameIdentifier:
                        int id = 0;

                        if (Int32.TryParse(claim.Value, out id))
                        {
                            baseUser.Id = id;
                        }

                        break;

                    case ClaimTypes.Name:
                        baseUser.Email = claim.Value;
                        break;

                    case ClaimTypes.Role:
                        if (roles == null)
                        {
                            roles = new List<string>();
                        }

                        roles.Add(claim.Value);

                        break;

                    default:

                        break;
                }
            }

            baseUser.Roles = roles;

            return baseUser;
        }

        private static int? GetId(IIdentity identity)
        {
            if (identity == null) { throw new ArgumentNullException("identity"); }
            if (!identity.IsAuthenticated) { throw new InvalidOperationException("The current IIdentity is not Authenticated"); }
            ClaimsIdentity ci = identity as ClaimsIdentity;

            int idParsed = 0;

            if (ci != null)
            {
                Claim claim = ci.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (claim != null && Int32.TryParse(claim.Value, out idParsed))
                {
                    return idParsed;
                }
            }
            return null;
        }

        private static string GetApplicationName()
        {
            var entryAssembly = Assembly.GetExecutingAssembly();

            var titleAttribute = entryAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).FirstOrDefault() as AssemblyTitleAttribute;

            return titleAttribute == null ? entryAssembly.GetName().Name : titleAttribute.Title;
        }
    }
}