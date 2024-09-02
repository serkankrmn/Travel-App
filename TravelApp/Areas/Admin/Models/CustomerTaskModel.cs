namespace TravelApp.Areas.Admin.Models
{
    public class CustomerTaskModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }


        public DateTime CreateDate { get; set; }


        public int SeyahatId { get; set; }
        public string SeyahatName { get; set; }


        public string CustomerServiceName { get; set; }

        public int CustomerServiceId { get; set; }

        public bool IsCompleted { get; set; }

    }
}
