namespace Xo_Test.Models
{
    public class Phone
    {
        public int PhoneId { get; set; } 
        public int Number { get; set; } 
        public int BusinessId { get; set; } 
        public Business Business { get; set; } 
    }
}
