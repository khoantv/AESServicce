using ImportTrialAccount.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrialAccount.Services
{
    public class HTSService
    {
        public static string token = string.Empty;
        private static string urlRoot = TempData.URL; // ConfigurationManager.AppSettings["api-url"];

        public static async Task<ResponseObject<LoginResponse>> LoginAsync(string tenDN, string mk)
        {
            string urlApi = "api/login";

            var loginRequest = new LoginRequest
            {
                email = tenDN,
                password = mk
            };

            var json = JsonConvert.SerializeObject(loginRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var loginResponse = JsonConvert.DeserializeObject<ResponseObject<LoginResponse>>(result);
            TempData.LoginResponseData = loginResponse.data;
            if (loginResponse.data != null)
            {
                token = loginResponse.data.token;
            }

            return loginResponse;
        }

        public static async Task<ResponseObject<string>> GetTeacherCode(int schoolId)
        {
            string urlApi = "api/Teacher/GetTeacherCode?schoolId=" + schoolId;

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Bearer " + token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<string>>(result);
        }

        public static async Task<ResponseObject<CreateNewTeacherResponse>> CreateNewTeacher(CreateNewTeacherRequest request)
        {
            string urlApi = "api/Teacher/CreateNewTeacherWithPass";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var createNewTeacherResponse = JsonConvert.DeserializeObject<ResponseObject<CreateNewTeacherResponse>>(result);

            return createNewTeacherResponse;
        }

        public static async Task<ResponseObject<List<EducationDepartmentGetIdCodeNameModel>>> GetAllEducationDepartment()
        {
            string urlApi = "api/EducationDepartment/GetAllEducationDepartment";

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<List<EducationDepartmentGetIdCodeNameModel>>>(result);
        }

        public static async Task<ResponseObject<string>> ResetPass(ResetPassModel request)
        {
            string urlApi = "api/UserResetPassword";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var resetPassResponse = JsonConvert.DeserializeObject<ResponseObject<string>>(result);

            return resetPassResponse;
        }

        public static async Task<ResponseObject<IdentityResult>> ChangePasswordByUserName(UserChangePasswordByUserNameModel request)
        {
            string urlApi = "api/change-password-by-username";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var changePassResponse = JsonConvert.DeserializeObject<ResponseObject<IdentityResult>>(result);

            return changePassResponse;
        }

        public static async Task<ResponseObject<bool>> TeacherTrainingPermissonInsert(int teacherId, int trainingId)
        {
            string urlApi = "api/Teacher/TeacherTrainingPermissonInsert/" + teacherId + "/" + trainingId;

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<bool>>(result);
        }

        public static void ExportExcel(string fileName, string sheetName, List<string> headerNames, List<GiaoVienTrial> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("User Id");
                headerNames.Add("Teacher Id");
                headerNames.Add("Kết quả");
                headerNames.Add("Kết quả Gán khoá tập huấn");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        row.CreateCell(cellIndex).SetCellValue(gv[cellIndex] ?? "Không lấy được dữ liệu");
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static void ExportExcel(string fileName, string sheetName, List<string> headerNames, List<TeacherInsertModel> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("User Id");
                headerNames.Add("Teacher Id");
                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        row.CreateCell(cellIndex).SetCellValue(gv[cellIndex] ?? "Không lấy được dữ liệu");
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static async Task<ResponseObject<int>> CheckEmail(long userId, string email)
        {
            string urlApi = "api/Teacher/CheckEmail";

            var request = new
            {
                userId = userId,
                email = email,
            };

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var checkEmailResponse = JsonConvert.DeserializeObject<ResponseObject<int>>(result);

            return checkEmailResponse;
        }

        public static async Task<ResponseObject<object>> UpdateEmailTeacher(UpdateEmailModel request)
        {
            string urlApi = "api/Teacher/UpdateEmailTeacher";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var checkEmailResponse = JsonConvert.DeserializeObject<ResponseObject<object>>(result);

            return checkEmailResponse;
        }

        public static async Task<ResponseObject<bool>> ActiveTeacher(long userId)
        {
            string urlApi = "api/Teacher/active-account?userId=" + userId;

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Bearer " + token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<bool>>(result);
        }

        public static async Task<ResponseObject<GetListTeacherAllResponse>> GetListTeacherAll(int pageIndex = 1, int pageSize = 10)
        {
            string urlApi = "api/Teacher/GetListTeacherAll";

            var url = Path.Combine(urlRoot, urlApi);

            var request = new
            {
                direction = "",
                isDelete = 0,
                isLock = 0,
                isStatus = 1,  // Chưa đăng nhập
                keyword = "",
                pageIndex = pageIndex,
                pageSize = pageSize,
                sort = ""
            };

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<GetListTeacherAllResponse>>(result);
        }

        public static async Task<ResponseObject<PhongTruongIdModel>> GetPhongSchoolId(string phongCode, string schoolCode)
        {
            string urlApi = "api/PhongGDDT/GetPhongSchoolId?phongCode=" + phongCode + "&schoolCode=" + schoolCode;

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<PhongTruongIdModel>>(result);
        }

        public static async Task<ResponseObject<string>> GetSchoolCode(int? phongGDDTId, int typeId)
        {
            string urlApi = "api/School/GetSchoolCode?phongGDDTId=" + phongGDDTId + "&typeId=" + typeId;

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResponseObject<string>>(result);
        }

        public static async Task<ResponseObject<CreateNewSchoolResponse>> CreateNewSchool(CreateNewSchoolRequest request)
        {
            string urlApi = "api/School/SchoolCreate";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Path.Combine(urlRoot, urlApi);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var createNewSchoolResponse = JsonConvert.DeserializeObject<ResponseObject<CreateNewSchoolResponse>>(result);

            return createNewSchoolResponse;
        }
    }
}
