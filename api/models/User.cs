using System;

namespace api.models{
    public class User : BaseEntity
    {
        public int userid { get; set; }
        public string username { get; set; }
    }
}