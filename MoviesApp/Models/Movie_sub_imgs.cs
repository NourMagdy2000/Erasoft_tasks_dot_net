using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApp.Models
{ [PrimaryKey(nameof(Movieid), nameof(img))] 
    public class Movie_sub_imgs
    {
       
        public int Movieid { get; set; }
        public Models.Movie Movie { get; set; }

        public string img {  get; set; }
    }
}
