namespace api.models
{
    public class UserViewModel
    {
        public string username { get; set; }
        public string roleselected { get; set; }
        public bool isauthor { get; set; }
        public string password { get; set; }

        public string email { get; set; }
    }
}