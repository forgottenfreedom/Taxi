using SQLite;

namespace Taxi.Models
{
    public class TaxiFahrpreis
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public decimal Fahrpreis { get; set; }
        public decimal Trinkgeld { get; set; }
        public decimal Kredit { get; set; }
        public string Schichttag { get; set; }
    }
}