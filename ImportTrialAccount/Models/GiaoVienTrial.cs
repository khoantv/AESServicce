using System;
using System.Collections.Generic;

namespace ImportTrialAccount.Models
{
    public class GiaoVienTrial
    {
        public int STT { get; set; }
        public string TenSo { get; set; }
        public string IdSo { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string MonHoc { get; set; }
        public string IdKhoaTapHuan { get; set; }
        public long UserId { get; set; }
        public int TeacherId { get; set; }
        public string KetQua { get; set; }
        public string KetQuaGanKTH { get; set; }
        public List<string> IdKhoaTapHuans { get; set; }

        public dynamic this[int index]
        {
            // Đọc dữ liệu theo chỉ mục
            get
            {
                if (index == 0) return STT;
                else if (index == 1) return TenSo;
                else if (index == 2) return IdSo;
                else if (index == 3) return TenTaiKhoan;
                else if (index == 4) return MatKhau;
                else if (index == 5) return MonHoc;
                else if (index == 6) return IdKhoaTapHuan;
                else if (index == 7) return UserId;
                else if (index == 8) return TeacherId;
                else if (index == 9) return KetQua;
                else if (index == 10) return KetQuaGanKTH;
                else if (index == 11) return IdKhoaTapHuans;
                else throw new Exception("Chỉ số không tồn tại");
            }

            // Gán dữ liệu theo chỉ mục
            set
            {
                if (index == 0) STT = value;
                else if (index == 1) TenSo = value;
                else if (index == 2) IdSo = value;
                else if (index == 3) TenTaiKhoan = value;
                else if (index == 4) MatKhau = value;
                else if (index == 5) MonHoc = value;
                else if (index == 6) IdKhoaTapHuan = value;
                else if (index == 7) UserId = value;
                else if (index == 8) TeacherId = value;
                else if (index == 9) KetQua = value;
                else if (index == 10) KetQuaGanKTH = value;
                else if (index == 11) IdKhoaTapHuans = value;
                else throw new Exception("Chỉ số không tồn tại");
            }
        }
    }
}
