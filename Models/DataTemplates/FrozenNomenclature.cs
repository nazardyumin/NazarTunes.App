using Dapper.Contrib.Extensions;

namespace NazarTunes.Models.DataTemplates
{
    public class FrozenNomenclature
    {
        public int FrozenNomenclatureId { get; set; }
        public int ClientId { get; set; }
        [Computed]
        public string? ClientInfo { get; set; }
        public int NomenclatureId { get; set; }
        [Computed]
        public string? NomenclatureInfo { get; set; }
        public int Amount { get; set; }
    }
}
