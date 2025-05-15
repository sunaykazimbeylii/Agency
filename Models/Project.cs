using Agency.Models.Base;

namespace Agency.Models
{
    public class Project:BaseEntity
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
