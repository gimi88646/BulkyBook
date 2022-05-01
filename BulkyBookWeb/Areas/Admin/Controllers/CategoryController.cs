using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        
        //private readonly ICategoryRepository db;
        //public CategoryController(ICategoryRepository db)

        private IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            //dependency injections take cares of database object
            //this.db = db;

            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Models.Category>? objCategoryList = db.Categories.ToList();
            IEnumerable<Category> objectCategoryList = unitOfWork.Category.GetAll();
            return View(objectCategoryList);
        }
        public IActionResult Create() { 
        return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Category category) {
            if (category.Name == category.displayOrder.ToString()) {
                ModelState.AddModelError("CustomError", "Please double-check your entries");
                ModelState.AddModelError("name", "Name and Display Order can't be the same.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);

        }

        public IActionResult Edit(uint? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = unitOfWork.Category.GetFirstOrDefault(u=>u.id==id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.displayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Please double-check your entries");
                ModelState.AddModelError("name", "Name and Display Order can't be the same.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Save();
                TempData["success"] = "Category edited successfully.";
                return RedirectToAction("Index");
            }
            return View(category);

        }
        public IActionResult Delete(uint? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = unitOfWork.Category.GetFirstOrDefault(u=>u.id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
       // [HttpPost,ActionName("Delete")] for explicit reference.

        //POST  method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            unitOfWork.Category.Remove(category);
            unitOfWork.Save();
            TempData["success"] = "Category deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
