using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class Employer // Работодатель
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
    }
}
