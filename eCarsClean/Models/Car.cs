using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using eCarsClean.Models;

namespace eCarsClean.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Car Model")]
        public string CarModel { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [DisplayName("Miles Per Charge")]
        public double MilesPerCharge { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [DisplayName("Image File")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Required]
        public string Video { get; set; }
        [Required]
        public int Likes { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
    }
}