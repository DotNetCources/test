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
    public class BooksController : Controller
    {
        BooksService service = new BooksService();
        
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
        public async Task<ActionResult> Create(BookViewModel model)
        {
            try
            {
                await service.Add(model.Name, model.Author, model.ISBN);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            return View(await service.Get(id));
        }
        
        [HttpPut]
        public async Task<ActionResult> Edit(BookViewModel model)
        {
            await service.Edit(model);
            return RedirectToAction("Index");
        }
        // GET: Books/Delete/5
        public async Task<ActionResult>  Delete(int id)
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
