using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspPay.Models.ViewModels.Pages;
using aspPay.Models.Data;


namespace aspPay.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            List<PageViewModel> ListofPages;

            using (DataB db = new DataB())
            {
                //initialize the list
                ListofPages = db.pages.ToArray().OrderBy(x => x.Sort).Select(x => new PageViewModel(x)).ToList();
            }
            return View(ListofPages);
        }


        //GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }


        //POST: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageViewModel model)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (DataB db = new DataB())
            {
                //declare the slug
                string slug;

                //init page DtO
                PageDtO dto = new PageDtO();

                //DtO Title
                dto.Title = model.Title;

                //Check for and set slug if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                //Make sure title and slug are unique on /Admin/Pages/AddPage
                //notice a a model is returned in View()
                //thus the fields will remain filled out
                if (db.pages.Any(x => x.Title == model.Title) || db.pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists.");
                    return View(model);

                }

                //Save DtO
                db.pages.Add(dto);
                db.SaveChanges();
            }

            //tempdata persists after redirection and viewback
            TempData["SM"] = "Successfully added a new page!!!";


            //Redirect
            return RedirectToAction("AddPage");
        }


        //GET: Admin/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {

            PageViewModel model;

            using (DataB db = new DataB())
            {
                //Get the page
                PageDtO dto = db.pages.Find(id);

                //confirm that the page exists
                if (dto == null)
                {
                    return Content("The page does not exist");
                }

                //no need to initialize all fields because 
                //of the handy 1 parameter constructor in the 
                //PageViewModel Class!
                model = new PageViewModel(dto);


            }

            return View(model);
        }

        //POST: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (DataB db = new DataB())
            {
                //Get page id
                int id = model.Id;

                //initialize
                string slug = "home";

                //get the page
                PageDtO dto = db.pages.Find(id);

                //dto the title
                dto.Title = model.Title;

                //check for slug and set it if need be
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                //Make sure title and slug are unique
                //x => x.Id != id is more or less equivalent to
                //SomeFunction (x) { return x.Id != id; }
                if (db.pages.Where(x => x.Id != id).Any(x => x.Title == model.Title) ||
                   db.pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists.");
                    return View(model);
                }

                //DTO the slug body and sidebar
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;

                //Save
                db.SaveChanges();

            }

            TempData["SM"] = "You have edited the page!";

            //redirect
            return RedirectToAction("EditPage");
        }

        //GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            PageViewModel model;
            using (DataB db = new DataB())
            {
                //Get the page
                PageDtO dto = db.pages.Find(id);

                //confirm page exists
                if (dto == null)
                    return Content("The Page doesn't exist!!!");

                //initialize the pageviewmodel by 1 parameter constructor
                model = new PageViewModel(dto);
            }

            return View(model);
        }

        //GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {

            using (DataB db = new DataB())
            {
                //Get page
                PageDtO dto = db.pages.Find(id);

                //Remove page
                db.pages.Remove(dto);

                //Save
                db.SaveChanges();

                //redirect
                return RedirectToAction("Index");
            }

        }

        //POST: Admin/Pages/ReorderPages
        [HttpPost]
        public void ReorderPages(int[] id)
        {

            using (DataB db = new DataB())
            {
                //Set initial count
                int count = 1;

                //Declare PageDtO
                PageDtO dto;

                //Set sorting for each page
                foreach (var pageId in id)
                {
                    dto = db.pages.Find(pageId);
                    dto.Sort = count;

                    db.SaveChanges();

                    count++;
                }

            }
        }

        //GET: Admin/Pages/EditSideBar
        [HttpGet]
        public ActionResult EditSideBar()
        {
            SideBarViewModel model;

            using (DataB db = new DataB())
            {
                SideBarDtO dto = db.sidebar.Find(1);

                model = new SideBarViewModel(dto);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSideBar(SideBarViewModel model)
        {
            using (DataB db = new DataB())
            {
                SideBarDtO dto = db.sidebar.Find(1);

                dto.Body = model.Body;

                db.SaveChanges();
            }

            TempData["SM"] = "You made a successful edit to the sidebar!!!";

            return RedirectToAction("EditSidebar");

        }

    }

}