using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //BusinessLayer.Test test = new BusinessLayer.Test();

            //insert etme testi için yazıldı
            //test.InsertTest();

            //update testi
            //test.UpdateTest();

            //delete testi
            //test.DeleteTest();

            //comment testi
            //test.CommentTest();

            //Category Controller üzerinden gelen view talebi ve model
            //if (TempData["mm"]!=null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

            NoteManager nm = new NoteManager();


            return View(nm.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList());
            //queryable ile gösterimi alternatif yöntem
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        //index view içerisinde seçilen kategoriye göre notların listelenmesi
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                //hata kodu gönderir
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            //eğer kategori yoksa
            if (cat == null)
            {
                return HttpNotFound();

                //ana sayfaya yönlendirme
                //return RedirectToAction("Index", "Home");
            }

            //ByCategory sayfasını Index içerisinde ve notların düzenlenme tarihine göre tersten sıralanıp gönderilme durumu
            return View("Index", cat.Notes.OrderByDescending(x=>x.ModifiedOn).ToList());
        }

        //son yazılar view bölgesi
        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();

            //MostLiked sayfasını İndex view'ı içerisinde gösterdik
            return View("Index",nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }

        //hakkımızda sayfası
        public ActionResult About()
        {
            return View();
        }

        //üye giriş bölgesi login bölgesi
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();

                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    
                    //kullanıcı aktif değilse e-posta gönderme yaptırıldı
                    if (res.Errors.Find(x => x.Code == Entities.Messages.ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-7890";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                //Session'a kullanıcı bilgi saklama
                Session["login"] = res.Result;

                //yönlendirme
                return RedirectToAction("Index");
                

            }

            return View(model);
        }


        //logout bölgesi
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        //register bölgesi
        public ActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanıcı username var mı yok mu kontrolü
            //kullanıcı e-posta kontrolü
            //kayıt işlemi
            //aktivasyon e-postası gönderimi

            

            //EvernoteUser user = null;


            //try
            //{
            //    user=eum.RegisterUser(model);
            //}
            //catch (Exception ex)
            //{

            //    ModelState.AddModelError("", ex.Message);
            //}

            

            if (ModelState.IsValid)
            {

                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

                //hata varsa
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                

                //kullanıcı username var mı yok mu kontrolü
                //if (model.Username=="aaa")
                //{
                //    ModelState.AddModelError("", "kullanıcı adı kullanılıyor");
                //}

                ////kullanıcı e-posta kontrolü
                //if (model.Email=="aaa@aa.com")
                //{
                //    ModelState.AddModelError("", "e-posta adresi kullanılıyor");
                //}

                //foreach (var item in ModelState)
                //{
                //    //eğer değer 0 dan büyükse hata vardır
                //    if (item.Value.Errors.Count>0)
                //    {
                //        return View(model);
                //    }
                //}

                //if (user==null)
                //{
                //    return View(model);
                //}

                return RedirectToAction("RegisterOk");
                
            }

            

            return View(model);
        }

        public ActionResult UserActivate(Guid activate_id)
        {
            //kullanıcı aktivasyonu sağlanacak
            return View();
        }

        public ActionResult RegisterOk()
        {
            return View();
        }
    }
}