using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using OfficeFiddleMVC.OfficeVerification;


namespace OfficeFiddleMVC.Models
{
  public class OfficeAuthorizeAttribute : AuthorizeAttribute
  {
    public bool AllowTrialUsers { get; set; }

    /// <summary>
    /// Checks a given license token
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      try
      {
        var isValid = false;
        if (httpContext.Request["et"] == null) return true;
        string token = httpContext.Request.Params["et"];
        byte[] decodedBytes = Convert.FromBase64String(token);
        string decodedToken = Encoding.Unicode.GetString(decodedBytes);


        var service = new VerificationServiceClient();
        var result = service.VerifyEntitlementTokenAsync(new VerifyEntitlementTokenRequest() { EntitlementToken = decodedToken }).Result;

        if (result.IsTest && result.EntitlementType != "Free")
        {
          if (httpContext.Session != null) httpContext.Session["IsValid"] = "ValidityConfirmed2016";
          return true;
        }

        // Check if license is generally valid
        if (result.IsValid )
        {
          if (result.EntitlementType == "Trial" && AllowTrialUsers) isValid = true;
          if (result.EntitlementType == "Paid") isValid = true;
          if (result.IsExpired) isValid = false;
          if (result.IsEntitlementExpired) isValid = false;
        }

        if (isValid)
        {
          if (httpContext.Session != null) httpContext.Session["IsValid"] = "ValidityConfirmed2016";
          return true;
        }
        else
        {
          if (httpContext.Session != null) httpContext.Session["IsValid"] = "false";
          return true;
        }
      }
      catch (Exception)
      {
        if (httpContext.Session != null) httpContext.Session["IsValid"] = "false";
        return true;
      }
    }
  }
}