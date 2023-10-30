using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class JobTitle // Должность
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string  Title { get; set; }
    }
}
