using Agency.Models;
using System.ComponentModel.DataAnnotations;

namespace Agency.ViewModes.ProjectVM
{
    public class UpdateProjectVM
    {
        public IFormFile? Photo { get; set; }
        public string Image {  get; set; }
        public string Description { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
