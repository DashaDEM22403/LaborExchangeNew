using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class Institution // Учебное заведение
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
