using MySoft.Employee.Entities;
using MySoftCorporation.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MySoftCorporation.Helpers
{
    public static class UserInfo
    {
        public static string IP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        public static string Agent()
        {
            var userAgent = HttpContext.Current.Request.UserAgent;
            var browserWithVersion = "";
            if (userAgent.IndexOf("Edge") > -1)
            {
                //Edge
                browserWithVersion = "Edge Browser Version : " + userAgent.Split(new string[] { "Edge/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Chrome") > -1)
            {
                //Chrome
                browserWithVersion = "Chrome Browser Version : " + userAgent.Split(new string[] { "Chrome/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Safari") > -1)
            {
                //Safari
                browserWithVersion = "Safari Browser Version : " + userAgent.Split(new string[] { "Safari/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Firefox") > -1)
            {
                //Firefox
                browserWithVersion = "Firefox Browser Version : " + userAgent.Split(new string[] { "Firefox/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("rv") > -1)
            {
                //IE11
                browserWithVersion = "Internet Explorer Browser Version : " + userAgent.Split(new string[] { "rv:" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("MSIE") > -1)
            {
                //IE6-10
                browserWithVersion = "Internet Explorer Browser  Version : " + userAgent.Split(new string[] { "MSIE" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Other") > -1)
            {
                //Other
                browserWithVersion = "Other Browser Version : " + userAgent.Split(new string[] { "Other" }, StringSplitOptions.None)[1].Split('.')[0];
            }

            return browserWithVersion;
        }
        public static string Location()
        {
            return "Pakistan";
        }

    }
}