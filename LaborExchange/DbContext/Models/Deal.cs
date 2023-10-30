using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Models
{
    public class Deal // Сделка
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public JobVacancy JobVacancy { get; set; } 
        public Applicant Applicant { get; set; }
        public DateTime DealDate { get; set; }
        public string Сontractor { get; set; } // Подрядчик

    }
}
