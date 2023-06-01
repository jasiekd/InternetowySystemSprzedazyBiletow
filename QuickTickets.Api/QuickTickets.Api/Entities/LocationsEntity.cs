using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class LocationsEntity
    {
        [Key]
        public long LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgURL { get; set; }
    }
}
