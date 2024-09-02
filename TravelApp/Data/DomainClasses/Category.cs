namespace TravelApp.Data.DomainClasses
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }


        public ICollection<Seyahat> Seyahats { get; set;}
    }
}
