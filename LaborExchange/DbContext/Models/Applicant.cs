using System;
using System.ComponentModel.DataAnnotations.Schema;
using DbContext.Enums;

namespace DbContext.Models
{
    public class Applicant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int PassportSeries { get; set; }
        public int PassportId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string IssuedBy { get; set; }
        public string Photo { get; set; }
        public EducationTypes EducationTypes { get; set; }
        public Institution Institution { get; set; }
        public Speciality Speciality { get; set; }
        public string WorkExperience { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Benefit { get; set; } // Пособие

    }
}
