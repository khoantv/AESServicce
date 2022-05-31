namespace ImportTrialAccount.Models
{
    public class CreateNewTeacherResponse
    {
        public int teacherId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public object imageUrl { get; set; }
        public object address { get; set; }
        public object email { get; set; }
        public object phoneNumber { get; set; }
        public int roleInSchool { get; set; }
        public int schoolTypeId { get; set; }
        public int schoolId { get; set; }
        public int phongGDDTId { get; set; }
        public int educationDepartmentId { get; set; }
        public int userId { get; set; }
        public string passWord { get; set; }
        public object qualification { get; set; }
        public object isFinishInfoTeacher { get; set; }
        public object isUseFormFinishInfo { get; set; }
        public object isAdminAllowEdit { get; set; }
        public object subjects { get; set; }
        public object teachings { get; set; }
    }
}
