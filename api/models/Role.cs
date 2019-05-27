using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.models{
    public class Role : BaseEntity
     {
        public int roleid { get; set; }
        public string title { get; set; }
        public List<RolePermissions> permissions { get; set; }

   
    }

    public class RolePermissions
    {
        public string keyid { get; set; }
        public string value {get;set;}
    }


}