using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OfficeFiddleMVC.Models;

namespace OfficeFiddleMVC.Controllers
{
  [OfficeAuthorize(AllowTrialUsers = true)]
  public class AppController : Controller
  {
    public ActionResult Index(int id = 1)
    {
      var application = new ApplicationInfo();
      application = GetApplicationDetails();
      if (application.Application == "BROWSER")
      {
        ViewBag.SysInfo =
          "You are running the app from Office 2013 or a Browser.<br/> Office 2013 is supported but can not be detected.The Application can only be used from within an office application.";
      }
      if (id == -1 || (id == 1 && User.Identity.IsAuthenticated))
      {
        var officeFiddle = new Fiddle()
        {
         Application = application.Application,
         Version = application.VersionKey,
         IsPublic = true,
         HTML = "<p>Start edditing your new fiddle</p>",
         Name = "New Fiddle",
         userid = User.Identity.GetUserId()
        };
        if (string.IsNullOrEmpty(application.Application))
        {
          return View("IndexViewOnly", officeFiddle);
        }
        return View(officeFiddle);
      }
      else
      {
      var db = new MainData();
        bool ignoreApplication = string.IsNullOrEmpty(application.Application);

        var officeFiddle = (id > 1 ? db.Fiddles.FirstOrDefault(p => p.id == id 
        && (p.Application == application.Application.ToUpper() || ignoreApplication) 
        && (p.Version == application.VersionKey || ignoreApplication) ) : 
        db.Fiddles.FirstOrDefault(p => p.userid == "SYSTEM" && p.Application == application.Application.ToUpper() && p.Version == application.VersionKey)) ?? new Fiddle() {
          Application = application.Application,
          Version = application.Version,
          CSS = "",
          HTML = "<p>We do not have an example for this Office Product</p>"
        };

        if (string.IsNullOrEmpty(application.Application))
        {
          return View("IndexViewOnly", officeFiddle);
        }

        return View(officeFiddle);
      }
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
      }
      else
      {
        appinfo = GetApplicationInfo();
      }
      return appinfo;
    }


    public ActionResult Find(string searchterm,bool showAllVersions = true)
    {
      try
      {
        var application = GetApplicationInfo();
        bool ignoreApplication = string.IsNullOrEmpty(application.Application);

        var db = new MainData();
        string userId = User.Identity.GetUserId();
        if (!string.IsNullOrEmpty(searchterm))
        {
          var officeFiddle = db.Fiddles.Where(p => (p.Application == application.Application.ToUpper() || ignoreApplication)
                                                   && (p.Version == application.VersionKey || showAllVersions) &&
                                                   p.Name.Contains(searchterm) && (p.IsPublic || p.userid == userId))
            .Include("Category")
            .ToList();
          return View(officeFiddle);
        }
        else
        {
          var officeFiddle = db.Fiddles.Where(p => (p.Application == application.Application.ToUpper() || ignoreApplication)
                                                   && (p.Version == application.VersionKey || showAllVersions) &&
                                                   (p.IsPublic || p.userid == userId)).ToList();
          return View(officeFiddle);
        }
      }
      catch (Exception ex)
      {
        
      }
      return View(new List<Fiddle>());
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

    [Authorize]
    public ActionResult Manage()
    {
      ViewBag.Message = "Your saved fiddles";

      return View();
    }

  }
}