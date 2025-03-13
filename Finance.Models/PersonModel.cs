using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("persons")]
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Income { get; set; }

        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}
