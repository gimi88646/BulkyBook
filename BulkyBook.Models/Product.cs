using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {

        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ISBN { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [Required]
        [Range(1,10000)]
        [Display(Name ="List Price")]
        public double ListPrice { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 51-100")]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }

        [ValidateNever]
        public string ImageURL { get; set; }


        // EF Core is smart enough to recognize foreign key 
        [Required]
        [Display(Name = "Category")]
        public int CategoryId  { get; set; }

        // this is an explicit reference just for the sake of knowledge, although it is not needed.
        // if the name were other than CategoryId, this explicit reference would be required
        // [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Cover Type")]
        public int CoverTypeId  { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
