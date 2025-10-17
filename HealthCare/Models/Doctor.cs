namespace HealthCare.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Img { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
