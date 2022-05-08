using System.ComponentModel.DataAnnotations;

namespace PflegeboxAPI.DataAccess
{
    public class Adresse
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Strasse { get; set; } = string.Empty;
        public string Hausnummer { get; set; } = string.Empty;
        public string Plz { get; set; } = string.Empty;
        public string Ort { get; set; } = string.Empty;
    }

    public class PflegeboxAntrag
    {
        public int Id { get; set; }
        public Adresse? EmpfaengerAdresse { get; set; }
        public Adresse? LieferAdresse { get; set; }
        public bool IstPrivatVersichert { get; set; }
        public string VersichertenNummer { get; set; } = string.Empty;
        public string Krankenkasse { get; set; } = string.Empty;
        [Range(1, 6)]
        public int BoxArt { get; set; }
    }
}
