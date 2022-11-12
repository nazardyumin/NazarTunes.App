namespace NazarTunes.Models.DataTemplates
{
    public class Client : AbstractUser
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public double TotalAmountSpent { get; set; }
        public int PersonalDiscount { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
