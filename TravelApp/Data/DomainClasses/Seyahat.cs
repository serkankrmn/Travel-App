namespace TravelApp.Data.DomainClasses
{
    public class Seyahat : BaseEntity
    {
        public string Header { get; set; }

        public string MiniDescription { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        /// <summary>
        /// resim yolu
        /// </summary>
        public string ImagePath { get; set; } 

        /// <summary>
        /// slider resim yolu
        /// </summary>
        public string ImagePathSlider { get; set; }

        /// <summary>
        /// ana sayfada gösterilmesi için
        /// </summary>
        public bool ShowMainPage { get; set; }

        /// <summary>
        /// sliderda gösterilmesi için
        /// </summary>
        public bool ShowInSlider { get; set; }

        public ICollection<CustomerServiceTask> CustomerServiceTasks { get; set; }
    }
}
