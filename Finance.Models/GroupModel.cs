using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("groups")]
    public class GroupModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public List<PersonModel> Persons { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
