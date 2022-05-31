namespace ImportTrialAccount.Models
{
    public class CreateNewSchoolResponse
    {
        public int schoolId { get; set; }
        public string name { get; set; }
        public int typeId { get; set; }
        public int phongGDDTId { get; set; }
        public int educationDepartmentId { get; set; }
        public string code { get; set; }
        public int userId { get; set; }
        public object email { get; set; }
        public object phone { get; set; }
        public object representative { get; set; }
        public int position { get; set; }
        public int limitTeacherAccount { get; set; }
        public int totalTeacherAccount { get; set; }
        public int groupTrainingId { get; set; }
        public object address { get; set; }
        public object phongGDDTName { get; set; }
        public object educationDepartmentName { get; set; }
        public object imageUrl { get; set; }
        public object schoolContacts { get; set; }
        public object classModel { get; set; }
    }
}
