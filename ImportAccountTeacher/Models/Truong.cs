using System.Collections.Generic;

namespace ImportAccountTeacher.Models
{
    public class Truong
    {
        public string MaTruong { get; set; }
        public string TenTruong { get; set; }
        public List<GiaoVien> GiaoViens { get; set; }
    }
}
