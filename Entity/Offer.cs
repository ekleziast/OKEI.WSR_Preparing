using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Offer")]
    public class Offer
    {
        [Key]
        public int ID { get; set; }
        public int Price { get; set; }

        [ForeignKey("Estate")]
        public int EstateID { get; set; }
        public virtual Estate Estate { get; set; }

        // Клиент, создавший запрос на продажу
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        // Риэлтор, занимающийся этой заявкой
        [ForeignKey("Agent")]
        public int AgentID { get; set; }
        public virtual Agent Agent { get; set; }

        public bool isCompleted { get; set; }
        public bool isDeleted { get; set; }
    }
}
