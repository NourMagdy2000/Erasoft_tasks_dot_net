namespace HealthCare.Models
{
    public class Apointment
    {

        public int Id { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime ? Date { get; set; }
        public string Time { get; set; }
    }
}
