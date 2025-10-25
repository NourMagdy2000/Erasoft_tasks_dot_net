using System.ComponentModel.DataAnnotations;

namespace To_do_app.Models
{
    public class Task
    {
        public int Id { get; set; }

        [MaxLength (100)]
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DeadLine { get; set; }
  
        public string File { get; set; }

    }
}
