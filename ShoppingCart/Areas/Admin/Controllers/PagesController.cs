using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ShoppingCart.Models.Data;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        //
        // GET: /Admin/Pages/
        public ActionResult Index()
        {
            
            var pages = db.Pages_tbl.ToList();

            List<Pages> p = db.Pages_tbl
                .ToArray().OrderBy(x => x.Sorting).Select(x => new Pages(x)).ToList();
            return View(p);
        }
        // GET: /Admin/Pages/addpage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // post: /Admin/Pages/addpage
        [HttpPost]
        public ActionResult AddPage(Pages model)
        {
            if (! ModelState.IsValid)
            {
                return View(model);
            }

            Pages_tbl pages = new Pages_tbl();
            string slug;
            if (string.IsNullOrEmpty(model.Slug))
            {
                slug = model.Title.Replace(" ", "-").ToLower();
            }
            else
            {
                slug = model.Slug.Replace(" ", "-").ToLower();
                
            }
            if (db.Pages_tbl.Any(x => x.Title == model.Title) || db.Pages_tbl.Any(x => x.Slug == model.Slug))
            {
                   ModelState.AddModelError("" , " Title or slub already exist ");
                return View(model);
            }
            pages.Body = model.Body;
            pages.Title = model.Title;
            pages.Slug = slug;
            pages.Sorting = 100;
            db.Pages_tbl.Add(pages);
            db.SaveChanges();

            TempData["SM"] = "You heve added a new page";
            return RedirectToAction("AddPage");
        }


        // GET: /Admin/Pages/editpage
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            var row = db.Pages_tbl.Find(id);
            if (row == null)
            {
                return Content("The page doesn't exsist");
            }

            Pages pages = new Pages(row);

            return View(pages);
        }

        // post: /Admin/Pages/editpage
        [HttpPost]
        public ActionResult EditPage(Pages model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var row = db.Pages_tbl.Find(model.Id);

            string slug = "home";

            if (model.Slug != "home")
            {
                if (string.IsNullOrEmpty(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();

                } 
            }

            if (db.Pages_tbl.Where(x => x.Id != model.Id).Any(x => x.Title == model.Title) || db.Pages_tbl.Where(x => x.Id != model.Id).Any(x => x.Slug == model.Slug))
            {
                ModelState.AddModelError("", " Title or slub already exist ");
                return View(model);
            }

            row.Slug = slug;
            row.Title = model.Title;
            row.Body = model.Body;
            row.HasSlidebar = model.HasSlidebar;

            db.SaveChanges();

            TempData["SM"] = "You have edited the page";

            return RedirectToAction("EditPage");
        }

        // GET: /Admin/Pages/pageDetails
        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            var page = db.Pages_tbl.Find(id);

            if (page == null)
            {
                return Content("Page Doesn't Exsit");
            }

            Pages model = new Pages(page);

            return View(model);
        }

        public ActionResult DeletePage(int id)

        {
            var page = db.Pages_tbl.Find(id);

            if (page != null) db.Pages_tbl.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

	}
}