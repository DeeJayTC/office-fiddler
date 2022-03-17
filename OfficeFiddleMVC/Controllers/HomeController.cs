using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeFiddleMVC.Models;

namespace OfficeFiddleMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int id = 1)
        {
          var application = new ApplicationInfo();
          application = GetApplicationDetails();
          if (application.Application != "BROWSER") return RedirectToAction("Index", "App");
          return View();
        }

        public ActionResult Privacy()
        {
          return View();
        }

        public ActionResult Support()
        {
          return View();
        }


    private ApplicationInfo GetApplicationDetails()
    {
      var appinfo = new ApplicationInfo();
      if (Request["_host_Info"] != null && !string.IsNullOrEmpty(Request["_host_info"]))
      {
        var data = Request["_host_Info"].Split('|');
        HttpCookie myCookie = Request.Cookies["OfficeFiddle"] ?? new HttpCookie("OfficeFiddle");
        myCookie.Values["APPLICATION"] = data[0].ToUpper();
        myCookie.Expires = DateTime.Now.AddDays(10);
        appinfo.Application = data[0];
        appinfo.Version = data[2];
        appinfo.VersionKey = data[2].Substring(0, 2);
        myCookie.Values["VERSION"] = appinfo.VersionKey;
        Response.Cookies.Add(myCookie);
        Session["APPINFO"] = appinfo;
      }
      else
      {
        appinfo = GetApplicationInfo();
      }
      return appinfo;
    }
    private ApplicationInfo GetApplicationInfo()
    {
      var application = new ApplicationInfo();
      var myCookie = Request.Cookies["OfficeFiddle"];
      if (myCookie != null)
      {
        application.Application = myCookie["APPLICATION"];
        application.VersionKey = myCookie["VERSION"];
      }
      else
      {
        application.Application = "BROWSER";
      }

      if (Session["APPINFO"] != null)
      {
        var app = (ApplicationInfo)Session["APPINFO"];
        if (app.Application != application.Application && application.Application != "BROWSER")
        {
          application.Application = app.Application;
          application.VersionKey = app.VersionKey;
          application.Version = app.Version;
        }
      }


      return application;
    }

  }





}