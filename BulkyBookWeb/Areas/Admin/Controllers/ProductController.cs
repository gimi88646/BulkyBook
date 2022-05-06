using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    public class ProductController : Controller
    {
        
        //private readonly ICategoryRepository db;
        //public CategoryController(ICategoryRepository db)

        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            //dependency injections take cares of database object
            //this.db = db;

            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {

            /*
            Product Product = new();

            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString()
                }
                );

            IEnumerable<SelectListItem> CoverTypeList = unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );
            create new product
            ViewBag.CategoryList = CategoryList;
            ViewBag.CoverTypeList = CoverTypeList;
            */

            ProductVM ProductVM = new()
            {
                Product = new(),
                CategoryList = unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                CoverTypeList = unitOfWork.CoverType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
            };

            if (id == 0 || id == null)
            {
                //create new product
                return View(ProductVM);

            }
            else {
                // update product
                ProductVM.Product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(ProductVM);
            }
            
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {   
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null) {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products\");  
                    var extension = Path.GetExtension(file.FileName);

                    if (productVM.Product.ImageURL != null) {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\')); 
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create)) {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageURL = @"Images\Products\" + filename + extension;

                }
                if (productVM.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productVM.Product);
                }
                else { 
                    unitOfWork.Product.Update(productVM.Product);

                }

                //unitOfWork.CoverType.Add(CoverType);
                unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
            //return RedirectToAction("Index");
        
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() { 
            var productList = unitOfWork.Product.GetAll(includeProperties:"CoverType,Category");
            return Json(new { data = productList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id) {
            Product product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (product == null) {
                return Json(new { success = false, message = "Error while deleting." });
            }

            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, product.ImageURL.TrimStart('\\'));
            
            if (System.IO.File.Exists(oldImagePath)){ 
                System.IO.File.Delete(oldImagePath);
            }
            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
