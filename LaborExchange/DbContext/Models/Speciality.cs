using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class Speciality // Специальность
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public ICollection<Applicant> Applicants { get; set; }
    }
}
