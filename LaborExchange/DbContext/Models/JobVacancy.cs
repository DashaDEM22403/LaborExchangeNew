namespace DbContext.Models
{
    public class JobVacancy // Вакансия
    {
        public int Id { get; set; }
        public JobType JobType { get; set; }
        public JobTitle JobTitle { get; set; }   
        public JobArea JobArea { get; set; }     
        public Employer Employer { get; set; }
        public string Patch { get; set; } // Зарплата
        public string? SpecialRequirements { get; set; }
    }
}
