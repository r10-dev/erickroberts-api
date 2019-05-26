using System.ComponentModel.DataAnnotations;
 
namespace api.models
{
    public class Author : BaseEntity
    {
        [Key]
        public long authorid { get; set; }
 
        [Required]
        public string authorname { get; set; }
 
        public string authorimage { get; set; }

        public int roleid { get; set; }
    }

}