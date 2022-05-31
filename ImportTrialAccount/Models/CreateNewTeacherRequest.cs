namespace ImportTrialAccount.Models
{
    public class CreateNewTeacherRequest
    {
        public string code { get; set; }
        public int? educationDepartmentId { get; set; }
        public string name { get; set; }
        public string PassWord { get; set; }
        public int phongGDDTId { get; set; }
        public int schoolId { get; set; }
        public string email { set; get; }
        public string phoneNumber { set; get; }
    }

}
