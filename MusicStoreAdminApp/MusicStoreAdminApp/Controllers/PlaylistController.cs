
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using MusicStoreAdminApp.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System.Reflection;
using System.Text;

namespace MusicStoreAdminApp.Controllers
{
    public class PlaylistController : Controller
    {
        public PlaylistController() 
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://musicstoreweb20240808175547.azurewebsites.net/api/Admin/GetAllPlaylists";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<UserPlaylist>>().Result;
            return View(data);
        }
        public IActionResult Details(string id)
        {
            HttpClient client = new HttpClient();
            //added in next aud
            string URL = "https://musicstoreweb20240808175547.azurewebsites.net/api/Admin/GetDetails";
            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<UserPlaylist>().Result;


            return View(result);
        }

        public FileContentResult CreateInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://musicstoreweb20240808175547.azurewebsites.net/api/Admin/GetDetails";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<UserPlaylist>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{PlaylistID}}", data.id.ToString());
            document.Content.Replace("{{UserName}}", data.Owner.UserName);
            StringBuilder sb = new StringBuilder();
            
            foreach (var item in data.TracksInPlaylist)
            {
                sb.Append("Track: " + item.Track.TrackName + " from Album: " + item.Track.Album.AlbumName + " from Artist: " + item.Track.Album.Artist.ArtistName + "\n");
            }
            document.Content.Replace("{{TrackList}}", sb.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }


        [HttpGet]
        public FileContentResult ExportAllPlaylists() 
        {
            string fileName = "Playlists.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Playlist");
                worksheet.Cell(1, 1).Value = "PlaylistID";
                worksheet.Cell(1, 2).Value = "Creator UserName";
                worksheet.Cell(1, 3).Value = "Total Songs";
                HttpClient client = new HttpClient();
                string URL = "https://musicstoreweb20240808175547.azurewebsites.net/api/Admin/GetAllPlaylists";

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<UserPlaylist>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.id.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Owner.UserName;
                    var total = 0;
                    for (int j = 0; j < item.TracksInPlaylist.Count(); j++)
                    {
                        worksheet.Cell(1, 4 + j).Value = "Track - " + (j + 1);
                        worksheet.Cell(i + 2, 4 + j).Value = item.TracksInPlaylist.ElementAt(j).Track.TrackName;
                        worksheet.Cell(i + 2, 3).Value = item.TracksInPlaylist.Count();
                    }

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

     }
  }
