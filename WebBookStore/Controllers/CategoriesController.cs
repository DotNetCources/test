using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;
using WebBookStore.Services;

namespace WebBookStore.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService service = new CategoriesService();

        public async Task<ActionResult> Index()
        {
            var all = await service.GetAll();
            return View(all);
        }


        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel model)
        {
            try
            {
                await service.Add(model.Name);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}