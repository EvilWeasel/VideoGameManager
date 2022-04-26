using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoGameManagerDot5.DataAccess
{
    /// Data-Annotations Docs: 
    /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
    public class GameGenre
    {
        public int ID { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Game> Games { get; set; }

    }

    public class Game
    {
        public int ID { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        // One to Many
        public GameGenre Genre { get; set; }

        public int PersonalRating { get; set; }
    }
}
