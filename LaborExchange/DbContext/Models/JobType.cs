using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class JobType // Тип вакансии
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
