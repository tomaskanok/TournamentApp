using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TournamentApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            LocalizationHelper.ChangeCulture();
        }

        [HttpGet]
        public ActionResult Language(string cultureCode = "cs-CZ")
        {
            LocalizationHelper.SetCulture(cultureCode);
            if (null == Request.UrlReferrer ||
                string.IsNullOrEmpty(Request.UrlReferrer.AbsolutePath))
            {
                return new RedirectResult("~/");
            }
            else
            {
                return new RedirectResult(Request.UrlReferrer.AbsolutePath);
            }
        }
    }

    public static class LocalizationHelper
    {
        public static readonly string CultureCacheKey = "current_culture";
        static readonly string DefaultCultureCode = "cs-CZ";
        public static void ChangeCulture()
        {
            string thisCultureCode = HttpContext.Current.Session[CultureCacheKey] as string;
            if (string.IsNullOrEmpty(thisCultureCode))
            {
                // Get thread's culture if there is one.
                thisCultureCode = GetBrowserLanguage();
                if (string.IsNullOrEmpty(thisCultureCode))
                {
                    thisCultureCode = DefaultCultureCode;
                }
            }
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                CultureInfo.CreateSpecificCulture(thisCultureCode);
        }

        public static void SetCulture(string cultureCode = "cs-CZ")
        {
            HttpContext.Current.Session[CultureCacheKey] = cultureCode;
        }

        public static string GetBrowserLanguage()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}