using ImportTrialAccount.Models;

namespace ImportTrialAccount.Services
{
    public static class TempData
    {
        public static string URL { get; set; }
        public static string TenDangNhap { get; set; }
        public static string MatKhau { get; set; }
        public static string Token { get; set; }
        public static LoginResponse LoginResponseData { get; set; }
    }
}
