using System.Collections.Generic;

namespace ImportAccountTeacher.Models
{
    public class GiaoVien
    {
        public string HoTen { get; set; }
        public Dictionary<string, string> MonHocs { get; set; } // key: Tên môn; value: Id môn
        public string DienThoai { get; set; }
        public string Email { get; set; }
    }
}
