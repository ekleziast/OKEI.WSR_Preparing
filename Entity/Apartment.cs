using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Apartment")]
    public class Apartment : Estate
    {
        public int? Floor { get; set; }
        public int? RoomsApartment { get; set; }
    }
}
