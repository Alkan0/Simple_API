namespace Xo_Test.Models
{
    public class SecondaryActivity
    {
        public int SecondaryActivityId { get; set; }
        public string Name { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
