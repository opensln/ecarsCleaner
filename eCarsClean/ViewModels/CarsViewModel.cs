using System;
using System.Collections.Generic;
using eCarsClean.Models;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace eCarsClean.ViewModels
{
    public class CarsViewModel
    {
        public IEnumerable<Car> CarList{ get; set; }

        [DisplayName("Make")]
        public IEnumerable<Manufacturer> ManufacturerList { get; set; }
    }
}