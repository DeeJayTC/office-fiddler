using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OfficeFiddleMVC.Models;

namespace OfficeFiddleMVC.Controllers
{
  [OfficeAuthorize(AllowTrialUsers = false)]
  public class UserController : Controller
  {
    [Authorize]
    public ActionResult MyFiddles()
    {
      var application = new ApplicationInfo {Application = "EXCEL", Version = "16.1", VersionKey = "16"};
      var myCookie = Request.Cookies["OfficeFiddle"];
      if (myCookie != null)
      {
        application.Application = myCookie["APPLICATION"];
        application.VersionKey = myCookie["VERSION"];
      }

      var db = new MainData();
      var user = User.Identity.GetUserId();
      var officeFiddle =
        db.Fiddles.Where(
          p => p.userid == user && p.Application == application.Application && p.Version == application.VersionKey)
          .ToList();
      return View(officeFiddle);
    }


    public string SaveFiddle([Bind(Include = "id,Name,HTML,CSS,JS,userid,IsPublic,Application,Description,CatID")] Fiddle data)
    {
      var application = new ApplicationInfo();
      var myCookie = Request.Cookies["OfficeFiddle"];
      if (myCookie != null)
      {
        application.Application = myCookie["APPLICATION"];
        application.VersionKey = myCookie["VERSION"];
      }

      if (Session["APPINFO"] != null)
      {
        var app = (ApplicationInfo)Session["APPINFO"];
        if (app.Application != application.Application)
        {
          application.Application = app.Application;
          application.VersionKey = app.VersionKey;
          application.Version = app.Version;
        }
      }



      var db = new MainData();
      if (data.id <= 1)
      {
        data.CSS = HttpUtility.UrlDecode(data.CSS, System.Text.Encoding.Default);
        data.HTML = HttpUtility.UrlDecode(data.HTML.Replace("+", "%2b"), System.Text.Encoding.Default);
        data.JS = HttpUtility.UrlDecode(data.JS.Replace("+", "%2b"), System.Text.Encoding.Default);
        data.userid = User.Identity.GetUserId();
        data.Category = db.Categories.FirstOrDefault(p => p.id == data.CatID);
        data.Application = application.Application;
        data.Version = application.VersionKey;
        db.Fiddles.Add(data);
        db.SaveChanges();
        return data.id.ToString();
      }
      var existing = db.Fiddles.Where(p => p.id == data.id);

      if (existing.Any())
      {
        var fiddle = existing.FirstOrDefault();
        if (fiddle != null)
        {
          fiddle.CSS = HttpUtility.UrlDecode(data.CSS, System.Text.Encoding.Default); 
          fiddle.HTML = HttpUtility.UrlDecode(data.HTML.Replace("+", "%2b"), System.Text.Encoding.Default); 
          fiddle.JS = HttpUtility.UrlDecode(data.JS.Replace("+", "%2b"), System.Text.Encoding.Default); 
          fiddle.Name = data.Name;
          fiddle.Description = data.Description;
          fiddle.IsPublic = data.IsPublic;
          fiddle.Category = db.Categories.FirstOrDefault(p => p.id == data.CatID);
          data.Application = application.Application;
          data.Version = application.VersionKey;
          db.SaveChanges();
        }
      }
      else
      {
        data.userid = User.Identity.GetUserId();
        data.CSS = HttpUtility.UrlDecode(data.CSS, System.Text.Encoding.Default);
        data.HTML = HttpUtility.UrlDecode(data.HTML.Replace("+", "%2b"), System.Text.Encoding.Default); 
        data.JS = HttpUtility.UrlDecode(data.JS.Replace("+", "%2b"), System.Text.Encoding.Default); 
        data.Category = db.Categories.FirstOrDefault(p => p.id == data.CatID);
        data.Application = application.Application;
        data.Version = application.VersionKey;
        db.Fiddles.Add(data);
        db.SaveChanges();
        return "&id=" + data.id;
      }
      return "";
    }


    public ActionResult Save(int id)
    {
      var db = new MainData();
      var user = User.Identity.GetUserId();
      var fiddle = db.Fiddles.Where(p => p.id == id && p.userid == user);
      return View(fiddle.FirstOrDefault());
    }


    public string Share(Fiddle data)
    {
      return "";
    }
  }
}