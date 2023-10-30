using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class JobArea // Сфера вакансии
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
