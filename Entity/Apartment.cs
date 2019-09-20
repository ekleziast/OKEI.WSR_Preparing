using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Apartment")]
    public class Apartment : Estate
    {
        public string Floor { get; set; }
        public string RoomsApartment { get; set; }
    }
}
