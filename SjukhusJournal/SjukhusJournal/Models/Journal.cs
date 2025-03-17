namespace SjukhusJournal.Models
{
    public class Journal
    {
        public int PatientID { get; set; }
        public string PatientNamn { get; set; }

        public string Personnummer { get; set; }

        public string Avdelning { get; set; }

        public string AnsvarigLakare { get; set; }

        public DateTime Datum { get; set; }

        public string BesokOrsak { get; set; }

        public string Diagnos { get; set; }

        public string Atgarder { get; set; }

        public string Kommentarer { get; set; }
    }
}
