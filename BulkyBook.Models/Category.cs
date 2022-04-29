﻿using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public uint id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Range(1,100,ErrorMessage ="The range must be within 1 and 100")]
        public byte displayOrder { get; set; }

        public DateTime createdDatetime { get; set; } = DateTime.Now;
    }
}
