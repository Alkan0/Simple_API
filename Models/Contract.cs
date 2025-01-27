namespace Xo_Test.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int BusinessId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public Business Business { get; set; }
        public Product Product { get; set; }
    }
}
