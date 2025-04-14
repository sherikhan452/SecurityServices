namespace SecurityServices.Models
{
    public class Vacancy
    {
        public int VacancyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public bool IsFilled { get; set; }
    }
}
