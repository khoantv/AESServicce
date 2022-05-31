using System;

namespace ImportTrialAccount.Models
{
    public class GiaoVienUpdateEmailModel
    {
        public int STT { get; set; }
        public string TenTaiKhoan { get; set; }
        public string Email { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string KetQua { get; set; }

        public dynamic this[int index]
        {
            // Đọc dữ liệu theo chỉ mục
            get
            {
                if (index == 0) return STT;
                else if (index == 1) return TenTaiKhoan;
                else if (index == 2) return Email;
                else if (index == 3) return UserId;
                else if (index == 4) return TeacherId;
                else if (index == 5) return KetQua;
                else throw new Exception("Chỉ số không tồn tại");
            }

            // Gán dữ liệu theo chỉ mục
            set
            {
                if (index == 0) STT = value;
                else if (index == 1) TenTaiKhoan = value;
                else if (index == 2) Email = value;
                else if (index == 3) UserId = value;
                else if (index == 4) TeacherId = value;
                else if (index == 5) KetQua = value;
                else throw new Exception("Chỉ số không tồn tại");
            }
        }
    }
}
