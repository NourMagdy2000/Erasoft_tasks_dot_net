using Microsoft.EntityFrameworkCore;

namespace MoviesApp.Models
{
        [PrimaryKey(nameof(MovieId),nameof(ActorId))]
    public class MovieActors
    {

        private int MovieId { get; set; }
        private Movie Movie { get; set; }

        private int ActorId { get; set; }
        
        private Actor Actor { get; set; }


    }
}
