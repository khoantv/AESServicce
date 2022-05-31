namespace ImportTrialAccount.Models
{
    public class UpdateEmailModel
    {
        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
