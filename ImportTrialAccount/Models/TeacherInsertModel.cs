using System;

namespace ImportTrialAccount.Models
{
    public class TeacherInsertModel
    {
        public int STT { get; set; }
        public string TenSo { get; set; }
        public string IdSo { get; set; }
        public string TenPhong { get; set; }
        public string IdPhong { get; set; }
        public string TenTruong { get; set; }
        public string IdTruong { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public long UserId { get; set; }
        public int TeacherId { get; set; }
        public string KetQua { get; set; }

        public dynamic this[int index]
        {
            // Đọc dữ liệu theo chỉ mục
            get
            {
                if (index == 0) return STT;
                else if (index == 1) return TenSo;
                else if (index == 2) return IdSo;
                else if (index == 3) return TenPhong;
                else if (index == 4) return IdPhong;
                else if (index == 5) return TenTruong;
                else if (index == 6) return IdTruong;
                else if (index == 7) return TenTaiKhoan;
                else if (index == 8) return MatKhau;
                else if (index == 9) return HoTen;
                else if (index == 10) return Email;
                else if (index == 11) return SoDienThoai;
                else if (index == 12) return UserId;
                else if (index == 13) return TeacherId;
                else if (index == 14) return KetQua;
                else throw new Exception("Chỉ số không tồn tại");
            }

            // Gán dữ liệu theo chỉ mục
            set
            {
                if (index == 0) STT = value;
                else if (index == 1) TenSo = value;
                else if (index == 2) IdSo = value;
                else if (index == 3) TenPhong = value;
                else if (index == 4) IdPhong = value;
                else if (index == 5) TenTruong = value;
                else if (index == 6) IdTruong = value;
                else if (index == 7) TenTaiKhoan = value;
                else if (index == 8) MatKhau = value;
                else if (index == 9) HoTen = value;
                else if (index == 10) Email = value;
                else if (index == 11) SoDienThoai = value;
                else if (index == 12) UserId = value;
                else if (index == 13) TeacherId = value;
                else if (index == 14) KetQua = value;
                else throw new Exception("Chỉ số không tồn tại");
            }
        }
    }
}
