namespace Xo_Test.Models
{
    public class Business
    {
            public int BusinessId { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Coordinates { get; set; }
    public string ZipCode { get; set; }
    public ICollection<Phone> Phones { get; set; }
    public bool IsActive { get; set; } = true;
    public string MainActivity { get; set; }
    public ICollection<SecondaryActivity> SecondaryActivities { get; set; }
    }
}
