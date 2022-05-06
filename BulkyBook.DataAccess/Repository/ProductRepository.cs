using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext db;
        
        public ProductRepository(ApplicationDbContext db): base(db)
        {
           this.db = db;
        }

        public void Update(Product Product)
        {


            // if the image URL isn't reset then the imageURL of the object from the db must retain
            Product dbProduct = db.Products.FirstOrDefault(u=>u.Id==Product.Id);

            if (dbProduct != null)
            {
                if (Product.ImageURL != null)
                {
                    dbProduct.ImageURL = Product.ImageURL;

                }
                dbProduct.ISBN = Product.ISBN;
                dbProduct.ListPrice = Product.ListPrice;
                dbProduct.Price = Product.Price;
                dbProduct.Price100 = Product.Price100;
                dbProduct.Price50 = Product.Price50;
                dbProduct.Description = Product.Description;
                dbProduct.Author = Product.Author;
                dbProduct.Title = Product.Title;
                dbProduct.CategoryId = Product.CategoryId;
                dbProduct.CoverTypeId = Product.CoverTypeId;


                // db.Products.Update(dbProduct); no need to call this...
            }
        }
    }
}
