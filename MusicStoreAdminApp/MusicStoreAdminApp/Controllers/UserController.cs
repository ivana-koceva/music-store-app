
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using MusicStoreAdminApp.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System.Reflection;
using System.Text;

namespace MusicStoreAdminApp.Controllers
{
    public class UserController : Controller
    {
        public UserController() 
        {
       
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7245/api/Admin/GetAllUsers";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<MusicStoreUser>>().Result;
            return View(data);
        }

        public IActionResult ImportUsers(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<User> users = getAllUsersFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7245/api/Admin/ImportAllUsers";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "User");

        }

        private List<User> getAllUsersFromFile(string fileName)
        {
            List<User> users = new List<User>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        users.Add(new Models.User
                        {
                            Email = reader.GetValue(0)?.ToString(),
                            Password = reader.GetValue(1)?.ToString(),
                            ConfirmPassword = reader.GetValue(2)?.ToString(),
                        });
                    }

                }
            }
            return users;

        }
    }

}
 
