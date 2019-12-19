using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Agent")]
    public class Agent : Person
    {
        public int DealShare { get; set; }
    }
}
