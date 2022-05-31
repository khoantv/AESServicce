using System;

namespace ImportTrialAccount.Models
{
    public class SchoolInsertModel
    {
        public int STT { get; set; }
        public string TenTruong { get; set; }
        public string TenPhong { get; set; }
        public int? IdPhong { get; set; } = null;
        public string TenSo { get; set; }
        public int? IdSo { get; set; } = null;
        public string LoaiHinh { get; set; }
        public int LoaiTruong { get; set; }

        public int IdTruong { get; set; }
        public string Code { get; set; }
        public string KetQua { get; set; }

        public dynamic this[int index]
        {
            // Đọc dữ liệu theo chỉ mục
            get
            {
                if (index == 0) return STT;
                else if (index == 1) return TenTruong;
                else if (index == 2) return TenPhong;
                else if (index == 3) return IdPhong;
                else if (index == 4) return TenSo;
                else if (index == 5) return IdSo;
                else if (index == 6) return LoaiHinh;
                else if (index == 7) return LoaiTruong;
                else if (index == 8) return IdTruong;
                else if (index == 9) return Code;
                else if (index == 10) return KetQua;

                else throw new Exception("Chỉ số không tồn tại");
            }

            // Gán dữ liệu theo chỉ mục
            set
            {
                if (index == 0) STT = value;
                else if (index == 1) TenTruong = value;
                else if (index == 2) IdSo = value;
                else if (index == 3) TenPhong = value;
                else if (index == 4) IdPhong = value;
                else if (index == 5) TenSo = value;
                else if (index == 6) IdSo = value;
                else if (index == 7) LoaiHinh = value;
                else if (index == 8) LoaiTruong = value;
                else if (index == 9) IdTruong = value;
                else if (index == 10) Code = value;
                else if (index == 11) KetQua = value;

                else throw new Exception("Chỉ số không tồn tại");
            }
        }
    }
}
