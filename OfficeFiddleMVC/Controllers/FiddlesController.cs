using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeFiddleMVC.Models;

namespace OfficeFiddleMVC.Controllers
{

    public class FiddlesController : Controller
    {
        private MainData db = new MainData();

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Index()
        {
            return View(await db.Fiddles.ToListAsync());
        }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiddle fiddle = await db.Fiddles.FindAsync(id);
            if (fiddle == null)
            {
                return HttpNotFound();
            }
            return View(fiddle);
        }

    [Authorize]
    public ActionResult Create()
        {
            return View();
        }

        // POST: Fiddles/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<ActionResult> Create([Bind(Include = "id,Name,HTML,CSS,JS,userid,IsPublic,Application,Description")] Fiddle fiddle)
        {
            if (ModelState.IsValid)
            {
                db.Fiddles.Add(fiddle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fiddle);
        }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiddle fiddle = await db.Fiddles.FindAsync(id);
            if (fiddle == null)
            {
                return HttpNotFound();
            }
            return View(fiddle);
        }

        // POST: Fiddles/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit([Bind(Include = "id,Name,HTML,CSS,JS,userid,IsPublic,Application,Description")] Fiddle fiddle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiddle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fiddle);
        }

                                [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiddle fiddle = await db.Fiddles.FindAsync(id);
            if (fiddle == null)
            {
                return HttpNotFound();
            }
            return View(fiddle);
        }

    [Authorize]
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fiddle fiddle = await db.Fiddles.FindAsync(id);
            db.Fiddles.Remove(fiddle);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
