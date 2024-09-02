using System.Buffers.Text;

namespace TravelApp.Data.DomainClasses
{
    public class CustomerService : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }


        public ICollection<CustomerServiceTask> CustomerServiceTasks { get; set; }
    }
}
