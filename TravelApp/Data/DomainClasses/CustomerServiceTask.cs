namespace TravelApp.Data.DomainClasses
{
    public class CustomerServiceTask : BaseEntity
    {
        public CustomerService CustomerService { get; set; }

        public int CustomerServiceId { get; set; }

        /// <summary>
        /// müşterinin ekrana girdiği email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// müşteri name'i
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// müşteri notu
        /// </summary>
        public string Description { get; set; }


        public int SeyahatId {  get; set; }

        public Seyahat Seyahat { get; set; }

        public bool IsComplete { get; set; }

        public DateTime TaskDate { get; set; } = DateTime.Now;
    }
}
