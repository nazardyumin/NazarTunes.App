namespace NazarTunes.Models.DataTemplates
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactInfo { get; set; }
        public bool IsCooperating { get; set; }
        public override string ToString()
        {
            return SupplierName!;
        }
    }
}
