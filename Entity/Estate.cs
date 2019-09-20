using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Estate")]
    public class Estate
    {
        [Key]
        public int ID { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public double? Area { get; set; }

        // Широта
        public double? Latitude { get; set; }
        // Долгота
        public double? Longitude { get; set; }
        
        public int? EstateTypeID { get; set; }

        public bool isDeleted { get; set; }
    }
}