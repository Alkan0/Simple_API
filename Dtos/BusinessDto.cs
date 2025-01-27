namespace Xo_Test.Dtos
{
    public class BusinessDto
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Coordinates { get; set; }
        public string ZipCode { get; set; }
        public string MainActivity { get; set; }
        public List<string> SecondaryActivities { get; set; }
        public List<int> Phones { get; set; } 
        public bool IsActive { get; set; }
    }
}
