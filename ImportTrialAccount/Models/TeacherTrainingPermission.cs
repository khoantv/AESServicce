using System;
using System.Collections.Generic;

namespace ImportTrialAccount.Models
{
    public class TeacherTrainingPermission
    {
        public int STT { get; set; }
        public string TenTaiKhoan { get; set; }
        public List<string> IdKhoaTapHuans { get; set; }
        public long UserId { get; set; }
        public int TeacherId { get; set; }
        public string KetQua { get; set; }

        public dynamic this[int index]
        {
            // Đọc dữ liệu theo chỉ mục
            get
            {
                if (index == 0) return STT;
                else if (index == 1) return TenTaiKhoan;
                else if (index == 2) return UserId;
                else if (index == 3) return TeacherId;
                else if (index == 4) return IdKhoaTapHuans;
                else if (index == 5) return KetQua;
                else throw new Exception("Chỉ số không tồn tại");
            }

            // Gán dữ liệu theo chỉ mục
            set
            {
                if (index == 0) STT = value;
                else if (index == 1) TenTaiKhoan = value;
                else if (index == 2) UserId = value;
                else if (index == 3) TeacherId = value;
                else if (index == 4) IdKhoaTapHuans = value;
                else if (index == 5) KetQua = value;
                else throw new Exception("Chỉ số không tồn tại");
            }
        }
    }
}
