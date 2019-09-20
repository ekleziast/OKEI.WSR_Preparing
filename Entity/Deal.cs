using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Deal")]
    public class Deal
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Offer")]
        public int? OfferID { get; set; }
        public virtual Offer Offer { get; set; }

        [ForeignKey("Demand")]
        public int? DemandID { get; set; }
        public virtual Demand Demand { get; set; }
    }
}
