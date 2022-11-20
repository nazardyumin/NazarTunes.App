using Dapper.Contrib.Extensions;

namespace NazarTunes.Models.DataTemplates
{
    public class Nomenclature
    {
        public int NomenclatureId { get; set; }
        [Computed]
        public Record? Record { get; set; }
        public int TotalAmount { get; set; }
        public double SellPrice { get; set; }
        public int TotalItemsSold { get; set; }
        public bool IsAvailable { get; set; }
    }
}
