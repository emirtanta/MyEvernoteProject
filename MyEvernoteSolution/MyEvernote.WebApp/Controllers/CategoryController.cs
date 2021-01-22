using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // seçilen kategoriye göre notların listelenmesi
        public ActionResult Select(int? id)
        {
            if (id==null)
            {
                //hata kodu gönderir
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            //eğer kategori yoksa
            if (cat==null)
            {
                return HttpNotFound();

                //ana sayfaya yönlendirme
                //return RedirectToAction("Index", "Home");
            }

            //farklı controller içerisinden view çağırma işlemi
            TempData["mm"] = cat.Notes;

            return RedirectToAction("Index", "Home");
        }
    }
}