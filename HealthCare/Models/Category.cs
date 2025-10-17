namespace HealthCare.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Doctor> Doctors = new List<Doctor>();
    }
}
