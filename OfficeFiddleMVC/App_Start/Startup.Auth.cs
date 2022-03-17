using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using OfficeFiddleMVC.Models;

namespace OfficeFiddleMVC
{
    public partial class Startup
    {
        // Weitere Informationen zum Konfigurieren der Authentifizierung finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301864".
        public void ConfigureAuth(IAppBuilder app)
        {
            // Konfigurieren des db-Kontexts, des Benutzer-Managers und des Anmelde-Managers für die Verwendung einer einzelnen Instanz pro Anforderung.
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

      // Auskommentierung der folgenden Zeilen aufheben, um die Anmeldung mit Anmeldeanbietern von Drittanbietern zu ermöglichen
      //app.UseMicrosoftAccountAuthentication(
      //    clientId: "000000004C178120",
      //    clientSecret: "Kk81w1ITgrwfNrA/P52d6MIJkNQzr78i");

      //app.UseTwitterAuthentication(
      //consumerKey: "Djm1Q5l9eaLJfoabl6TA3BKQD",
      //   consumerSecret: "j4kulAJKvoLpNyJmToRZoKKU2Os2LjRCvmAROTFvIeQTV5l8Fr");

      app.UseFacebookAuthentication(
      appId: "1655098601441328",
         appSecret: "85fb91de3782321c552c7f5b5a28f031");

      app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
      {
        ClientId = "411430372441-k3dfs2n0ddt9hdkrj9k35tscag0ioqg8.apps.googleusercontent.com",
        ClientSecret = "bh43t8nCBCnBq3cvirCKaxvh",
        Provider = new GoogleOAuth2AuthenticationProvider()
      });
    }
    }
}