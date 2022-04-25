namespace Identity.API.Models
{
    public class ChangePasswordCurrentUserViewModel
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordUserViewModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
