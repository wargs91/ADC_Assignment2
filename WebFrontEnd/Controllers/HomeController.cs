using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebFrontEnd.Models;


namespace WebFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        [HttpPost]
        public IActionResult insertCentre([FromBody] AdminCentreName centreName)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/AdminCentreNames", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centreName));
            RestResponse restResponse = restClient.Execute(restRequest);

           AdminCentreName returnName = JsonConvert.DeserializeObject<AdminCentreName>(restResponse.Content);
            if (returnName != null)
            {
                return Ok(returnName);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult createBooking([FromBody] CentreBooking newBooking)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/CentreBookings", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(newBooking));
            RestResponse restResponse = restClient.Execute(restRequest);

            AdminCentreName returnName = JsonConvert.DeserializeObject<AdminCentreName>(restResponse.Content);
            if (returnName != null)
            {
                return Ok(returnName);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
