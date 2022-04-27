using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        /// We need this, to prevent circular Json-Parsing when using http-post when inserting into db from json
        /// See below for work-around, alternatively you can just get your post-data from a form, omiting json-parsing
        /// !IMPORTANT!: use 'JsonIgnore' from 'System.Text.Json.Serialization' (NOT Newtonsoft)
        [JsonIgnore]
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
