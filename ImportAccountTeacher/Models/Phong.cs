using System.Collections.Generic;

namespace ImportAccountTeacher.Models
{
    public class Phong
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public List<Truong> Truongs { get; set; }
    }
}
