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
            Product ProductFromDb = db.Products.FirstOrDefault(x => x.Id == Product.Id);
            string imageURLFromDb = ProductFromDb.ImageURL;
            if (ProductFromDb != null) {
                ProductFromDb = Product;
                if (Product.ImageURL == null) { 
                    ProductFromDb.ImageURL = imageURLFromDb;
                }
            }
            
            
            //db.Products.Update(Product);
        }
    }
}
