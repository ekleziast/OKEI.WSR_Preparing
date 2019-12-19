using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("House")]
    public class House : Estate
    {
        public int? Floors { get; set; }
        public int? RoomsHouse { get; set; }
    }
}
