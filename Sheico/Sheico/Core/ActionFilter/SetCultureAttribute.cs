﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sheico.Core.ActionFilter
{
    public class SetCultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureCode = SetCurrentLanguage(filterContext);

            if (string.IsNullOrEmpty(cultureCode)) return;

            HttpContext.Current.Response.Cookies.Add
            (
                new HttpCookie("Culture", cultureCode)
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddYears(100)
                }
            );

            filterContext.HttpContext.Session["Culture"] = cultureCode;

            switch (cultureCode)
            {
                case "en-US":
                    filterContext.HttpContext.Session["CultureName"] = "ENGLISH";
                    break;
                case "zh-TW":
                    filterContext.HttpContext.Session["CultureName"] = "繁體中文";
                    break;
                case "zh-CN":
                    filterContext.HttpContext.Session["CultureName"] = "简体中文";
                    break;
                case "ja-JP":
                    filterContext.HttpContext.Session["CultureName"] = "日本語";
                    break;
                default:
                    filterContext.HttpContext.Session["CultureName"] = "繁體中文";
                    break;
            }

            CultureInfo culture = new CultureInfo(cultureCode);
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        private static string GetCookieCulture(
            ActionExecutingContext filterContext,
            ICollection<string> Cultures)
        {
            /* Get the language in the cookie*/
            HttpCookie userCookie = filterContext.RequestContext.HttpContext.Request.Cookies["Culture"];

            if (userCookie != null)
            {
                if (!string.IsNullOrEmpty(userCookie.Value))
                {
                    if (Cultures.Contains(userCookie.Value))
                    {
                        return userCookie.Value;
                    }
                }
            }
            return string.Empty;
        }

        private static string GetSessionCulture(
            ActionExecutingContext filterContext,
            ICollection<string> Cultures)
        {
            if (filterContext.RequestContext.HttpContext.Session["Culture"] != null)
            {
                string SessionCulture = filterContext.RequestContext.HttpContext.Session["Culture"].ToString();

                if (!string.IsNullOrEmpty(SessionCulture))
                {
                    return Cultures.Contains(SessionCulture)
                        ? SessionCulture
                        : string.Empty;
                }
            }
            return string.Empty;
        }

        private static string GetBrowserCulture(
            ActionExecutingContext filterContext,
            IEnumerable<string> Cultures)
        {
            /* Gets Languages from Browser */
            IList<string> BrowserLanguages = filterContext.RequestContext.HttpContext.Request.UserLanguages;

            foreach (var thisBrowserLanguage in BrowserLanguages)
            {
                foreach (var thisCultureLanguage in Cultures)
                {
                    if (thisCultureLanguage != thisBrowserLanguage)
                    {
                        continue;
                    }
                    return thisCultureLanguage;
                }
            }
            return string.Empty;
        }

        private static string SetCurrentLanguage(ActionExecutingContext filterContext)
        {
            IList<string> Cultures = new List<string> 
            {
                "en-US",
                "zh-TW", 
                "zh-CN",
                "ja-JP"
            };

            string CookieValue = GetCookieCulture(filterContext, Cultures);

            if (string.IsNullOrEmpty(CookieValue))
            {
                string SessionValue = GetSessionCulture(filterContext, Cultures);

                if (string.IsNullOrEmpty(SessionValue))
                {
                    string BrowserCulture = GetBrowserCulture(filterContext, Cultures);
                    return string.IsNullOrEmpty(BrowserCulture)
                        ? "zh-TW"
                        : BrowserCulture;
                }
                return SessionValue;
            }
            return CookieValue;
        }
    }
}