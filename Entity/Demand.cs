﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Demand")]
    public class Demand
    {
        [Key]
        public int ID { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }

        [ForeignKey("Estate")]
        public int EstateID { get; set; }
        public virtual Estate Estate { get; set; }

        [ForeignKey("DemandFilter")]
        public int? DemandFilterID { get; set; }
        public virtual DemandFilter DemandFilter { get; set; }
        
        // Клиент, создавший запрос на покупку
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