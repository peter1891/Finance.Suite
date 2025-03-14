﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("users")]
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Uid { get; set; }

        public List<AccountModel> Accounts { get; set; }

        public List<GroupModel> Groups { get; set; }
    }
}
