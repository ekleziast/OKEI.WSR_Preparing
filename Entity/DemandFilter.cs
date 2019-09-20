using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("DemandFilter")]
    public class DemandFilter
    {
        [Key]
        [ForeignKey("Demand")]
        public int ID { get; set; }
        public virtual Demand Demand { get; set; }

        public int? MinFloors { get; set; }
        public int? MaxFloors { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }
        public int? MinRooms { get; set; }
        public int? MaxRooms { get; set; }
    }
}
