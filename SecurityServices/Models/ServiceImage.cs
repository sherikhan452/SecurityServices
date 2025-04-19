using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
namespace SecurityServices.Models
{
    public class ServiceImage
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}
