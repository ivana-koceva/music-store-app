using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Models.Domain;

namespace MusicStore.Web.Controllers
{
    public class PartnerOrderController : Controller 
    {
        public IActionResult Index()
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7045/api/Api/GetAllActiveOrders";

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<PartnerOrder>>().Result;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                return View("MagicAppNotRunning");
            }
            }
    }
}
