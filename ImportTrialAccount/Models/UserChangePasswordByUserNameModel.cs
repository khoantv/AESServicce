namespace ImportTrialAccount.Models
{
    public class UserChangePasswordByUserNameModel
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
    }
}
