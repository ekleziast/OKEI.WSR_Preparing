using System.ComponentModel.DataAnnotations.Schema;

namespace esoft.Entity
{
    [Table("Client")]
    public class Client : Person
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
