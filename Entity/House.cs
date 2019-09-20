using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("House")]
    public class House : Estate
    {
        public string Floors { get; set; }
        public string RoomsHouse { get; set; }
    }
}
