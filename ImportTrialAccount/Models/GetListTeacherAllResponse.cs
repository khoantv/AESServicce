using System.Collections.Generic;

namespace ImportTrialAccount.Models
{
    public class GetListTeacherAllResponse
    {
        public List<TeacherInfoModel> list { get; set; }
        public PagingModel page { get; set; }
    }
}
