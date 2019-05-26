using System;

namespace api.models{
    public class UserRole : BaseEntity
    {
        public int userroleid { get; set; }
        public int userid { get; set; }
    }
}