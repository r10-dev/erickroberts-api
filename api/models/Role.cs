using System;

namespace api.models{
    public class Role : BaseEntity
     {
        public int roleid { get; set; }
        public string title { get; set; }
        public string permissions { get; set; }
    }
}