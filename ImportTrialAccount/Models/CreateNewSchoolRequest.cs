namespace ImportTrialAccount.Models
{
    public class CreateNewSchoolRequest
    {
        public int? educationDepartmentId { get; set; }
        public int? phongGDDTId { get; set; }
        public int typeId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }
}
