using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        
        //private readonly ICategoryRepository db;
        //public CategoryController(ICategoryRepository db)

        private IUnitOfWork unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            //dependency injections take cares of database object
            //this.db = db;

            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Models.Category>? objCategoryList = db.Categories.ToList();
            IEnumerable<CoverType> objectCoverTypeList = unitOfWork.CoverType.GetAll();
            return View(objectCoverTypeList);
        }
        public IActionResult Create() { 
        return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(CoverType CoverType) {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Add(CoverType);
                unitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }
            return View(CoverType);

        }

        public IActionResult Edit(uint? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType CoverType = unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id==id);

            if (CoverType == null)
            {
                return NotFound();
            }
            return View(CoverType);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType CoverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Update(CoverType);
                unitOfWork.Save();
                TempData["success"] = "CoverType edited successfully.";
                return RedirectToAction("Index");
            }
            return View(CoverType);

        }
        public IActionResult Delete(uint? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType CoverType = unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id == id);
            if (CoverType == null)
            {
                return NotFound();
            }
            return View(CoverType);
        }
        
       // [HttpPost,ActionName("Delete")] for explicit reference.

        //POST  method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType CoverType)
        {
            unitOfWork.CoverType.Remove(CoverType);
            unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
