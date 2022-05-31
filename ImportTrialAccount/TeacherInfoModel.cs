namespace ImportTrialAccount
{
    public class TeacherInfoModel
    {
        public int teacherId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public object firstName { get; set; }
        public object lastName { get; set; }
        public object email { get; set; }
        public object phoneNumber { get; set; }
        public object imageUrl { get; set; }
        public int userId { get; set; }
        public int schoolTypeId { get; set; }
        public object schoolTypeName { get; set; }
        public object schoolName { get; set; }
        public object phongGDDTName { get; set; }
        public object educationDepartmentName { get; set; }
        public object address { get; set; }
        public int schoolId { get; set; }
        public int phongGDDTId { get; set; }
        public int educationDepartmentId { get; set; }
        public int status { get; set; }
        public int isDelete { get; set; }
        public bool isLock { get; set; }
        public object isFirstLogin { get; set; }
        public object qualification { get; set; }
        public object isFinishInfoTeacher { get; set; }
        public object[] subjects { get; set; }
        public object teachings { get; set; }
    }

}
