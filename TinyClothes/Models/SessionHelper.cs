using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public static class SessionHelper
    {
        private const string IdKey = "Id";
        private const string UserKey = "Username";

        /// <summary>
        /// Create a individual users session(logged in).
        /// Logs user in.
        /// </summary>
        /// <param name="http"></param>
        /// <param name="id">userID</param>
        /// <param name="username">username</param>
        public static void CreateUserSession(IHttpContextAccessor http, int id, string username)
        {
            http.HttpContext.Session.SetInt32(IdKey, id);
            http.HttpContext.Session.SetString(UserKey, username);
        }

        /// <summary>
        /// Destroy an individual users session(logged out and clear user token with log in info).
        /// Logs user out.
        /// </summary>
        /// <param name="http"></param>
        public static void DestroyUserSession(IHttpContextAccessor http)
        {
            http.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Checks if user is logged in. If user logged in return true.
        /// </summary>
        /// <param name="http"></param>
        public static bool IsUserLoggedIn(IHttpContextAccessor http)
        {
            int? memberId = http.HttpContext.Session.GetInt32(IdKey);
            #region If-Stament
            //if (memberId.HasValue)
            //{
            //    return true;
            //}
            //return false;
            #endregion
            return memberId.HasValue;
        }
    }
}
